using System;
using System.Linq;
using System.Threading.Tasks;
using Plugins.Nanory.SaveLoad;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using Zenject;

namespace WordMaster
{
    public class CoreLoader : MonoBehaviour
    {
        private static Trie _trie;
        
        private async void Start()
        {
            var saveLoad = new PlayerPrefsSaveLoad();

            var loadInfo = saveLoad.Load<PlayerSerializationState>("player");

            if (loadInfo.Status == LoadStatus.Failed)
                throw loadInfo.Exception;

            var state = loadInfo.Status == LoadStatus.FileNotExist ?
                new PlayerSerializationState(0, false, YandexGamesSdk.GetUserLanguage()) :
                loadInfo.Result;

            var locale = await SyncLocalization(state.Language);

            var alphabet = GetAlphabet(state.Language);

            await LoadTrie(state);

            GetComponent<CoreInstaller>().Construct(saveLoad, _trie, state, alphabet, locale);
            GetComponent<SceneContext>().Run();
        }

        private static async Task LoadTrie(PlayerSerializationState state)
        {
            if (_trie?.Language != state.Language || _trie == null)
                _trie = await new TrieLoader(state.Language).Load();
        }

        private static async Task<Locale> SyncLocalization(SystemLanguage language)
        {
            var localization = LocalizationSettings.Instance;

            await localization.GetInitializationOperation().Task;
            
            var locales = localization.GetAvailableLocales().Locales;
            var locale = locales.First(l => language == SystemLanguage.Russian
                ? l.LocaleName == "Russian (ru)"
                : l.LocaleName == "English (en)");

            localization.SetSelectedLocale(locale);

            return locale;
        }

        private Alphabet GetAlphabet(SystemLanguage language)
        {
            return language == SystemLanguage.Russian ?
                new Alphabet("абвгдеёжзиклмнопрстуфхцчшщыэюя", "арбуз", "ы") 
                : new Alphabet("abcdefghigklmnopqrstuvwxyz", "apple", string.Empty);
        }
    }
}