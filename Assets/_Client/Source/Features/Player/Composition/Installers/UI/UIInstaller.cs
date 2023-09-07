using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class UIInstaller : Installer<UIInstaller>
    {
        private UISettings _uiSettings;

        public UIInstaller(UISettings uiSettings)
        {
            _uiSettings = uiSettings;
        }

        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromComponentInNewPrefab(_uiSettings.CanvasPrefab).AsSingle();

            Container.Bind<CoreScreen>()
                .FromSubContainerResolve()
                .ByInstaller<ScreensInstaller>()
                .AsSingle()
                .NonLazy();
        }
    }
}