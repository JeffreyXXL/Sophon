using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConfigDeserializeException : Exception
    {
        public ConfigDeserializeException(string message, Exception inner) : base(message, inner) { }
    }
}
