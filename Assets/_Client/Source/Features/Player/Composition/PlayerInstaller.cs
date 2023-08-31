﻿using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CompositeDisposable>().AsSingle();
            
            Container.Bind<Player>().AsSingle();
            
            Container.Bind<Score>()
                .FromSubContainerResolve()
                .ByInstaller<ScoreInstaller>()
                .AsSingle();

            Container.Bind<Energy>().AsSingle();
            
            Container.Bind<Canvas>()
                .FromSubContainerResolve()
                .ByInstaller<UIInstaller>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesTo<ReactiveCollection<Word>>().AsSingle();

            Container.Bind<Sequence>()
                .FromSubContainerResolve()
                .ByInstaller<SequenceInstaller>()
                .AsSingle();
            
            Container.BindRule<AddScoreRule>();
            
            Container.BindViewToModel<Score, ScoreWidget>();
            
            Container.BindSubKernel();
        }
    }
}