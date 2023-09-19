using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
        [SerializeField] private CameraSettings _cameraSettings;
        [SerializeField] private ScriptableObject[] _settings;

        private PlayerSerializationState _playerSerializationState;
        private Trie _trie;
        private Alphabet _alphabet;

        public void Construct(Trie trie, PlayerSerializationState playerSerializationState, Alphabet alphabet)
        {
            _trie = trie;
            _playerSerializationState = playerSerializationState;
            _alphabet = alphabet;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_trie).AsSingle();
            
            Container.Bind<Level>()
                .FromSubContainerResolve()
                .ByInstaller<LevelInstaller>()
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_playerSerializationState).AsSingle();

            Container.Bind(typeof(Player), typeof(IFlowTarget))
                .FromSubContainerResolve()
                .ByInstaller<PlayerInstaller>()
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_alphabet).AsSingle();

            if (_playerSerializationState.IsTutorialShown)
            {
                Container
                    .BindInstance(_alphabet.ValidStartValues[Random.Range(0, _alphabet.ValidStartValues.Length)])
                    .AsSingle();
            }
            else
            {
                Container.BindInstance(_alphabet.InitialWord[0]).AsSingle();
            }
            
            Container.Bind<Camera>().FromComponentInNewPrefab(_cameraSettings.Prefab).AsSingle();
            
            foreach (var s in _settings) 
                Container.Bind(s.GetType()).FromInstance(s).AsSingle();
        }
    }
}