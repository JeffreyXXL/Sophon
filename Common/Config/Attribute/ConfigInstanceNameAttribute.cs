using System;

namespace Common
{
    /// <summary>
    /// 如果使用ini配置，则类需要使用此特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IniConfigInstanceNameAttribute : Attribute
    {
        public IniConfigInstanceNameAttribute()
        {
        }
    }
}