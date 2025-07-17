using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class RegisterAllModule
    {
        /// <summary>
        /// 注册所有模组的扩展方法
        /// </summary>
        /// <param name="builder"></param>
        public static ContainerBuilder RegisterAllModuleExt(this ContainerBuilder builder)
        {
            //1、加载所有assembly
            List<Assembly> assemblies = new List<Assembly>();
            var assemblyNames = ConfigurationManager.AppSettings["ModuleAssemblies"].Split(';');
            foreach (var name in assemblyNames)
            {
                try
                {
                    assemblies.Add(Assembly.Load(name));
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"加载{name}失败：" + e.Message);
                }
            }
            //2、获取所有继承IModuleRegister的类
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                    .Where(t => typeof(IModuleRegister).IsAssignableFrom(t)
                       && !t.IsInterface && !t.IsAbstract
                        && t.IsDefined(typeof(ModuleRegisterAttribute), false));

                //3、使用所有类的RegisterModule()方法
                foreach (var type in types)
                {
                    try
                    {
                        var instance = (IModuleRegister)Activator.CreateInstance(type);
                        instance.Register(builder);
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e.Message);
                    }
                }
            }
            return builder;
        }
    }
}
