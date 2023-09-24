using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class SequenceInstaller : Installer<SequenceInstaller>
    {
        public override void InstallBindings()
        {
            BindData();
            BindFactories();
            BindRules();
            Container.BindSubKernel();
        }

        private void BindData()
        {
            Container.Bind<Sequence>().AsSingle();
            Container.Bind<CompositeDisposable>().AsSingle();
            Container.BindInterfacesTo<ClearableReactiveCollection<Node>>().AsSingle();
            
            Container.Bind<Node>()
                .FromSubContainerResolve()
                .ByInstaller<HeadInstaller>()
                .AsSingle();
        }

        private void BindFactories()
        {
            Container.BindFactory<Letter, Node, NodeFactory>()
                .FromSubContainerResolve()
                .ByInstaller<NodeInstaller>()
                .AsSingle();

            Container.BindFactory<char, Vector2, Letter, LetterFactory>()
                .FromSubContainerResolve()
                .ByInstaller<LetterInstaller>()
                .AsSingle();
        }

        private void BindRules()
        {
            Container.BindRule<PickLetterRule>();
            Container.BindRule<MatchWordRule>();
            Container.BindRule<CompleteLettersRule>();
            Container.BindRule<InitializeEmptySequenceRule>();
            Container.BindRule<TailMovementRule>();

            BindInputRules();
        }

        private void BindInputRules()
        {
            var inputRule = Application.isMobilePlatform 
                ? typeof(MobileInputRule) 
                : typeof(MouseInputRule);

            Container.BindInterfacesTo(inputRule).AsCached();
        }
    }
}