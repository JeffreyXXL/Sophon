using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface IHardwareProvider
    {
        IAxisController Axis { get; }
        IIoController Io { get; }
    }
}
