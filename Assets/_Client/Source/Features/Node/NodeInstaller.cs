using Zenject;

namespace WordMaster
{
    public class NodeInstaller : Installer<NodeInstaller>
    {
        private readonly LetterSettings _letterSettings;

        public NodeInstaller(LetterSettings letterSettings)
        {
            _letterSettings = letterSettings;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<Node>().AsSingle();

            Container
                .Bind<Letter>()
                .FromSubContainerResolve()
                .ByInstaller<LetterInstaller>()
                .WithKernel()
                .WithArguments(_letterSettings);
        }
    }
    
    public class NodeFactory : PlaceholderFactory<Letter, Node, Node> { }
}