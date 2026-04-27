using DryIoc;
using Prism.Ioc;

namespace Sophon.Core
{
    public static class CoreModuleRegister
    {
        public static void RegisterCore(this IContainerRegistry containerRegistry)
        {
            var container = ((IContainerExtension<IContainer>)containerRegistry).Instance;
            var assembly = typeof(CoreModuleRegister).Assembly;

            container.RegisterMany(new[] { assembly },
                type => type.IsClass && type.Name.EndsWith("Factory"), Reuse.Singleton);

            containerRegistry.RegisterSingleton<IStateMachine, StateMachine>();
            containerRegistry.RegisterSingleton<IWorkStationManager, WorkStationManager>();
        }
    }
}