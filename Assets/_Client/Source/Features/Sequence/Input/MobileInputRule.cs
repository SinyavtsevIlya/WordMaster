using Rules;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class MobileInputRule : IRule
    {
        private readonly Sequence _sequence;
        private readonly CompositeDisposable _disposables;

        public MobileInputRule(Sequence sequence, CompositeDisposable disposables)
        {
            _sequence = sequence;
            _disposables = disposables;
        }
        
        public void Initialize()
        {
            Observable.EveryUpdate()
                .Select(_ => Input.mousePosition)
                .Where(_ => Input.GetMouseButton(0))
                .Pairwise().Select(pair => pair.Current - pair.Previous)
                .Subscribe(TrackPointerPosition)
                .AddTo(_disposables);
        }

        private void TrackPointerPosition(Vector3 dragDelta)
        {
            _sequence.Head.Value.Letter.Position.Value += (Vector2)dragDelta;
        }
    }
}