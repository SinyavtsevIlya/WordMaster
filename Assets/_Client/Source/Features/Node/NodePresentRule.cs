using Rules;
using UnityEngine;
using Zenject;
using UniRx;

namespace WordMaster
{
    public class NodePresentRule : IRule, IInitializable
    {
        private readonly Node _node;
        private readonly NodeView _nodeView;
        private readonly Material _material;

        public NodePresentRule(Node node, NodeView nodeView)
        {
            _node = node;
            _nodeView = nodeView;
        }
        
        public void Initialize()
        {
            _node.Letter.Position.Subscribe(_nodeView.SetPosition).AddTo(_node.Disposables);
            _nodeView.Appear();
            _nodeView.AddTo(_node.Disposables);
        }
    }
}