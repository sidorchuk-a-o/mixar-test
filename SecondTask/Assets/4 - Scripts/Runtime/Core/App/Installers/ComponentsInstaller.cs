using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AD.Core
{
    public class ComponentsInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private Component[] _components;

        void IInstaller.Install(IContainerBuilder builder)
        {
            foreach (var component in _components)
            {
                builder
                    .RegisterComponent(component)
                    .AsSelf();
            }
        }
    }
}