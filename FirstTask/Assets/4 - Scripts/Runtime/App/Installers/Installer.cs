using VContainer;
using VContainer.Unity;

namespace Game
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