using UniRx;

namespace WordMaster
{
    public class Sequence
    {
        public IReactiveCollection<Node> Value { get; }
        public IReadOnlyReactiveProperty<Node> Head => Value.ObserveAdd().Select(e => e.Value).ToReadOnlyReactiveProperty();

        public int Radius { get; }

        public CompositeDisposable Disposables { get; }
    }
}