using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        Debug.Log($"OnRewardedAdWatched {id}");
        if (_handlers.TryGetValue(id, out var handler))
        {
            Debug.Log("Set Watched");
            handler.SetWatched();
            _handlers.Remove(id);
        }
    }

    [Preserve]
    public void OnRewardedAdClosed(int id)
    {
        if (_handlers.TryGetValue(id, out var handler))
        {
            handler.SetClosed();
            _handlers.Remove(id);
        }
    }

    [Preserve]
    public void OnRewardedAdError(int id)
    {
        if (_handlers.TryGetValue(id, out var handler))
        {
            handler.SetError();
            _handlers.Remove(id);
        }
    }
}
