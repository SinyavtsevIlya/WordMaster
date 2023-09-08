using System;
using DG.Tweening;
using UnityEngine;

namespace WordMaster
{
    public class LetterView : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMPro.TMP_Text[] _textLayers;
        [SerializeField] private LetterViewSettings _settings;
        [SerializeField] private LetterViewSettings _pickedStateSettings;
        
        [SerializeField] private Color _matchedColor;
        [SerializeField] private Color _failedColor;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _duration;
        [SerializeField] private float _endValue;

        private IFlowTarget _flowTarget;
        
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetCharacter(char character)
        {
            foreach (var text in _textLayers) 
                text.SetText(character.ToString().ToUpper());
        }

        public void SetFlowTarget(IFlowTarget flowTarget) => _flowTarget = flowTarget;
        
        public void SetMatchState(bool isMatched)
        {
            foreach (var text in _textLayers) 
                text.color = isMatched ? _matchedColor : _failedColor;

            if (isMatched)
            {
                gameObject.transform
                    .DOMove(_flowTarget.GetTargetPosition(), 1f).SetEase(Ease.InExpo)
                    .OnComplete(() =>
                    {
                        _flowTarget.SetFlowCompleted();
                        Dispose();
                    });
            }
            else
            {
                gameObject.transform.DOScale(_endValue, _duration).SetEase(_ease).OnComplete(Dispose);
            }
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
        
        public void SetAsPicked()
        {
            Colorize(_pickedStateSettings);
        }

        public float GetWidth() => _textLayers[0].textBounds.size.x;
        
        private void Awake()
        {
            Colorize(_settings);
        }

        private void Colorize(LetterViewSettings settings)
        {
            for (var index = 0; index < _textLayers.Length; index++)
                ColorizeTextLayer(index, settings);
        }

        private void ColorizeTextLayer(int index, LetterViewSettings settings)
        {
            var normalizedValue = (float)index / _textLayers.Length;
            _textLayers[index].color = settings.EvaluateColor(normalizedValue);
        }

        public void RandomizeRotation()
        {
            transform.Rotate(Vector3.forward, UnityEngine.Random.Range(-7, 7));
        }
    }
}