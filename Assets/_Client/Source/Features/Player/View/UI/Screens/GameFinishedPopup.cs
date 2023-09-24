using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WordMaster
{
    public class GameFinishedPopup : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public Button ResumeGameButton;
        public Button RestartGameButton;

        public TMP_Text BestDistanceLabel;
        public TMP_Text CurrentDistanceLabel;
        
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