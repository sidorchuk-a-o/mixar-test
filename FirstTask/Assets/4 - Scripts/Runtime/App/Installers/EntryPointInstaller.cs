using VContainer;
using VContainer.Unity;

namespace Game
{
    public class EntryPointInstaller<T> : IInstaller
    {
        void IInstaller.Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<T>();
        }
    }
}