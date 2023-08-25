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
                .WithArguments(_levelSettings);

            Container.BindFactory<char, Vector2, Letter, LetterFactory>()
                .FromSubContainerResolve()
                .ByInstaller<LetterInstaller>()
                .AsSingle()
                .WithArguments(_letterSettings);

            Container.Bind<Player>()
                .FromSubContainerResolve()
                .ByInstaller<PlayerInstaller>()
                .WithKernel()
                .AsSingle();

            Container.BindInstance(_nodeSettings).AsSingle();
        }
    }
}