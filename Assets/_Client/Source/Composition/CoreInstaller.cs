using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
        [SerializeField] private LevelSettings _levelSettings;
        [SerializeField] private LetterSettings _letterSettings;
        [SerializeField] private NodeSettings _nodeSettings;

        public override void InstallBindings()
        {
            Container.Bind<Level>()
                .FromSubContainerResolve()
                .ByInstaller<LevelInstaller>()
                .AsSingle()
                .NonLazy();

            Container.Bind<Player>()
                .FromSubContainerResolve()
                .ByInstaller<PlayerInstaller>()
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_levelSettings).AsSingle();
            Container.BindInstance(_letterSettings).AsSingle();
            Container.BindInstance(_nodeSettings).AsSingle();
        }
    }
}