using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class IniConfigException : Exception
    {
        public IniConfigException(string message) : base(message) { }
    }
}
