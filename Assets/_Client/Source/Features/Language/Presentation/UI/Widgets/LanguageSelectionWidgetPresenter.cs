using System.Linq;
using Rules;
using UniRx;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace WordMaster
{
    public class LanguageSelectionWidgetPresenter : IRule
    {
        private readonly LanguageSelectionWidget _languageSelectionWidget;
        private readonly RestartRequest _restartRequest;
        private readonly Locale _selectedLocale;

        public LanguageSelectionWidgetPresenter(LanguageSelectionWidget languageSelectionWidget, RestartRequest restartRequest, Locale selectedLocale)
        {
            _languageSelectionWidget = languageSelectionWidget;
            _restartRequest = restartRequest;
            _selectedLocale = selectedLocale;
        }
        
        public void Initialize()
        {
            var localization = LocalizationSettings.Instance;

            var widget = _languageSelectionWidget.LanguageWidgets.First(w =>
                w.LanguageCode == _selectedLocale.LocaleName);
            
            _languageSelectionWidget.Initialize(widget);

            _languageSelectionWidget.Clicked
                .Subscribe(_ => _languageSelectionWidget.DisplayDropDown(!_languageSelectionWidget.IsDropdownVisible))
                .AddTo(_languageSelectionWidget);

            foreach (var languageWidget in _languageSelectionWidget.LanguageWidgets)
            {
                languageWidget.Clicked
                    .Subscribe(_ => _languageSelectionWidget.SelectedWidget.Value = languageWidget)
                    .AddTo(languageWidget);
            }

            _languageSelectionWidget.SelectedWidget.Subscribe(languageWidget =>
            {
                var locale = localization.GetAvailableLocales().Locales
                    .First(l => l.LocaleName == languageWidget.LanguageCode);

                localization.SetSelectedLocale(locale);
                
                _restartRequest.Value.OnNext(Unit.Default);
            });
        }
    }
}