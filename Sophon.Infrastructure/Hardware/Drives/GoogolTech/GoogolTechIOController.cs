using System;

namespace Sophon.Infrastructure
{
    public class GoogolTechIoController : IIoController
    {
        public int InputCount => throw new NotImplementedException();

        public int OutputCount => throw new NotImplementedException();

        public bool GetIOCount(int cardNo, ref ushort _inCount, ref ushort _outCount)
        {
            throw new NotImplementedException();
        }

        public bool Initialize()
        {
            throw new NotImplementedException();
        }

        public bool ReadIn(int cardNo, int ioNo)
        {
            throw new NotImplementedException();
        }

        public bool ReadOut(int cardNo, int ioNo)
        {
            throw new NotImplementedException();
        }

        public bool SetOut(int cardNo, int ioNo, bool state)
        {
            throw new NotImplementedException();
        }
    }
}