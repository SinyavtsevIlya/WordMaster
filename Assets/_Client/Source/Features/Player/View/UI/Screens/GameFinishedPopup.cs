using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace WordMaster
{
    public class GameFinishedPopup : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        public Button ResumeGameButton;
        public Button RestartGameButton;

        [SerializeField] private TMP_Text _bestDistanceLabel;
        [SerializeField] private TMP_Text _currentDistanceLabel;

        [SerializeField] private LocalizedString _unitsSufix;
        
        [field: SerializeField] public LanguageSelectionWidget LanguageSelectionWidget { get; private set; }

        public void DisplayCurrentDistance(float currentDistance)
        {
            _currentDistanceLabel.SetText($"{(int)currentDistance} {_unitsSufix.GetLocalizedStringAsync().Result}");
        }

        public void DisplayBestDistance(float bestDistance)
        {
            _bestDistanceLabel.SetText($"{(int)bestDistance} {_unitsSufix.GetLocalizedStringAsync().Result}");
        }

        public void Show()
        {
            if (IsAnimatable)
                _animator.SetTrigger("Show");
            else
                DisplayImmediately(true);
        }
        
        public void Hide()
        {
            if (IsAnimatable)
                _animator.SetTrigger("Hide");
            else
                DisplayImmediately(false);
        }

        private void DisplayImmediately(bool value) => _canvasGroup.alpha = value ? 1f : default; 
        
        private static bool IsAnimatable => !Application.isMobilePlatform;

        private void Awake()
        {
            _animator.enabled = IsAnimatable;
            
            if (!IsAnimatable)
                DisplayImmediately(false);
        }
    }
}