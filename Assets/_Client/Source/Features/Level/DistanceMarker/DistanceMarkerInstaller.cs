using Zenject;

namespace WordMaster
{
    public class DistanceMarkerInstaller : Installer<DistanceMarkerInstaller>
    {
        private readonly LevelSettings _levelSettings;

        public DistanceMarkerInstaller(LevelSettings levelSettings)
        {
            _levelSettings = levelSettings;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<DistanceMarkerView>()
                .FromComponentInNewPrefab(_levelSettings.DistanceMarkerView)
                .AsSingle();
        }
    }
}