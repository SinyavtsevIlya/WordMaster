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
        [SerializeField] private SpriteRenderer[] _sprites;

        [SerializeField] private Color _matchedColor;
        [SerializeField] private Color _failedColor;

        [SerializeField] private NodeViewSettings _settings;

        public void Appear()
        {
            _root.localScale = Vector3.one * .3f; 
            _root.DOScale(1f, _settings.AppearDuration).SetEase(_settings.AppearEase);
        }
        
        public void SetPosition(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }

        public void SetMatchState(bool isMatched)
        {
            foreach (var spriteRenderer in _sprites)
            {
                spriteRenderer.color = isMatched ? _settings.MatchedColor : _settings.FailedColor;
                spriteRenderer.DOFade(0f, _settings.ColorDuration).SetDelay(.1f);
            }
            _root.DOScale(_settings.ScaleEndValue, _settings.ScaleDuration).SetEase(_settings.ScaleEase).OnComplete(Dispose);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}