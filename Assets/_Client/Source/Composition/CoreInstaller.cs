using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
        [SerializeField] private CameraSettings _cameraSettings;
        [SerializeField] private ScriptableObject[] _settings;

        public override void InstallBindings()
        {
            Container.Bind<Level>()
                .FromSubContainerResolve()
                .ByInstaller<LevelInstaller>()
                .AsSingle()
                .NonLazy();

            Container.Bind(typeof(Player), typeof(IFlowTarget))
                .FromSubContainerResolve()
                .ByInstaller<PlayerInstaller>()
                .AsSingle()
                .NonLazy();
            

            Container.Bind<Alphabet>().AsSingle()
                .WithArguments("абвгдеёжзиклмнопрстуфхцчшщыэюя", "ы");
            
            Container.Bind<Camera>().FromComponentInNewPrefab(_cameraSettings.Prefab).AsSingle();
            
            foreach (var s in _settings) 
                Container.Bind(s.GetType()).FromInstance(s).AsSingle();
        }
    }
}