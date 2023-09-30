using System;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WordMaster
{
    public class LanguageSelectionWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _selectedLanguageLabel;
        [SerializeField] private Button _clickHandler;
        [SerializeField] private GameObject _dropdownRoot;
        private LanguageWidget[] _languageWidgets;

        public ReactiveProperty<LanguageWidget> SelectedWidget { get; private set; }

        private void Awake()
        {
            _languageWidgets = _dropdownRoot.GetComponentsInChildren<LanguageWidget>(true);
        }

        public void Initialize(LanguageWidget initialLanguageWidget)
        {
            SelectedWidget = new ReactiveProperty<LanguageWidget>(initialLanguageWidget);
            SelectedWidget.Value.SetSelected(true);

            SelectedWidget.Pairwise().Subscribe(pair =>
            {
                pair.Current.SetSelected(true);
                pair.Previous.SetSelected(false);
            }).AddTo(this);
            
            SelectedWidget
                .Subscribe(languageWidget =>
                {
                    _selectedLanguageLabel.SetText(languageWidget.LanguageName);
                    DisplayDropDown(false);
                })
                .AddTo(this);
        }

        public IObservable<Unit> Clicked => _clickHandler.OnClickAsObservable();

        public LanguageWidget[] LanguageWidgets => _languageWidgets;

        public void DisplayDropDown(bool value)
        {
            _dropdownRoot.SetActive(value);
        }

        public bool IsDropdownVisible => _dropdownRoot.activeSelf;
    }
}