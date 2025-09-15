using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class DataReceivedEventArgs : EventArgs
    {
        public byte[] Data { get; }
        public DataReceivedEventArgs(byte[] data)
        {
            Data = data;
        }
    }
}
