using Autofac;
using Common;

namespace Sophon.Application
{
    [ModuleRegister]
    public class ApplicationModuleRegister : IModuleRegister
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<UserContext>()
                   .As<IUserContext>()
                   .SingleInstance();

            builder.RegisterType<HardwareService>()
                   .As<IHardwareService>()
                   .SingleInstance();
        }
    }
}