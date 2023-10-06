using System;
using System.Collections.Generic;
using System.Linq;
using Rules;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace WordMaster
{
    public class SidePropsGenerationRule : IRule
    {
        private readonly Player _player;
        private readonly Level _level;
        private readonly ZoomSettings _zoomSettings;

        private Queue<GameObject> _pool;
        private int _generationStep;
        
        public SidePropsGenerationRule(Player player, Level level, ZoomSettings zoomSettings)
        {
            _player = player;
            _level = level;
            _zoomSettings = zoomSettings;
        }
        
        public void Initialize()
        {
            var backgroundRenderer = _level.Settings.Background.transform.GetChild(0).GetComponent<Renderer>();
            _pool = new Queue<GameObject>(_level.Settings.Props.Select(prop => prop.Prefab).Select(Object.Instantiate));
            
            foreach (var instance in _pool) 
                instance.SetActive(false);

            GenerateBackground(-backgroundRenderer.bounds.size.x / _zoomSettings.Value);
            GenerateBackground(0);
            RefreshGenerationStep();

            var distanceChange = _player.ObserveEveryValueChanged(player => Mathf.RoundToInt(player.DistancePassed) + _level.LevelHalfWidth + 10)
                .ToReadOnlyReactiveProperty();
            
            distanceChange
                .Where(distancePassed => distancePassed % (int)backgroundRenderer.bounds.size.x == 0)
                .Select(d => (float) d)
                .Subscribe(GenerateBackground)
                .AddTo(_level.Disposables);
            
            distanceChange
                .Where(distancePassed => distancePassed % _generationStep == 0)
                .Subscribe(GenerateProps)
                .AddTo(_level.Disposables);
        }

        private void GenerateProps(int generationPosition)
        {
            var sign = Random.value > .5f ? 1 : -1;
            var spawnPosition = new Vector3(generationPosition, _level.Height / 2f * sign, 0f);

            if (_level.IsColliding(Vector2Int.RoundToInt(spawnPosition), 5))
                return;
            
            var propInstance = _pool.Dequeue();
            propInstance.SetActive(true);
            propInstance.transform.position = spawnPosition;
            propInstance.transform.Rotate(Vector3.forward, Random.Range(-25, 25), Space.World);

            IDisposable cullingDisposable = null;
            
            cullingDisposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (_player.DistancePassed - propInstance.transform.position.x > _level.LevelHalfWidth)
                    {
                        cullingDisposable?.Dispose();
                        propInstance.SetActive(false);
                        _pool.Enqueue(propInstance);
                    }
                })
                .AddTo(_level.Disposables)
                .AddTo(propInstance);

            RefreshGenerationStep();
        }

        private void RefreshGenerationStep()
        {
            _generationStep = 12;
        }

        private void GenerateBackground(float generationPosition)
        {
            var background = Object.Instantiate(_level.Settings.Background);
            background.transform.position += Vector3.right * generationPosition;
            background.transform.localScale = Vector3.one / _zoomSettings.Value;
        }
    }
}