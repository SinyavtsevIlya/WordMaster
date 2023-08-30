using Zenject;

namespace WordMaster
{
    public class ScoreInstaller : Installer<ScoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Score>().AsSingle();
            Container.BindRule<ScoreWidgetPresenter>();
            Container.Bind<Binding<Score, ScoreWidget>>().AsSingle();
        }
    }
}