using Common;
using DryIoc;
using Prism.Ioc;
using Sophon.Common;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Sophon.Infrastructure
{
    public static class InfrastructureModuleRegister
    {
        public static void RegisterInfrastructure(this IContainerRegistry containerRegistry)
        {
            var container = ((IContainerExtension<IContainer>)containerRegistry).Instance;
            var assembly = typeof(InfrastructureModuleRegister).Assembly;
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsDefined(typeof(InjectableAttribute), false));
            var SingletonTypes = types
                 .Where(t => t.GetCustomAttribute<InjectableAttribute>().Lifetime == DependencyLifetime.Singleton).ToList();

            container.RegisterMany(SingletonTypes, Reuse.Singleton);

            container.RegisterDelegate<DbContext>(c =>
            {
                var factory = c.Resolve<ILoggerFactory>();
                var path = ConfigurationManager.AppSettings["DatebaseFilePath"];
                string connstr = "Data Source = " + PathResolver.GetAbsolutePath(path) + ";";
                return new DbContext(connstr, factory);
            }, Reuse.Singleton);

            container.RegisterDelegate<IMotionFactory>(c =>
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
              c.Resolve<IMotionFactory>().CreateAxisController(), Reuse.Singleton);
            container.RegisterDelegate<IIoController>(c =>
              c.Resolve<IMotionFactory>().CreateIoController(), Reuse.Singleton);
        }
    }
}