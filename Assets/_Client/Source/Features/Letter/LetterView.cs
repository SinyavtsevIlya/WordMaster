using UnityEngine;

namespace WordMaster
{
    public class LetterView : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text[] _textLayers;
        [SerializeField] private LetterViewSettings _settings;
        
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetCharacter(char character)
        {
            foreach (var text in _textLayers) 
                text.SetText(character.ToString());
        }

        private void Awake()
        {
            for (var index = 0; index < _textLayers.Length; index++) 
                ColorizeTextLayer(index);
        }

        private void ColorizeTextLayer(int index)
        {
            var normalizedValue = (float)index / _textLayers.Length;
            _textLayers[index].color = _settings.EvaluateColor(normalizedValue);
        }

        public void PlayCompletion()
        {
            Destroy(gameObject);
        }
    }
}