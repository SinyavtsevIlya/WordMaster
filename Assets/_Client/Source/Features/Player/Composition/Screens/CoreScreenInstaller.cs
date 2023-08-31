using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class CoreScreenInstaller : Installer<CoreScreenInstaller>
    {
        private readonly UISettings _uiSettings;
        private readonly Player _player;
        private readonly Canvas _canvas;

        public CoreScreenInstaller(UISettings uiSettings, Player player, Canvas canvas)
        {
            _uiSettings = uiSettings;
            _player = player;
            _canvas = canvas;
        }

        public override void InstallBindings()
        {
            Container.Bind<CoreScreen>()
                .FromComponentInNewPrefab(_uiSettings.CoreScreen)
                .UnderTransform(_canvas.transform)
                .AsSingle();

            Container.BindInstance(_player.Score).AsSingle();
            Container.BindInstance(_player.Disposables).AsSingle();

            Container.BindRule<CoreScreenPresenter>();

            Container.BindSubKernel();
        }
    }
}