using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class CoreScreenInstaller : Installer<CoreScreenInstaller>
    {
        private readonly CoreScreen _coreScreen;
        private readonly CompositeDisposable _disposables;
        private readonly Score _score;
        private readonly Energy _energy;

        public CoreScreenInstaller(CoreScreen coreScreen, CompositeDisposable disposables, Score score, Energy energy)
        {
            _coreScreen = coreScreen;
            _disposables = disposables;
            _score = score;
            _energy = energy;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_disposables).AsSingle();

            Container.BindInterfacesTo<ScoreWidgetPresenter>()
                .AsCached()
                .WithArguments(_score, _coreScreen.ScoreWidget);
            
            Container.BindInterfacesTo<EnergyWidgetPresenter>()
                .AsCached()
                .WithArguments(_energy, _coreScreen.EnergyWidget);
            
            Container.BindRule<CoreScreenPresenter>();
            
            Container.Bind<IFlowTarget>().FromInstance(_coreScreen.EnergyWidget).AsSingle();

            Container.BindSubKernel();
        }
    }
}