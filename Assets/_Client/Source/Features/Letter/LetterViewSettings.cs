using UnityEngine;

namespace WordMaster
{
    public class LetterViewSettings : ScriptableObject
    {
        [SerializeField] private Gradient _gradient;

        public Color EvaluateColor(float normalizedValue) => _gradient.Evaluate(normalizedValue);
    }
}