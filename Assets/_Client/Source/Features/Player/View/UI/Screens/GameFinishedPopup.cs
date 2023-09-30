using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace WordMaster
{
    public class GameFinishedPopup : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public Button ResumeGameButton;
        public Button RestartGameButton;

        [SerializeField] private TMP_Text _bestDistanceLabel;
        [SerializeField] private TMP_Text _currentDistanceLabel;

        [SerializeField] private LocalizedString _unitsSufix;
        
        [field: SerializeField] public LanguageSelectionWidget LanguageSelectionWidget { get; private set; }

        public void DisplayCurrentDistance(float currentDistance)
        {
            _currentDistanceLabel.SetText($"{(int)currentDistance} {_unitsSufix.GetLocalizedString()}");
        }

        public void DisplayBestDistance(float bestDistance)
        {
            _bestDistanceLabel.SetText($"{(int)bestDistance} {_unitsSufix.GetLocalizedString()}");
        }

        public void Show()
        {
            _animator.SetTrigger("Show");
        }

        public void Hide()
        {
            _animator.SetTrigger("Hide");
        }
    }
}