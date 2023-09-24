using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WordMaster
{
    public class EnergyWidget : MonoBehaviour, IFlowTarget
    {
        private const float Duration = .5f;

        [SerializeField] private Image _progressBar;
        [SerializeField] private Image _flashlight;
        [SerializeField] private Transform _flowTarget;

        [SerializeField] private Gradient _colorByFillAmount;
        [SerializeField] private AnimationCurve _amplitudeByFillAmount;

        private float _maxValue;
        private float _initialWidth;

        private bool _isInitialized;

        private Tweener _tweener;
        private Tweener _flashlightTweener;

        private void Awake()
        {
            _initialWidth = _progressBar.rectTransform.sizeDelta.x;
        }
        
        public void SetEnergy(float current, float maxValue, float duration)
        {
            if (maxValue == 0f)
                throw new ArgumentException("max energy can't be zero", nameof(maxValue));

            current = Mathf.Clamp(current, 0f, maxValue);
            
            _maxValue = maxValue;
            
            if (!_isInitialized)
            {
                duration = 0f;
                _isInitialized = true;
            }
            
            SetFillAmount(current, duration);
            AnimateFlashlight(current);
        }

        public Transform GetTarget() => _flowTarget;

        public void SetFlowCompleted()
        {
            _tweener.Play();
            
            var sequence = DOTween.Sequence();
            sequence
                .Append(_flowTarget.DOScale(Vector3.one * 1.2f, .1f).SetEase(Ease.OutSine))
                .Append(_flowTarget.DOScale(Vector3.one, .1f).SetEase(Ease.InSine));
        }

        private void SetFillAmount(float value, float duration)
        {
            var endValue = new Vector2(_initialWidth * (value / _maxValue), _progressBar.rectTransform.sizeDelta.y);
            _progressBar.color = _colorByFillAmount.Evaluate(value / _maxValue);
            
            if (duration == 0f)
            {
                if (_tweener != null && _tweener.IsPlaying())
                    return;
                
                _progressBar.rectTransform.sizeDelta = endValue;
            }
            else
            {
                DOTween.Kill(_progressBar.rectTransform);
                _tweener = _progressBar.rectTransform.DOSizeDelta(endValue, duration)
                    .SetEase(Ease.OutExpo)
                    .SetUpdate(true)
                    .Pause();
            }
        }

        private void AnimateFlashlight(float value)
        {
            if (value / _maxValue > .25f)
            {
                _flashlightTweener?.Kill();
                _flashlightTweener = null;
            }
            else
            {
                _flashlightTweener ??= _flashlight.transform.DOPunchScale(Vector3.one * 0.2f, 1f, vibrato: 0).SetLoops(-1)
                    .SetEase(Ease.OutExpo);
            }
        }

        public void Dispose()
        {
            _isInitialized = false;
        }
    }
}