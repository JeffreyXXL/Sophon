using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Common
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class InjectableAttribute : Attribute
    {
        public DependencyLifetime Lifetime { get; }

        public InjectableAttribute(DependencyLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }

    public enum DependencyLifetime
    {
        Singleton,
        Delegate
    }
}