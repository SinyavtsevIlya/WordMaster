using UnityEngine;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create LetterSettings", fileName = "LetterSettings", order = 0)]
    public class LetterSettings : ScriptableObject
    {
        [SerializeField] private LetterView _viewPrefab;
        
        public float Size;
        public LetterView ViewPrefab => _viewPrefab;
    }
}