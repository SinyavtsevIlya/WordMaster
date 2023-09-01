using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class CoreScreenInstaller : Installer<CoreScreenInstaller>
    {
        private readonly CoreScreen _coreScreen;
        private readonly Player _player;
        private readonly Canvas _canvas;

        public CoreScreenInstaller(CoreScreen coreScreen, Player player, Canvas canvas)
        {
            _player = player;
            _canvas = canvas;
            _coreScreen = coreScreen;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_player.Score).AsSingle();
            Container.BindInstance(_player.Disposables).AsSingle();

            Container.BindInterfacesTo<ScoreWidgetPresenter>()
                .AsCached()
                .WithArguments(_player.Score, _coreScreen.ScoreWidget);
            
            Container.BindRule<EnergyWidgetPresenter>();
            
            Container.BindRule<CoreScreenPresenter>();

            Container.BindInstance(0).AsSingle();

            Container.BindSubKernel();
        }
    }
}