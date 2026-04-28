using DryIoc;
using Prism.Ioc;
using Sophon.Common;
using System.Linq;
using System.Reflection;

namespace Sophon.Core
{
    public static class CoreModuleRegister
    {
        public static void RegisterCore(this IContainerRegistry containerRegistry)
        {
            var container = ((IContainerExtension<IContainer>)containerRegistry).Instance;
            var assembly = typeof(CoreModuleRegister).Assembly;
            var types = assembly.GetTypes()
                   .Where(t => t.IsClass && !t.IsAbstract && t.IsDefined(typeof(InjectableAttribute), false));
            var SingletonTypes = types
                 .Where(t => t.GetCustomAttribute<InjectableAttribute>().Lifetime == DependencyLifetime.Singleton).ToList();

            container.RegisterMany(SingletonTypes, Reuse.Singleton);
        }
    }
}