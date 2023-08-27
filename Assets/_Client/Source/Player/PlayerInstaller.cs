using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        private readonly CameraSettings _cameraSettings;

        public PlayerInstaller(CameraSettings cameraSettings)
        {
            _cameraSettings = cameraSettings;
        }

        public override void InstallBindings()
        {
            Container.Bind<CompositeDisposable>().AsSingle();
            
            Container.Bind<Player>().AsSingle();
            Container.Bind<Camera>().FromComponentInNewPrefab(_cameraSettings.Prefab).AsSingle();

            Container.BindInterfacesTo<ReactiveCollection<Word>>().AsSingle();

            Container.Bind<Sequence>()
                .FromSubContainerResolve()
                .ByInstaller<SequenceInstaller>()
                .AsSingle();

            Container.BindRule<ScrollCameraRule>();
            
            Container.BindSubKernel();
        }
    }
}