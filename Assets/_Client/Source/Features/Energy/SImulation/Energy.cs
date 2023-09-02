using UniRx;

namespace WordMaster
{
    public class Energy
    {
        public IReactiveProperty<float> Current { get; }
        public IReactiveProperty<float> Max { get; }

        public Energy(IReactiveProperty<float> current, IReactiveProperty<float> max)
        {
            Current = current;
            Max = max;
        }
    }
}