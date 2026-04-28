using DryIoc;
using Prism.Ioc;
using System.Linq;

namespace Sophon.Core
{
    public static class CoreModuleRegister
    {
        public static void RegisterCore(this IContainerRegistry containerRegistry)
        {
            var container = ((IContainerExtension<IContainer>)containerRegistry).Instance;
            var assembly = typeof(CoreModuleRegister).Assembly;
            var serviceTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Factory"));
            container.RegisterMany(serviceTypes, Reuse.Singleton);

            containerRegistry.RegisterSingleton<IStateMachine, StateMachine>();
            containerRegistry.RegisterSingleton<IWorkStationManager, WorkStationManager>();
        }
    }
}