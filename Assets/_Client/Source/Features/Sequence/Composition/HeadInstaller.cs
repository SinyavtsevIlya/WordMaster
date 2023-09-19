using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class HeadInstaller : Installer<HeadInstaller>
    {
        private char _startingLetter;

        public HeadInstaller(char startingLetter)
        {
            _startingLetter = startingLetter;
        }

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
            
            Container.BindInstance(_startingLetter);
            Container.BindInstance(Vector2.zero);
        }
    }
}