using UniRx;

namespace WordMaster
{
    public class LoseEvent : ReactiveEvent<Unit> { }
    public class ResumeEvent : ReactiveEvent<Unit> { }
    public class RanOutOfEnergyEvent : ReactiveEvent<Unit> { }
}