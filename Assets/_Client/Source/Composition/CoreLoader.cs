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
            
            var loadInfo = SaveLoadOps.Load<PlayerSerializationState>("player");

            if (loadInfo.Status == LoadStatus.Failed)
                throw loadInfo.Exception;

            var state = loadInfo.Status == LoadStatus.FileNotExist ?
                new PlayerSerializationState(0) :
                loadInfo.Result;
            
            GetComponent<CoreInstaller>().Construct(_trie, state);
            GetComponent<SceneContext>().Run();
        }
    }
}