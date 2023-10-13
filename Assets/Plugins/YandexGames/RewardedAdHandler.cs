using System;

public class RewardedAdHandler : IRewardedAdHandler
{
    public static int _NextHandlerId;
    
    public int Id { get; }
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
#if UNITY_EDITOR
        callback?.Invoke();
#endif
        
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
        Id = _NextHandlerId++;
    }
    
    public void SetOpen() => RewardedAdOpen?.Invoke();

    public void SetWatched() => RewardedAdWatched?.Invoke();

    public void SetClosed() => RewardedAdClosed?.Invoke();

    public void SetError() => RewardedAdError?.Invoke();

    public void Dispose()
    {
        Disposed?.Invoke();
        Disposed = null;
        RewardedAdOpen = null;
        RewardedAdWatched = null;
        RewardedAdClosed = null;
        RewardedAdError = null;
    }
}