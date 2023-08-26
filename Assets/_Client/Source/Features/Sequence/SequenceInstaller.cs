﻿using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class SequenceInstaller : Installer<SequenceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Sequence>().AsSingle();
            Container.Bind<CompositeDisposable>().AsSingle();
            
            Container.BindInterfacesTo<ReactiveCollection<Node>>().AsSingle();

            Container.BindFactory<Letter, Node, NodeFactory>()
                .FromSubContainerResolve()
                .ByInstaller<NodeInstaller>()
                .AsSingle();
            
            Container.BindRule<PickLetterRule>();
            Container.BindRule<InputToHeadTrackingRule>();

            Container.Bind<Node>()
                .FromSubContainerResolve()
                .ByInstaller<HeadInstaller>()
                .AsSingle();
            
            Container.BindSubKernel();
        }
    }
}