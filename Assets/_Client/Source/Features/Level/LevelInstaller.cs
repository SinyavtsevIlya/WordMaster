using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class LevelInstaller : Installer<LevelInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Level>().AsSingle();
            Container.Bind<CompositeDisposable>().AsSingle();
            Container.BindInterfacesTo<ReactiveCollection<Letter>>().AsSingle();
            Container.BindInterfacesTo<ReactiveCollection<Word>>().AsSingle();

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
            Container.BindRule<DistanceMarkerGenerationRule>();
            Container.BindRule<RestartLevelOnZeroEnergyRule>();
            Container.BindRule<ScrollCameraRule>();
            
            Container.BindSubKernel();
        }
    }
}