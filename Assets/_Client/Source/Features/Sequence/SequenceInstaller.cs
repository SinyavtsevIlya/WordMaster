using UniRx;
using Zenject;

namespace WordMaster
{
    public class SequenceInstaller : Installer<SequenceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Sequence>().AsSingle();
            Container.Bind<CompositeDisposable>().AsSingle();
            
            Container
                .Bind<Node>()
                .FromSubContainerResolve()
                .ByInstaller<NodeInstaller>()
                .WithKernel()
                .AsSingle();

            Container.Bind<ReactiveCollection<Node>>().AsSingle();

            Container.BindFactory<Letter, Node, Node, NodeFactory>()
                .FromSubContainerResolve()
                .ByInstaller<NodeInstaller>()
                .AsSingle();
            
            Container.BindRule<PickLetterRule>();
        }
    }
}