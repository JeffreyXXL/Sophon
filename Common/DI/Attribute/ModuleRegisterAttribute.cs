using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleRegisterAttribute : Attribute
    {
        /// <summary>
        /// 根据order决定注册顺序
        /// </summary>
        public int Order { get; }
        public ModuleRegisterAttribute(int order = 0)
        {
            Order = order;
        }
    }
}
