using System;

namespace Common
{
    public class IniConfigException : Exception
    {
        public IniConfigException(string message) : base(message)
        {
        }
    }
}