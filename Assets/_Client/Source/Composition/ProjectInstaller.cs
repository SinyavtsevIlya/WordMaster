﻿using UnityEngine;
using Zenject;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create ProjectInstaller", fileName = "ProjectInstaller", order = 0)]
    public class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Trie>()
                .FromSubContainerResolve()
                .ByInstaller<TrieInstaller>()
                .AsSingle();
        }
    }
}