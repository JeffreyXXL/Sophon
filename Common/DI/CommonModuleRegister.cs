using DryIoc;
using Prism.Ioc;
using Sophon.Common;
using System.Linq;
using System.Reflection;

namespace Common
{
    public static class CommonModuleRegister
    {
        public static void RegisterCommon(this IContainerRegistry containerRegistry)
        {
            var container = ((IContainerExtension<IContainer>)containerRegistry).Instance;
            var assembly = typeof(CommonModuleRegister).Assembly;
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsDefined(typeof(InjectableAttribute), false));
            var SingletonTypes = types
                .Where(t => t.GetCustomAttribute<InjectableAttribute>().Lifetime == DependencyLifetime.Singleton).ToList();

            container.RegisterMany(SingletonTypes, Reuse.Singleton);

            container.RegisterDelegate<ILoggerFactory>(c =>
                new LoggerFactory(name => new NlogManager(name)), Reuse.Singleton);
        }
    }
}