using UnityEngine;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create LetterSettings", fileName = "LetterSettings", order = 0)]
    public class LetterSettings : ScriptableObject
    {
        [SerializeField] private LetterView _viewPrefab;
        [SerializeField] private LetterViewSettings[] _lettersByStages;
        
        public float Size;
        public LetterView ViewPrefab => _viewPrefab;
        public LetterViewSettings GetViewSettings(int stage) => _lettersByStages[stage % _lettersByStages.Length];
    }
}