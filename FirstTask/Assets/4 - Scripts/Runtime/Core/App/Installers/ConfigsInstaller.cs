﻿using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AD.Core
{
    public class ConfigsInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private ScriptableObject[] _configs;

        void IInstaller.Install(IContainerBuilder builder)
        {
            foreach (var config in _configs)
            {
                builder
                    .RegisterInstance(config)
                    .AsSelf();
            }
        }
    }
}