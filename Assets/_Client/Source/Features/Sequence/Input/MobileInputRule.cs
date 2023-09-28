using Rules;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class MobileInputRule : IRule
    {
        private readonly Sequence _sequence;
        private readonly CompositeDisposable _disposables;
        private readonly Camera _camera;
        
        private Plane _plane;

        public MobileInputRule(Sequence sequence, SequenceSettings settings, CompositeDisposable disposables, Camera camera)
        {
            _sequence = sequence;
            _disposables = disposables;
            _camera = camera;

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
            var ray = _camera.ScreenPointToRay(mousePosition);
            return _plane.Raycast(ray, out var distance) ? ray.GetPoint(distance) : default;
        }

        private void ApplyTranslation(Pair<Vector3> worldPositions)
        {
            var delta = worldPositions.Current - worldPositions.Previous;
            var position = _sequence.Head.Value.Letter.Position;
            position.Value += (Vector2)delta;
            
            ClampWithinScreen(position);
        }

        private void ClampWithinScreen(IReactiveProperty<Vector2> position)
        {
            var viewportPoint = _camera.WorldToViewportPoint(position.Value);
            var boundsViewportPoint = _camera.WorldToViewportPoint(position.Value + Vector2.one * _sequence.Head.Value.Settings.NodeBoundSize);
            var bounds = boundsViewportPoint - viewportPoint;
            var aspect = Screen.width / Screen.height;
            var horizontalOffset = bounds.x / aspect;
            var verticalOffset = bounds.y * aspect;
            viewportPoint.x = Mathf.Clamp(viewportPoint.x, horizontalOffset, 1f - horizontalOffset);
            viewportPoint.y = Mathf.Clamp(viewportPoint.y, verticalOffset, 1f - verticalOffset);
            position.Value = _camera.ViewportToWorldPoint(viewportPoint);
        }
    }
}