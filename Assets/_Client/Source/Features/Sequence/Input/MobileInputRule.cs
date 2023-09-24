using Rules;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class MobileInputRule : IRule
    {
        private readonly Sequence _sequence;
        private readonly CompositeDisposable _disposables;
        private readonly Plane _plane;

        public MobileInputRule(Sequence sequence, SequenceSettings settings, CompositeDisposable disposables)
        {
            _sequence = sequence;
            _disposables = disposables;

            _plane = new Plane(Vector3.forward, 0);
        }
        
        public void Initialize()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButton(0))
                .Select(_ => Input.mousePosition)
                .Select(GetWorldPosition)
                .Pairwise()
                .Where(_ => !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonUp(0))
                .Subscribe(ApplyTranslation)
                .AddTo(_disposables);
        }

        private Vector3 GetWorldPosition(Vector3 mousePosition)
        {
            mousePosition.z = 1f;
            var ray = Camera.main.ScreenPointToRay(mousePosition);
            return _plane.Raycast(ray, out var distance) ? ray.GetPoint(distance) : default;
        }

        private void ApplyTranslation(Pair<Vector3> worldPositions)
        {
            var delta = worldPositions.Current - worldPositions.Previous;
            _sequence.Head.Value.Letter.Position.Value += (Vector2)delta;
        }
    }
}