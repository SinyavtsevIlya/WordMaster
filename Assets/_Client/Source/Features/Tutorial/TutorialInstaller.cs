using Rules;
using WordMaster;
using Zenject;

namespace WordMaster
{
    public class TutorialInstaller : Installer<TutorialInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindRule<SpawnTutorialWordRule>();
            Container.BindSubKernel();
        }
    }
}