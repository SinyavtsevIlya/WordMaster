using Zenject;

namespace WordMaster
{
    public class BestDistanceMarkerInstaller : Installer<DistanceMarkerInstaller>
    {
        private readonly LevelSettings _levelSettings;

        public BestDistanceMarkerInstaller(LevelSettings levelSettings)
        {
            _levelSettings = levelSettings;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<BestDistanceMarkerView>()
                .FromComponentInNewPrefab(_levelSettings.BestDistanceMarkerView)
                .AsSingle();
        }
    }
}