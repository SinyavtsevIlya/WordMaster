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
            Container.BindInterfacesTo<ReactiveCollection<Letter>>().AsSingle();
            Container.BindInterfacesTo<ReactiveCollection<Word>>().AsSingle();
            Container.BindRule<LevelGenerationRule>();
            
            Container.BindFactory<char, Vector2, Letter, LetterFactory>()
                .FromSubContainerResolve()
                .ByInstaller<LetterInstaller>()
                .AsSingle();
            
            Container.BindSubKernel();
        }
    }
}