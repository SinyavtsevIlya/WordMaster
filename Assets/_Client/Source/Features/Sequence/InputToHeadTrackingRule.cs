using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class InputToHeadTrackingRule : IRule, IInitializable
    {
        private readonly Sequence _sequence;
        private readonly SequenceSettings _settings;
        private readonly CompositeDisposable _disposables;

        public InputToHeadTrackingRule(Sequence sequence, SequenceSettings settings, CompositeDisposable disposables)
        {
            _sequence = sequence;
            _settings = settings;
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
            var currentPosition = _sequence.Head.Value.Letter.Position.Value;
            var t = Time.deltaTime * _settings.HeadTrackingSmoothness;
            _sequence.Head.Value.Letter.Position.Value = Vector2.Lerp(currentPosition, worldPosition, t);
        }
    }
}