using System;

public interface IRewardedAdHandler : IDisposable
{
    public int Id { get; }
    public Action Disposed { get; set; }
    public void SetOpen();
    public void SetWatched();
    public void SetClosed();
    public void SetError();
}