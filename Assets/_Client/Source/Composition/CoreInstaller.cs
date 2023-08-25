using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
        [SerializeField] private LevelSettings _levelSettings;

        public override void InstallBindings()
        {
            Container.Bind<Level>()
                .FromSubContainerResolve()
                .ByInstaller<LevelInstaller>()
                .AsSingle()
                .WithArguments(_levelSettings);
            
            
        }
    }
}