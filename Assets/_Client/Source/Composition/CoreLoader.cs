using System;
using Plugins.Nanory.SaveLoad;
using UnityEngine;
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
                new PlayerSerializationState(0, false) :
                loadInfo.Result;

            var language = SystemLanguage.English;

            var alphabet = GetAlphabet(language);
            
            _trie ??= await new TrieLoader(language).Load();
            
            GetComponent<CoreInstaller>().Construct(saveLoad, _trie, state, alphabet);
            GetComponent<SceneContext>().Run();
        }

        private Alphabet GetAlphabet(SystemLanguage language)
        {
            return language == SystemLanguage.Russian ?
                new Alphabet("абвгдеёжзиклмнопрстуфхцчшщыэюя", "арбуз", "ы") 
                : new Alphabet("abcdefghigklmnopqrstuvwxyz", "apple", string.Empty);
        }
    }
}