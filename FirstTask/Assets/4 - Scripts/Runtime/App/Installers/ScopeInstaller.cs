using VContainer;
using VContainer.Unity;

namespace Game
{
    public class ScopeInstaller : IInstaller
    {
        private readonly IInstaller _configs;
        private readonly IInstaller _components;
        private readonly IInstaller[] _installers;

        public ScopeInstaller(
            ConfigsInstaller configs,
            ComponentsInstaller components,
            IInstaller[] installers)
        {
            _configs = configs;
            _components = components;
            _installers = installers;
        }

        void IInstaller.Install(IContainerBuilder builder)
        {
            _configs.Install(builder);
            _components.Install(builder);

            foreach (var installer in _installers)
            {
                installer.Install(builder);
            }
        }
    }
}