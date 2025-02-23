using VContainer;
using VContainer.Unity;

namespace AD.Core
{
    public class Installer<T> : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder
                .Register<T>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}