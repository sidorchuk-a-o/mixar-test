using UnityEngine;
using VContainer.Unity;

namespace AD.Core
{
    public class AppScope : LifetimeScope
    {
        [Header("Installers")]
        [SerializeField] private ConfigsInstaller _configs;
        [SerializeField] private ComponentsInstaller _components;

        public void Install(IInstaller[] installers)
        {
            var installer = new ScopeInstaller(_configs, _components, installers);

            using (Enqueue(installer))
            {
                Build();
            }
        }
    }
}