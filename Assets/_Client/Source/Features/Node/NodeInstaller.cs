using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class NodeInstaller : Installer<NodeInstaller>
    {
        private readonly Letter _letter;
        private readonly NodeSettings _settings;

        public NodeInstaller(Letter letter, NodeSettings settings)
        {
            _letter = letter;
            _settings = settings;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<Node>().AsSingle();
            Container.BindInstance(_letter);

            Container.Bind<NodeView>()
                .FromComponentInNewPrefab(_settings.NodePrefab)
                .AsSingle();

            Container.BindRule<NodePresentRule>();

            Container.BindSubKernel();
        }
    }

    public class NodeFactory : PlaceholderFactory<Letter, Node> { }
}