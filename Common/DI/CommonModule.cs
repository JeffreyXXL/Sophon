using Autofac;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CommonModule : IModuleRegister
    {
        public void Register(ContainerBuilder builder)
        {
            //注册日志工厂
            builder.Register(c => new LoggerFactory(name => new NlogManager(name)))
                   .As<ILoggerFactory>()
                   .SingleInstance();
        }
    }
}
