using DryIoc;
using Prism.Ioc;

namespace Sophon.Application
{
    public static class ApplicationModuleRegister
    {
        public static void RegisterApplication(this IContainerRegistry containerRegistry)
        {
            var container = ((IContainerExtension<IContainer>)containerRegistry).Instance;

            var assembly = typeof(ApplicationModuleRegister).Assembly;

            container.RegisterMany(new[] { assembly },
                type => type.IsClass && type.Name.EndsWith("Service"), Reuse.Singleton);

            containerRegistry.RegisterSingleton<IUserContext, UserContext>();
        }
    }
}