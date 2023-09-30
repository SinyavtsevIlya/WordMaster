using Rules;
using Zenject;

namespace WordMaster
{
    public class GameFinishedPopupInstaller : Installer<GameFinishedPopupInstaller>
    {
        private readonly GameFinishedPopup _popup;

        public GameFinishedPopupInstaller(GameFinishedPopup popup)
        {
            _popup = popup;
        }
        
        public override void InstallBindings()
        {
            Container.BindRule<GameFinishedPopupPresenter>();
            
            Container.BindInterfacesTo<LanguageSelectionWidgetPresenter>()
                .AsCached()
                .WithArguments(_popup.LanguageSelectionWidget);
            
            Container.BindSubKernel();
        }
    }
}