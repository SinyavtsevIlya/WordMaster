using System.Collections;
using UniRx;
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
            
            Container.BindSubKernel();
        }
    }
}