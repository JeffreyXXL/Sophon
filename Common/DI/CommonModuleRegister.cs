using DryIoc;
using Prism.Ioc;

namespace Common
{
    public static class CommonModuleRegister
    {
        public static void RegisterCommon(this IContainerRegistry containerRegistry)
        {
            var container = ((IContainerExtension<IContainer>)containerRegistry).Instance;

            container.RegisterDelegate<ILoggerFactory>(c =>
                new LoggerFactory(name => new NlogManager(name)), Reuse.Singleton);

            containerRegistry.RegisterSingleton<IConfigManagerFactory, ConfigManagerFactory>();
        }
    }
}