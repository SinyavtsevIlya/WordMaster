using System.Collections;
using UniRx;
using Zenject;

namespace WordMaster
{
    public class LevelInstaller : Installer<LevelInstaller>
    {
        private readonly LevelSettings _settings;

        public LevelInstaller(LevelSettings settings)
        {
            _settings = settings;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<Level>().AsSingle();
            Container.BindInterfacesTo<ReactiveCollection<Letter>>().AsSingle();
            Container.BindInterfacesTo<ReactiveCollection<Word>>().AsSingle();
            Container.Bind<LevelSettings>().FromInstance(_settings).AsSingle();

            Container.BindRule<LevelGenerationRule>();
        }
    }
}