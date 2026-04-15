using Autofac;
using Common;

namespace Sophon.Core
{
    [ModuleRegister]
    public class CoreModuleRegister : IModuleRegister
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<FlowEngineFactory>()
                   .As<IFlowEngineFactory>()
                   .SingleInstance();

            builder.RegisterType<FlowContextFactory>()
                   .As<IFlowContextFactory>()
                   .SingleInstance();

            builder.RegisterType<StateMachine>()
                   .As<IStateMachine>()
                   .InstancePerDependency();

            builder.RegisterType<WorkStationFactory>()
                   .As<IWorkStationFactory>()
                   .SingleInstance();

            builder.RegisterType<WorkStationManager>()
                   .As<IWorkStationManager>()
                   .SingleInstance();
        }
    }
}