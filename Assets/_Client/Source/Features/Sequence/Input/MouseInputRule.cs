using Rules;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class MouseInputRule : IRule
    {
        private readonly Sequence _sequence;
        private readonly SequenceSettings _settings;
        private readonly CompositeDisposable _disposables;
        private readonly Plane _plane;

        public MouseInputRule(Sequence sequence, SequenceSettings settings, CompositeDisposable disposables)
        {
            _sequence = sequence;
            _settings = settings;
            _disposables = disposables;

            _plane = new Plane(Vector3.forward, 0);
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
            var ray = Camera.main.ScreenPointToRay(mousePosition);
            if (_plane.Raycast(ray, out var distance))
            {
                var worldPosition = ray.GetPoint(distance);
                var currentPosition = _sequence.Head.Value.Letter.Position.Value;
                var t = Time.deltaTime * _settings.HeadTrackingSmoothness;
                _sequence.Head.Value.Letter.Position.Value = Vector2.Lerp(currentPosition, worldPosition, t);
            }
        }
    }
}