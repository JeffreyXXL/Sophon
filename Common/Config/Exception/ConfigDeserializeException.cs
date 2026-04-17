using System;

namespace Common
{
    public class ConfigDeserializeException : Exception
    {
        public ConfigDeserializeException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}