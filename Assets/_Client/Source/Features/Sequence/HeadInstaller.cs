using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class HeadInstaller : Installer<HeadInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<Node>()
                .FromSubContainerResolve()
                .ByInstaller<NodeInstaller>()
                .AsSingle();
            
            Container
                .Bind<Letter>()
                .FromSubContainerResolve()
                .ByInstaller<LetterInstaller>()
                .AsSingle();

            Container.BindInstance('c');
            Container.BindInstance(Vector2.zero);
        }
    }
}