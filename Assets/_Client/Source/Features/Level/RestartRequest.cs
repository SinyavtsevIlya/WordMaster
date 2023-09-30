using UniRx;

namespace WordMaster
{
    public class RestartRequest : ReactiveEvent<Unit> { }
    public class ResumeRequest : ReactiveEvent<Unit> { }
    public class RanOutOfEnergyEvent : ReactiveEvent<Unit> { }
}