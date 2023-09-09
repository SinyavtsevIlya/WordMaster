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
            _player
                .ObserveEveryValueChanged(player => Mathf.RoundToInt(player.DistancePassed) + 50)
                .Where(distancePassed => distancePassed % 50 == 0)
                .Subscribe(GenerateDistanceMarkers)
                .AddTo(_level.Disposables);

            Disposable.Create(() =>
            {
                _player.BestDistancePassed.Value = Math.Max(_player.BestDistancePassed.Value, _player.DistancePassed);
            }).AddTo(_player.Sequence.Disposables);

            if (_player.BestDistancePassed.Value > 0)
            {
                var bestDistanceMarker = _bestDistanceMarkerFactory.Create();
                bestDistanceMarker.SetDistance(_player.BestDistancePassed.Value);   
            }
        }

        private void GenerateDistanceMarkers(int horizontalPosition)
        {
            var distanceMarker = _distanceMarkerFactory.Create();
            distanceMarker.SetDistance(horizontalPosition);
        }
    }
}