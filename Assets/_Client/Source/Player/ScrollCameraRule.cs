using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class ScrollCameraRule : IRule, IInitializable
    {
        private readonly CameraSettings _settings;
        private readonly Camera _camera;
        private readonly Player _player;
        private readonly CompositeDisposable _disposables;

        public ScrollCameraRule(CameraSettings settings, Camera camera, Player player, CompositeDisposable disposables)
        {
            _settings = settings;
            _camera = camera;
            _player = player;
            _disposables = disposables;
        }

        public void Initialize()
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {
                var localCompleteness = _player.Sequence.Head.Value.Letter.Position.Value.x - _player.DistancePassed;
                var speedup = _settings.CompletenessToSpeedup.Evaluate(localCompleteness);
                var translation = Vector3.right * Time.deltaTime * _settings.ScrollSpeed * speedup;
                _camera.transform.Translate(translation);
            }).AddTo(_disposables);
        }
    }
}