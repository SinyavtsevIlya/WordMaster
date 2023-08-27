using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
        [SerializeField] private LevelSettings _levelSettings;
        [SerializeField] private LetterSettings _letterSettings;
        [SerializeField] private NodeSettings _nodeSettings;
        [SerializeField] private SequenceSettings _sequenceSettings;
        [SerializeField] private CameraSettings _cameraSettings;

        public override void InstallBindings()
        {
            Container.Bind<Level>()
                .FromSubContainerResolve()
                .ByInstaller<LevelInstaller>()
                .AsSingle()
                .NonLazy();

            Container.Bind<Player>()
                .FromSubContainerResolve()
                .ByInstaller<PlayerInstaller>()
                .AsSingle()
                .NonLazy();

            Container.Bind<Alphabet>().AsSingle()
                .WithArguments("абвгдеёжзиклмнопрстуфхцчшщыэюя");

            Container.Bind<Trie>()
                .FromSubContainerResolve()
                .ByInstaller<TrieInstaller>()
                .AsSingle();

            Container.BindInstance(_levelSettings).AsSingle();
            Container.BindInstance(_letterSettings).AsSingle();
            Container.BindInstance(_nodeSettings).AsSingle();
            Container.BindInstance(_sequenceSettings).AsSingle();
            Container.BindInstance(_cameraSettings).AsSingle();
        }
    }
}