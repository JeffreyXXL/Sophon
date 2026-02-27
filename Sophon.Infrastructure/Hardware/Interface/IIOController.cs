using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface IIoController
    {
        bool Initialize();

        int InputCount { get; }

        int OutputCount { get; }

        bool GetIOCount(int cardNo, ref ushort _inCount, ref ushort _outCount);

        bool SetOut(int cardNo, int ioNo, bool state);

        bool ReadIn(int cardNo, int ioNo);

        bool ReadOut(int cardNo, int ioNo);
    }
}
