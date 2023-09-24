using UniRx;

namespace WordMaster
{
    public class ReactiveEvent<TArg>
    {
        public Subject<TArg> Value { get; }

        public ReactiveEvent()
        {
            Value = new Subject<TArg>();
        }
    }
}