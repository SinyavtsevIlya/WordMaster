using System;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WordMaster
{
    public class NodeView : MonoBehaviour, IDisposable
    {
        [SerializeField] private Transform _root;
        [SerializeField] private Ease _ease;
        [SerializeField] private SpriteRenderer _sprite;

        [SerializeField] private Color _matchedColor;
        [SerializeField] private Color _failedColor;

        public void Appear()
        {
            _root.localScale = Vector3.one * .3f; 
            _root.DOScale(1f, .8f).SetEase(_ease);
        }
        
        public void SetPosition(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }

        public void SetMatchState(bool isMatched)
        {
            _sprite.color = isMatched ? _matchedColor : _failedColor;
            _root.DOScale(0f, 1f).SetEase(_ease).OnComplete(Dispose);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}