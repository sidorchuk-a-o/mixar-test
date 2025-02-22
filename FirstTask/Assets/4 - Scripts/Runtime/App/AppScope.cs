using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game
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

        public T Resolve<T>()
        {
            return Container.ResolveOrDefault<T>();
        }

        public void Inject(object instance)
        {
            Container.Inject(instance);
        }

        public void InjectGameObject(GameObject gameObject)
        {
            Container.InjectGameObject(gameObject);
        }
    }
}