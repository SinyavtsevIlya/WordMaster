using System;
using Rules;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class DistanceMarkerGenerationRule : IRule
    {
        private readonly Player _player;
        private readonly Level _level;
        private readonly DistanceMarkerFactory _distanceMarkerFactory;
        private readonly BestDistanceMarkerFactory _bestDistanceMarkerFactory;

        public DistanceMarkerGenerationRule(Player player, Level level, DistanceMarkerFactory distanceMarkerFactory,
            BestDistanceMarkerFactory bestDistanceMarkerFactory)
        {
            _player = player;
            _level = level;
            _distanceMarkerFactory = distanceMarkerFactory;
            _bestDistanceMarkerFactory = bestDistanceMarkerFactory;
        }
        
        public void Initialize()
        {
            var offset = 55;
            
            _player
                .ObserveEveryValueChanged(player => Mathf.RoundToInt(player.DistancePassed) + offset)
                .Where(distancePassed => distancePassed % offset == 0)
                .Subscribe(GenerateDistanceMarkers)
                .AddTo(_level.Disposables);

            Disposable.Create(() =>
            {
                _player.BestDistancePassed.Value = Math.Max(_player.BestDistancePassed.Value, _player.DistancePassed);
            }).AddTo(_player.Sequence.Disposables);

            if (_player.BestDistancePassed.Value > 0)
            {
                var bestDistanceMarker = _bestDistanceMarkerFactory.Create();
                bestDistanceMarker.SetDistance(_player.BestDistancePassed.Value, _level.HalfHeight - _level.Settings.VerticalOffset);
                
                var position = Vector2Int.RoundToInt(bestDistanceMarker.transform.position);
                _level.UsedPositions.Add(position);
            }
        }

        private void GenerateDistanceMarkers(int horizontalPosition)
        {
            var distanceMarker = _distanceMarkerFactory.Create();
            distanceMarker.SetDistance(horizontalPosition, _level.HalfHeight - _level.Settings.VerticalOffset);
            
            var position = Vector2Int.RoundToInt(distanceMarker.transform.position);
            _level.UsedPositions.Add(position);
            
            IDisposable cullingDisposable = null;
            cullingDisposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (_player.DistancePassed - distanceMarker.transform.position.x > _level.LevelHalfWidth)
                    {
                        cullingDisposable?.Dispose();
                        distanceMarker.Dispose();
                    }
                })
                .AddTo(_level.Disposables)
                .AddTo(distanceMarker);
            
            Disposable
                .Create(() => _level.UsedPositions.Remove(position))
                .AddTo(distanceMarker);
        }
    }
}