using VContainer;
using VContainer.Unity;

namespace AD.Core
{
    public class EntryPointInstaller<T> : IInstaller
    {
        void IInstaller.Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<T>();
        }
    }
}