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

        private Queue<GameObject> _pool;
        private int _generationStep;
        
        public SidePropsGenerationRule(Player player, Level level)
        {
            _player = player;
            _level = level;
        }
        
        public void Initialize()
        {
            var backgroundRenderer = _level.Settings.Background.transform.GetChild(0).GetComponent<Renderer>();

            GenerateBackground(-(int)backgroundRenderer.bounds.size.x);
            GenerateBackground(0);
            RefreshGenerationStep();

            var distanceChange = _player.ObserveEveryValueChanged(player => Mathf.RoundToInt(player.DistancePassed) + _level.Settings.LevelHalfWidth + 10)
                .ToReadOnlyReactiveProperty();
            
            distanceChange
                .Where(distancePassed => distancePassed % (int)backgroundRenderer.bounds.size.x == 0)
                .Subscribe(GenerateBackground)
                .AddTo(_level.Disposables);
            
            distanceChange
                .Where(distancePassed => distancePassed % _generationStep == 0)
                .Subscribe(GenerateProps)
                .AddTo(_level.Disposables);

            _pool = new Queue<GameObject>(_level.Settings.Props.Select(prop => prop.Prefab).Select(Object.Instantiate));
            
            foreach (var instance in _pool) 
                instance.SetActive(false);
        }

        private void GenerateProps(int generationPosition)
        {
            var propInstance = _pool.Dequeue();
            propInstance.SetActive(true);
            var sign = Random.value > .5f ? 1 : -1;
            propInstance.transform.position = new Vector3(generationPosition, _level.Settings.Height / 2f * sign, 0f);
            propInstance.transform.Rotate(Vector3.forward, Random.Range(-25, 25), Space.World);
            
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (_player.DistancePassed - propInstance.transform.position.x > _level.Settings.LevelHalfWidth)
                    {
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

        private void GenerateBackground(int generationPosition)
        {
            var background = Object.Instantiate(_level.Settings.Background);
            background.transform.position += Vector3.right * generationPosition;
        }
    }
}