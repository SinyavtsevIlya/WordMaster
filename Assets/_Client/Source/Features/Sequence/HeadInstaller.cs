using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class HeadInstaller : Installer<HeadInstaller>
    {
        private readonly Alphabet _alphabet;

        public HeadInstaller(Alphabet alphabet)
        {
            _alphabet = alphabet;
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

            var startLetter = _alphabet.Values[Random.Range(0, _alphabet.Values.Length)];
            Container.BindInstance(startLetter);
            Container.BindInstance(Vector2.zero);
        }
    }
}