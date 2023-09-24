using Rules;
using Zenject;

namespace WordMaster
{
    public class GameFinishedPopupInstaller : Installer<GameFinishedPopupInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindRule<GameFinishedPopupPresenter>();
            
            Container.BindSubKernel();
        }
    }
}