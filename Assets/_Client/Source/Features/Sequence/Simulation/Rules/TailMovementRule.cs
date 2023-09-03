using System;
using System.Collections.Generic;
using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class TailMovementRule : IRule, IInitializable
    {
        private readonly Sequence _sequence;
        private readonly NodeSettings _nodeSettings;
        private readonly CompositeDisposable _disposables;

        public TailMovementRule(Sequence sequence, NodeSettings nodeSettings, CompositeDisposable disposables)
        {
            _sequence = sequence;
            _nodeSettings = nodeSettings;
            _disposables = disposables;
        }

        public void Initialize()
        {
            _sequence.Value.ObserveAdd().Subscribe(addEvent =>
            {
                var headDisposables = new CompositeDisposable();

                _sequence.Head.Value.Letter.Position.Pairwise().Subscribe(headPositionPair =>
                {
                    var deltaMagnitude = (headPositionPair.Current - headPositionPair.Previous).magnitude;

                    for (var index = 0; index < _sequence.Value.Count - 1; index++)
                    {
                        var node = _sequence.Value[index];
                        var upcomingNode = _sequence.Value[index + 1];

                        var delta = upcomingNode.Letter.Position.Value - node.Letter.Position.Value;
                        
                        var currentDelta =
                            delta.normalized *
                            deltaMagnitude;

                        if (delta.magnitude > (node.Letter.Width + upcomingNode.Letter.Width) / 2f + _nodeSettings.LettersSpacing)
                        {
                            node.Letter.Position.Value += currentDelta;   
                        }
                    }
                }).AddTo(headDisposables);
            }).AddTo(_disposables);
        }
    }
}