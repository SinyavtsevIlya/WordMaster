using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class LetterInstaller : Installer<LetterInstaller>
    {
        private readonly char _character;
        private readonly Vector2 _position;
        private readonly LetterSettings _settings;
        private IFlowTarget _flowTarget;

        public LetterInstaller(char character, Vector2 position, LetterSettings settings, IFlowTarget flowTarget)
        {
            _character = character;
            _position = position;
            _settings = settings;
            _flowTarget = flowTarget;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<Letter>().AsSingle();
            Container.Bind<CompositeDisposable>().AsSingle();
            Container.BindInstance(_character).AsSingle();
            Container.BindInstance(_position).AsSingle();
            Container.BindInstance(_settings.Size).AsSingle();
            
            Container.BindRule<LetterPresentRule>();

            Container.Bind<LetterView>()
                .FromComponentInNewPrefab(_settings.ViewPrefab)
                .AsSingle()
                .OnInstantiated((InjectContext context, LetterView view) => view.SetFlowTarget(_flowTarget));
            
            Container.BindSubKernel();
        }
    }
    
    public class LetterFactory : PlaceholderFactory<char, Vector2, Letter>
    {
    }
}