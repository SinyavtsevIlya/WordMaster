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
        
        public void Appear()
        {
            _root.localScale = Vector3.one * .3f; 
            _root.DOScale(1f, .8f).SetEase(_ease);
        }
        
        public void SetPosition(Vector2 position)
        {
            _root.position = new Vector3(position.x, position.y, _root.position.z);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}