using Rules;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class NodeInstaller : Installer<NodeInstaller>
    {
        private readonly Letter _letter;

        public NodeInstaller(Letter letter)
        {
            _letter = letter;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<Node>().AsSingle();
            Container.BindInstance(_letter);
            Container.BindRule<TailMovementRule>();
            Container.BindSubKernel();
        }
    }

    public class NodeFactory : PlaceholderFactory<Letter, Node> { }
}