using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RewardedAdHandler : IRewardedAdHandler
{
    public string Id { get; private set; }
    public Action Disposed { get; set; }

    public event Action RewardedAdOpen;
    public event Action RewardedAdWatched;
    public event Action RewardedAdClosed;
    public event Action RewardedAdError;

    public RewardedAdHandler OnRewardedAdOpen(Action callback)
    {
        RewardedAdOpen += callback;
        return this;
    }

    public RewardedAdHandler OnRewardedAdWatched(Action callback)
    {
        RewardedAdWatched += callback;
        return this;
    }

    public RewardedAdHandler OnRewardedAdClosed(Action callback)
    {
        RewardedAdClosed += callback;
        return this;
    }

    public RewardedAdHandler OnRewardedAdError(Action callback)
    {
        RewardedAdError += callback;
        return this;
    }

    public RewardedAdHandler()
    {
        Id = Guid.NewGuid().ToString();
    }
    
    public void SetOpen() => RewardedAdOpen?.Invoke();

    public void SetWatched() => RewardedAdWatched?.Invoke();

    public void SetClosed() => RewardedAdClosed?.Invoke();

    public void SetError() => RewardedAdError?.Invoke();

    public void Dispose()
    {
        Disposed?.Invoke();
        Id = null;
        Disposed = null;
        RewardedAdOpen = null;
        RewardedAdWatched = null;
        RewardedAdClosed = null;
        RewardedAdError = null;
    }
}

public interface IRewardedAdHandler : IDisposable
{
    public string Id { get; }
    public Action Disposed { get; set; }
    public void SetOpen();
    public void SetWatched();
    public void SetClosed();
    public void SetError();
}

public class YandexGamesSdk : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowRewardedAdExternal(string id);

    private static Dictionary<string, IRewardedAdHandler> _handlers = new Dictionary<string, IRewardedAdHandler>();

    public static RewardedAdHandler ShowRewardedAd()
    {
        var handler = new RewardedAdHandler();
        _handlers.Add(handler.Id, handler);
        handler.Disposed += () => _handlers.Remove(handler.Id);
        ShowRewardedAdExternal(handler.Id);

        return handler;
    }

    public void OnRewardedAdOpen(string id)
    {
        if (_handlers.TryGetValue(id, out var handler)) 
            handler.SetOpen();
    }

    public void OnRewardedAdWatched(string id)
    {
        if (_handlers.TryGetValue(id, out var handler))
        {
            handler.SetWatched();
            _handlers.Remove(id);
        }
    }

    public void OnRewardedAdClosed(string id)
    {
        if (_handlers.TryGetValue(id, out var handler))
        {
            handler.SetClosed();
            _handlers.Remove(id);
        }
    }

    public void OnRewardedAdError(string id)
    {
        if (_handlers.TryGetValue(id, out var handler))
        {
            handler.SetError();
            _handlers.Remove(id);
        }
    }
}
