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
                .WithKernel()
                .AsSingle()
                .NonLazy();

            Container.BindFactory<char, Vector2, Letter, LetterFactory>()
                .FromSubContainerResolve()
                .ByInstaller<LetterInstaller>()
                .AsSingle();

            Container.Bind<Player>()
                .FromSubContainerResolve()
                .ByInstaller<PlayerInstaller>()
                .WithKernel()
                .AsSingle();

            Container.BindInstance(_levelSettings).AsSingle();
            Container.BindInstance(_letterSettings).AsSingle();
            Container.BindInstance(_nodeSettings).AsSingle();
        }
    }
}