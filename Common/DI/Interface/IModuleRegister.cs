using Autofac;

namespace Common
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