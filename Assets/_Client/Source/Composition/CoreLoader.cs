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
            _trie ??= await new TrieLoader().Load();

            var saveLoad = new PlayerPrefsSaveLoad();
            
            var loadInfo = saveLoad.Load<PlayerSerializationState>("player");

            if (loadInfo.Status == LoadStatus.Failed)
                throw loadInfo.Exception;

            var state = loadInfo.Status == LoadStatus.FileNotExist ?
                new PlayerSerializationState(0, false) :
                loadInfo.Result;

            var alphobet = new Alphabet("абвгдеёжзиклмнопрстуфхцчшщыэюя", "ы", "арбуз");
            
            GetComponent<CoreInstaller>().Construct(saveLoad, _trie, state, alphobet);
            GetComponent<SceneContext>().Run();
        }
    }
}