using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class LetterInstaller : Installer<LetterInstaller>
    {
        private readonly LetterSettings _settings;

        public LetterInstaller(LetterSettings settings)
        {
            _settings = settings;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<Letter>().AsSingle();
            Container.Bind<CompositeDisposable>().AsSingle();
            Container.BindInstance(_settings.Size).AsSingle();
            
            Container.BindRule<LetterPresentRule>();

            Container.Bind<LetterView>().FromComponentInNewPrefab(_settings.ViewPrefab);
        }
    }
    
    public class LetterFactory : PlaceholderFactory<char, Vector2, Letter>
    {
    }
}