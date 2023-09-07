using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        private readonly EnergySettings _energySettings;

        public PlayerInstaller(EnergySettings energySettings)
        {
            _energySettings = energySettings;
        }

        public override void InstallBindings()
        {
            Container.Bind<CompositeDisposable>().AsSingle();
            
            Container.Bind<Player>().AsSingle();
            
            Container.Bind<Score>().AsSingle();
            Container.Bind<Energy>().AsSingle()
                .WithArguments(
                    new ReactiveProperty<float>(_energySettings.InitialEnergyAmount),
                    new ReactiveProperty<float>(_energySettings.InitialEnergyAmount));

            Container.Bind<CoreScreen>()
                .FromSubContainerResolve()
                .ByInstaller<UIInstaller>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind(typeof(CoreScreenPresenter), typeof(IFlowTarget))
                .FromSubContainerResolve()
                .ByInstaller<CoreScreenInstaller>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesTo<ReactiveCollection<Word>>().AsSingle();

            Container.Bind<Sequence>()
                .FromSubContainerResolve()
                .ByInstaller<SequenceInstaller>()
                .AsSingle();

            Container.BindRule<AddScoreRule>();
            Container.BindRule<AddEnergyForScoreRule>();
            Container.BindRule<LossEnergyRule>();

            Container.BindSubKernel();
        }
    }
}