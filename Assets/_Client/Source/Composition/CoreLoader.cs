using Plugins.Nanory.SaveLoad;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class CoreLoader : MonoBehaviour
    {
        private void Start()
        {
            var loadInfo = SaveLoadOps.Load<PlayerSerializationState>("player");

            if (loadInfo.Status == LoadStatus.Failed)
                throw loadInfo.Exception;

            var state = loadInfo.Status == LoadStatus.FileNotExist ?
                new PlayerSerializationState(0) :
                loadInfo.Result;
            
            GetComponent<CoreInstaller>().SetPlayerSerializationState(state);
            GetComponent<SceneContext>().Run();
        }
    }
}