using Common;
using DryIoc;
using Prism.Ioc;
using System;
using System.Configuration;
using System.Linq;

namespace Sophon.Infrastructure
{
    public static class InfrastructureModuleRegister
    {
        public static void RegisterInfrastructure(this IContainerRegistry containerRegistry)
        {
            var container = ((IContainerExtension<IContainer>)containerRegistry).Instance;
            var assembly = typeof(InfrastructureModuleRegister).Assembly;
            var serviceTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract &&
                (t.Name.EndsWith("Repository") || t.Name.EndsWith("Protocol")));

            container.RegisterMany(serviceTypes, Reuse.Singleton);

            container.RegisterDelegate<DbContext>(c =>
            {
                var factory = c.Resolve<ILoggerFactory>();
                var path = ConfigurationManager.AppSettings["DatebaseFilePath"];
                string connstr = "Data Source = " + PathResolver.GetAbsolutePath(path) + ";";
                return new DbContext(connstr, factory);
            }, Reuse.Singleton);

            container.RegisterDelegate<IHardwareFactory>(c =>
            {
                string brand = ConfigurationManager.AppSettings["CardBrand"];

                switch (brand)
                {
                    case "LeadShine":
                        return new LeadShineFactory();

                    case "GoogolTech":
                        return new GoogolTechFactory();
                }
                throw new Exception("未知板卡品牌");
            }, Reuse.Singleton);

            container.RegisterDelegate<IAxisController>(c =>
              c.Resolve<IHardwareFactory>().CreateAxisController(), Reuse.Singleton);
            container.RegisterDelegate<IIoController>(c =>
              c.Resolve<IHardwareFactory>().CreateIoController(), Reuse.Singleton);

            containerRegistry.RegisterSingleton<IHardwareProvider, HardwareProvider>();
            containerRegistry.RegisterSingleton<IDatabaseInitializer, DatabaseInitializer>();
            containerRegistry.RegisterScoped<ITransaction, Transaction>();
        }
    }
}