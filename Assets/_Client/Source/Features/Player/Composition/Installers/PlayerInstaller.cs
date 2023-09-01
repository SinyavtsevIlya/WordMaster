using Rules;
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
            
            Container.Bind<Score>().AsSingle();
            Container.Bind<Energy>().AsSingle().WithArguments(new ReactiveProperty<int>(), new ReactiveProperty<int>());

            Container.Bind<Canvas>()
                .FromSubContainerResolve()
                .ByInstaller<UIInstaller>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesTo<ReactiveCollection<Word>>().AsSingle();

            Container.Bind<Sequence>()
                .FromSubContainerResolve()
                .ByInstaller<SequenceInstaller>()
                .AsSingle();
            
            Container.BindRule<AddScoreRule>();

            Container.BindSubKernel();
        }
    }
}