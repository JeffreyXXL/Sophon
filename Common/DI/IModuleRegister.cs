using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DI
{
    public interface IModuleRegister
    {
        /// <summary>
        /// 注册模组内所有需要注册的类
        /// </summary>
        /// <param name="builder"></param>
        void Register(ContainerBuilder builder);
    }
}
