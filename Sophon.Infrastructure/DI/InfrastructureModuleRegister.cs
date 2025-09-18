using Autofac;
using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
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
                   .As<IRepository<LoginHistory>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
                   .As<IRepository<User>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ProductionHistoryRepository>()
                   .As<IRepository<ProductionHistory>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ProductionRepository>()
                   .As<IRepository<Production>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<Transaction>()
                   .As<ITransaction>()
                   .InstancePerLifetimeScope();


            builder.RegisterType<TcpIpProtocol>()
                   .As<ITcpIpProtocol>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<SerialPortProtocol>()
                   .As<ISerialPortProtocol>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<HttpProtocol>()
                   .As<IHttpProtocol>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ModbusProtocol>()
                   .As<IModbusProtocol>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<AdsProtocol>()
                   .As<IAdsProtocol>()
                   .InstancePerLifetimeScope();

    

        }
    }
}
