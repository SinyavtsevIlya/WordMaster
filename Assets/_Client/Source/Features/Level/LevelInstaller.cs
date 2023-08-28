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
            
            Container.BindRule<LevelGenerationRule>();
            Container.BindRule<ScrollCameraRule>();
            
            Container.BindSubKernel();
        }
    }
}