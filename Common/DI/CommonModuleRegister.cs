using Autofac;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
