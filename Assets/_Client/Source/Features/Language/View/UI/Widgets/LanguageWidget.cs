using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WordMaster
{
    public class LanguageWidget : MonoBehaviour
    {
        [SerializeField] private Button _clickHandler;
        [SerializeField] private TMP_Text _languageLabel;
        [SerializeField] private Image _flagImage;
        
        [SerializeField] private string _languageCode;

        public IObservable<Unit> Clicked => _clickHandler.OnClickAsObservable();
        public string LanguageCode => _languageCode;
        public string LanguageName => _languageLabel.text;

        public void SetSelected(bool value) => gameObject.SetActive(!value);
    }
}