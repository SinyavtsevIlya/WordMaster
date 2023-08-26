using Zenject;

namespace WordMaster
{
    public class NodeInstaller : Installer<NodeInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Node>().AsSingle();

            Container.BindRule<TailMovementRule>();
            
            Container.BindSubKernel();
        }
    }
    
    public class NodeFactory : PlaceholderFactory<Letter, Node> { }
}