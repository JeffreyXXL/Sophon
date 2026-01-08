using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class GoogolTechAxisController : IAxisController
    {
        public bool IsInitialize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int CardCount => throw new NotImplementedException();

        public int AxisCount => throw new NotImplementedException();

        public void GetAxisCount(int cardNo, ref uint axisCount)
        {
            throw new NotImplementedException();
        }

        public double GetPos(int cardNo, int axisNo)
        {
            throw new NotImplementedException();
        }

        public double GetTorque(int cardNo, int axisNo)
        {
            throw new NotImplementedException();
        }

        public double GetVel(int cardNo, int axisNo)
        {
            throw new NotImplementedException();
        }

        public bool Home(int cardNo, int axisNo)
        {
            throw new NotImplementedException();
        }

        public bool Initialize()
        {
            throw new NotImplementedException();
        }

        public bool Jog(int cardNo, int axisno, bool direction)
        {
            throw new NotImplementedException();
        }

        public bool MoveAbs(int cardNo, int axisNo, double position)
        {
            throw new NotImplementedException();
        }

        public bool MoveInPos(int cardNo, int axisNo)
        {
            throw new NotImplementedException();
        }

        public bool MoveRel(int cardNo, int axisNo, double distance)
        {
            throw new NotImplementedException();
        }

        public bool ResetAll()
        {
            throw new NotImplementedException();
        }

        public bool ResetAxis(int cardNo, int axisno)
        {
            throw new NotImplementedException();
        }

        public bool ServeOff(int cardNo, int axisNo)
        {
            throw new NotImplementedException();
        }

        public bool ServeOn(int cardNo, int axisNo)
        {
            throw new NotImplementedException();
        }

        public bool Stop(int cardNo, int axisno)
        {
            throw new NotImplementedException();
        }
    }
}
