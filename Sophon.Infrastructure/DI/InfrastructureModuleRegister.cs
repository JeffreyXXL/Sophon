using Autofac;
using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class InfrastructureModuleRegister : IModuleRegister
    {
        public void Register(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var factory = c.Resolve<ILoggerFactory>();
                var connstr = ConfigurationManager.AppSettings["ConnectionString"];
                return new DbContext(connstr, factory); ;
            }).InstancePerLifetimeScope();

            builder.RegisterType<DatabaseInitializer>()
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterType<LoginHistoryRepository>()
                   .AsSelf()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
                   .AsSelf()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ProductionHistoryRepository>()
                   .AsSelf()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ProductionRepository>()
                   .AsSelf()
                   .InstancePerLifetimeScope();

            builder.RegisterType<Transaction>()
                   .As<ITransaction>()
                   .InstancePerLifetimeScope();
        }
    }
}
