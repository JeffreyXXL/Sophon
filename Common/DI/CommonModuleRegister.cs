using Autofac;
using NLog;

namespace Common
{
    [ModuleRegister]
    public class CommonModuleRegister : IModuleRegister
    {
        public void Register(ContainerBuilder builder)
        {
            //注册日志工厂
            builder.Register(c => new LoggerFactory(name => new NlogManager(name)))
                   .As<ILoggerFactory>()
                   .SingleInstance();
            //注册配置器工厂
            builder.RegisterType<ConfigManagerFactory>()
                   .As<IConfigManagerFactory>()
                   .SingleInstance();
        }
    }
}