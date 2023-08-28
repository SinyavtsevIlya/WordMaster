using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CompositeDisposable>().AsSingle();
            
            Container.Bind<Player>().AsSingle();

            Container.BindInterfacesTo<ReactiveCollection<Word>>().AsSingle();

            Container.Bind<Sequence>()
                .FromSubContainerResolve()
                .ByInstaller<SequenceInstaller>()
                .AsSingle();

            
            Container.BindSubKernel();
        }
    }
}