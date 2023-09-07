using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class ScreensInstaller : Installer<ScreensInstaller>
    {
        private readonly Canvas _canvas;
        private readonly UISettings _settings;

        public ScreensInstaller(Canvas canvas, UISettings settings)
        {
            _canvas = canvas;
            _settings = settings;
        }
        
        public override void InstallBindings()
        {
            foreach (var screen in _settings.Screens)
            {
                Container.Bind(screen.GetType())
                    .FromComponentInNewPrefab(screen)
                    .UnderTransform(_canvas.transform)
                    .AsSingle();
            }
        }
    }
}