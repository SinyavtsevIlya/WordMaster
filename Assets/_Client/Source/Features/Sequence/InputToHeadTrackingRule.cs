using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class InputToHeadTrackingRule : IRule, IInitializable
    {
        private readonly Sequence _sequence;
        private readonly CompositeDisposable _disposables;

        public InputToHeadTrackingRule(Sequence sequence, CompositeDisposable disposables)
        {
            _sequence = sequence;
            _disposables = disposables;
        }
        
        public void Initialize()
        {
            Observable.EveryUpdate()
                .Select(_ => Input.mousePosition)
                .Subscribe(TrackPointerPosition)
                .AddTo(_disposables);
        }

        private void TrackPointerPosition(Vector3 mousePosition)
        {
            mousePosition.z = 1f;
            var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            _sequence.Head.Value.Letter.Position.Value = worldPosition;
        }
    }
}