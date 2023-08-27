﻿using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class TailMovementRule : IRule, IInitializable
    {
        private readonly Node _node;

        public TailMovementRule(Node node)
        {
            _node = node;
        }

        public void Initialize()
        {
            _node.Prev
                .Where(prev => prev != null)
                .Subscribe(prev =>
                {
                    Observable.EveryUpdate()
                        .Subscribe(_ =>
                        {
                            var position = prev.Letter.Position.Value;
                            var t = Time.deltaTime * _node.Settings.VerticalMovementSmoothness;
                            var y = Mathf.Lerp(_node.Letter.Position.Value.y, position.y, t);
                            var x = position.x - _node.Letter.Radius * 2;
                            _node.Letter.Position.Value = new Vector2(x, y);
                        })
                        .AddTo(prev.Disposables);
                }).AddTo(_node.Disposables);
        }
    }
}