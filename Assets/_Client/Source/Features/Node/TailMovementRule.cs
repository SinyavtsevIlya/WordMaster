using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class TailMovementRule
    {
        public TailMovementRule(Node node)
        {
            node.Prev.Subscribe(prev =>
            {
                Observable.EveryUpdate()
                    .Subscribe(_ =>
                    {
                        var position = prev.Letter.Position.Value;
                        var t = Time.deltaTime * node.Settings.VerticalMovementSmoothness;
                        var y = Mathf.Lerp(node.Letter.Position.Value.y, position.y, t);
                        node.Letter.Position.Value = new Vector2(position.x, y);
                    })
                    .AddTo(prev.Disposables);
            }).AddTo(node.Disposables);
        }
    }
}