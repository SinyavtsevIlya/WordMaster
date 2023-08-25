using UniRx;
using Zenject;

namespace WordMaster
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Player>().AsSingle();
            Container.Bind<ReactiveCollection<Word>>().AsSingle();
        }
    }
}