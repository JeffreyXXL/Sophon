using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class HardwareProvider : IHardwareProvider
    {
        public IAxisController Axis { get; }
        public IIoController Io { get; }

        public HardwareProvider(IAxisController axis, IIoController io)
        {
            Axis = axis;
            Io = io;
        }
    }
}
