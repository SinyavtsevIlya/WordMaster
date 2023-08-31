using UniRx;

namespace WordMaster
{
    public class Energy
    {
        public IReactiveProperty<int> Current { get; }
        public IReactiveProperty<int> Max { get; }

        public Energy(IReactiveProperty<int> current, IReactiveProperty<int> max)
        {
            Current = current;
            Max = max;
        }
    }
}