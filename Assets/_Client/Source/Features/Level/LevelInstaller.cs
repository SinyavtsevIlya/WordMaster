using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class LevelInstaller : Installer<LevelInstaller>
    {
        private PlayerSerializationState _playerSerializationState;

        public LevelInstaller(PlayerSerializationState playerSerializationState)
        {
            _playerSerializationState = playerSerializationState;
        }

        public override void InstallBindings()
        {
            Container.Bind<Level>().AsSingle();
            Container.Bind<CompositeDisposable>().AsSingle();
            Container.BindInterfacesTo<ReactiveCollection<Letter>>().AsSingle();

            Container.BindFactory<char, Vector2, Letter, LetterFactory>()
                .FromSubContainerResolve()
                .ByInstaller<LetterInstaller>()
                .AsSingle();

            Container.BindFactory<DistanceMarkerView, DistanceMarkerFactory>()
                .FromSubContainerResolve()
                .ByInstaller<DistanceMarkerInstaller>()
                .AsSingle();
            
            Container.BindFactory<BestDistanceMarkerView, BestDistanceMarkerFactory>()
                .FromSubContainerResolve()
                .ByInstaller<BestDistanceMarkerInstaller>()
                .AsSingle();

            Container.BindRule<LevelGenerationRule>();
            Container.BindRule<LetterCullingRule>();

            if (!_playerSerializationState.IsTutorialShown)
            {
                Container.Bind<SpawnTutorialWordRule>()
                    .FromSubContainerResolve()
                    .ByInstaller<TutorialInstaller>()
                    .AsSingle()
                    .NonLazy();
            }

            Container.BindRule<SidePropsGenerationRule>();
            Container.BindRule<DistanceMarkerGenerationRule>();
            Container.BindRule<RestartLevelOnZeroEnergyRule>();
            Container.BindRule<ScrollCameraRule>();
            
            Container.BindSubKernel();
        }
    }
}