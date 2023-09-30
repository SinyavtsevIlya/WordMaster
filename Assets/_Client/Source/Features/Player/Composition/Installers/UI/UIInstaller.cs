using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class UIInstaller : Installer<UIInstaller>
    {
        private readonly UISettings _uiSettings;
        private readonly Camera _camera;

        public UIInstaller(UISettings uiSettings, Camera camera)
        {
            _uiSettings = uiSettings;
            _camera = camera;
        }

        public override void InstallBindings()
        {
            var canvas = Application.isMobilePlatform 
                ? _uiSettings.CanvasPrefab 
                : _uiSettings.DesktopCanvasPrefab;
            
            Container.Bind<Canvas>()
                .FromComponentInNewPrefab(canvas)
                .AsSingle()
                .OnInstantiated((InjectContext ctx, Canvas canvas) =>
                {
                    canvas.worldCamera = _camera;
                    canvas.planeDistance = 12f;
                });

            Container.Bind(typeof(CoreScreen), typeof(GameFinishedPopup))
                .FromSubContainerResolve()
                .ByInstaller<ScreensInstaller>()
                .AsSingle()
                .NonLazy();
        }
    }
}