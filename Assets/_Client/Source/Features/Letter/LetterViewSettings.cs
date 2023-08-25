using UnityEngine;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create LetterViewSettings", fileName = "LetterViewSettings", order = 0)]
    public class LetterViewSettings : ScriptableObject
    {
        [SerializeField] private Gradient _gradient;

        public Color EvaluateColor(float normalizedValue) => _gradient.Evaluate(normalizedValue);
    }
}