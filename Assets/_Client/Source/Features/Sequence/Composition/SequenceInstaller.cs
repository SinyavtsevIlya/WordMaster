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
            Container.Bind<Sequence>().AsSingle();
            Container.Bind<CompositeDisposable>().AsSingle();
            
            Container.BindInterfacesTo<ClearableReactiveCollection<Node>>().AsSingle();

            Container.BindFactory<Letter, Node, NodeFactory>()
                .FromSubContainerResolve()
                .ByInstaller<NodeInstaller>()
                .AsSingle();
            
            Container.BindFactory<char, Vector2, Letter, LetterFactory>()
                .FromSubContainerResolve()
                .ByInstaller<LetterInstaller>()
                .AsSingle();
            
            Container.BindRule<PickLetterRule>();
            Container.BindRule<MatchWordRule>();
            Container.BindRule<CompleteLettersRule>();
            Container.BindRule<InitializeEmptySequenceRule>();
            Container.BindRule<InputToHeadTrackingRule>();
            Container.BindRule<TailMovementRule>();

            Container.Bind<Node>()
                .FromSubContainerResolve()
                .ByInstaller<HeadInstaller>()
                .AsSingle();

            Container.BindSubKernel();
        }
    }
}