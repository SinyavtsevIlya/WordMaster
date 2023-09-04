using UnityEngine;

namespace WordMaster
{
    public class LetterView : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text[] _textLayers;
        [SerializeField] private LetterViewSettings _settings;
        [SerializeField] private LetterViewSettings _pickedStateSettings;
        
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetCharacter(char character)
        {
            foreach (var text in _textLayers) 
                text.SetText(character.ToString());
        }

        public void PlayCompletion()
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
    }
}