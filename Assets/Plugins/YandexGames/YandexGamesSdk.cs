using System.Collections.Generic;
using System.Runtime.InteropServices;
using UniRx;
using UnityEngine;
using UnityEngine.Scripting;

public class YandexGamesSdk : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowRewardedAdExternal(int id);

    [DllImport("__Internal")]
    private static extern string GetLanguageExternal();

    private static Dictionary<int, IRewardedAdHandler> _handlers = new Dictionary<int, IRewardedAdHandler>();

    public static SystemLanguage GetUserLanguage()
    {
        if (Application.isEditor) 
            return Application.systemLanguage;
        
        var languageCode = GetLanguageExternal();

        if (languageCode == "ru") return SystemLanguage.Russian;
        if (languageCode == "tr") return SystemLanguage.Turkish;
        
        return SystemLanguage.English;
    } 

    public static RewardedAdHandler ShowRewardedAd()
    {
        var handler = new RewardedAdHandler();
        
        _handlers.Add(handler.Id, handler);
        handler.Disposed += () => _handlers.Remove(handler.Id);
        
        if (!Application.isEditor)
            ShowRewardedAdExternal(handler.Id);
        
        return handler;
    }

    [Preserve]
    public void OnRewardedAdOpen(int id)
    {
        if (_handlers.TryGetValue(id, out var handler)) 
            handler.SetOpen();
    }

    [Preserve]
    public void OnRewardedAdWatched(int id)
    {
        MainThreadDispatcher.Post(_ =>
        {
            if (_handlers.TryGetValue(id, out var handler))
            {
                handler.SetWatched();
                _handlers.Remove(id);
            }
        }, null);
    }

    [Preserve]
    public void OnRewardedAdClosed(int id)
    {
        MainThreadDispatcher.Post(_ =>
        {
            if (_handlers.TryGetValue(id, out var handler))
            {
                handler.SetClosed();
                _handlers.Remove(id);
            }
        }, null);
    }

    [Preserve]
    public void OnRewardedAdError(int id)
    {
        MainThreadDispatcher.Post(_ =>
        {
            if (_handlers.TryGetValue(id, out var handler))
            {
                handler.SetError();
                _handlers.Remove(id);
            }
        }, null);
    }
}
