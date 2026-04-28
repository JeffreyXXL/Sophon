using DryIoc;
using Prism.Ioc;
using System.Linq;

namespace Sophon.Application
{
    public static class ApplicationModuleRegister
    {
        public static void RegisterApplication(this IContainerRegistry containerRegistry)
        {
            var container = ((IContainerExtension<IContainer>)containerRegistry).Instance;

            var assembly = typeof(ApplicationModuleRegister).Assembly;

            var serviceTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));

            container.RegisterMany(serviceTypes, Reuse.Singleton);
            
            containerRegistry.RegisterSingleton<IUserContext, UserContext>();
        }
    }
}