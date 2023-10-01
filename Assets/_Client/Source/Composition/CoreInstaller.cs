using System;
using System.Threading.Tasks;
using Plugins.Nanory.SaveLoad;
using UnityEngine;
using UnityEngine.Localization;
using Zenject;
using Random = UnityEngine.Random;

namespace WordMaster
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
        [SerializeField] private CameraSettings _cameraSettings;
        [SerializeField] private ScriptableObject[] _settings;

        private PlayerSerializationState _playerSerializationState;
        private ISaveLoad _saveLoad;
        private Trie _trie;
        private Alphabet _alphabet;
        private Locale _locale;

        public void Construct(ISaveLoad saveLoad, Trie trie, PlayerSerializationState playerSerializationState,
            Alphabet alphabet, Locale locale)
        {
            _saveLoad = saveLoad;
            _trie = trie;
            _playerSerializationState = playerSerializationState;
            _alphabet = alphabet;
            _locale = locale;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_trie).AsSingle();
            Container.BindInstance(_locale).AsSingle();
            
            Container.Bind<Level>()
                .FromSubContainerResolve()
                .ByInstaller<LevelInstaller>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<RanOutOfEnergyEvent>().AsSingle();
            Container.Bind<ResumeRequest>().AsSingle();
            Container.Bind<RestartRequest>().AsSingle();

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

            Container.Bind<ISaveLoad>().FromInstance(_saveLoad).AsSingle();
            
            Container.Bind<Camera>().FromComponentInNewPrefab(_cameraSettings.Prefab).AsSingle();
            
            foreach (var s in _settings) 
                Container.Bind(s.GetType()).FromInstance(s).AsSingle();
            
            InstallScreenOrientation();
        }

        private void InstallScreenOrientation()
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
        }
    }
}