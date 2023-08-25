using UniRx;

namespace WordMaster
{
    public class Sequence
    {
        public IReactiveCollection<Node> Value { get; }
        public IReadOnlyReactiveProperty<Node> Head => Value.ObserveAdd().Select(e => e.Value).ToReadOnlyReactiveProperty();

        public CompositeDisposable Disposables { get; }

        public Sequence(IReactiveCollection<Node> value, Node head, CompositeDisposable disposables)
        {
            Value = value;
            Disposables = disposables;
            value.Add(head);
        }
    }
}