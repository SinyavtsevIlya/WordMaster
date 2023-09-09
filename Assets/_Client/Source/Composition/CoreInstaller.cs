﻿using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
        [SerializeField] private CameraSettings _cameraSettings;
        [SerializeField] private ScriptableObject[] _settings;

        private PlayerSerializationState _playerSerializationState;

        public void SetPlayerSerializationState(PlayerSerializationState playerSerializationState) =>
            _playerSerializationState = playerSerializationState;

        public override void InstallBindings()
        {
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

            Container.Bind<Alphabet>().AsSingle()
                .WithArguments("абвгдеёжзиклмнопрстуфхцчшщыэюя", "ы");
            
            Container.Bind<Camera>().FromComponentInNewPrefab(_cameraSettings.Prefab).AsSingle();
            
            foreach (var s in _settings) 
                Container.Bind(s.GetType()).FromInstance(s).AsSingle();
        }
    }
}