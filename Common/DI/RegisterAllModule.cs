using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.DI
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
                var types = assembly.GetTypes().Where(x => typeof(IModuleRegister).IsAssignableFrom(x)
                                              && !x.IsInterface);
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

        /// <summary>
        /// 解决方案内所有项目名称集合
        /// </summary>
        private static readonly List<string> assemblyNames = new List<string>
        {
            "Common",
            "Sophon.Application",
            "Sophon.Core",
            "Sophon.Infrastructure",
            "Sophon.UI"
        };
    }
}
