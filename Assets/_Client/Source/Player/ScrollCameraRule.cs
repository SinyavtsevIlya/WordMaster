using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class ScrollCameraRule : IRule, IInitializable
    {
        private readonly CameraSettings _settings;
        private readonly Camera _camera;
        private readonly CompositeDisposable _disposables;

        public ScrollCameraRule(CameraSettings settings, Camera camera, CompositeDisposable disposables)
        {
            _settings = settings;
            _camera = camera;
            _disposables = disposables;
        }

        public void Initialize()
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {
                var translation = Vector3.right * Time.deltaTime * _settings.ScrollSpeed;
                _camera.transform.Translate(translation);
            }).AddTo(_disposables);
        }
    }
}