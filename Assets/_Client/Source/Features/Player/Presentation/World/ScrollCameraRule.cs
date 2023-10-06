using System;
using Rules;
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
        private readonly ZoomSettings _zoomSettings;

        public ScrollCameraRule(CameraSettings settings, Camera camera, Player player, CompositeDisposable disposables, 
            ZoomSettings zoomSettings)
        {
            _settings = settings;
            _camera = camera;
            _player = player;
            _disposables = disposables;
            _zoomSettings = zoomSettings;
        }

        public void Initialize()
        {
            var screenChange = this.ObserveEveryValueChanged(_ => Screen.width + Screen.height);
            
            screenChange
                .Subscribe(_ => Time.timeScale = 0f).AddTo(_disposables);
            
            screenChange
                .Throttle(TimeSpan.FromMilliseconds(250), Scheduler.MainThreadIgnoreTimeScale)
                .Subscribe(_ => Time.timeScale = 1f).AddTo(_disposables);
            
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    var localCompleteness = _player.Sequence.Head.Value.Letter.Position.Value.x - _player.DistancePassed;
                    var speedup = _settings.CompletenessToSpeedup.Evaluate(localCompleteness);
                    var translation = Vector3.right * Time.deltaTime * _settings.ScrollSpeed * speedup;
                    _camera.transform.Translate(translation);
                }).AddTo(_disposables);

            _camera.fieldOfView /= _zoomSettings.Value;
        }
    }
}