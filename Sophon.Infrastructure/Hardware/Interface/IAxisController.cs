using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface IAxisController
    {
        bool IsInitialize { get; set; }

        bool Initialize();

        bool GetAxisCount(int cardNo, ref uint axisCount);

        bool ServeOn(int cardNo, int axisNo);

        bool ServeOff(int cardNo, int axisNo);

        bool Home(int axisNo);

        bool MoveAbs(int axisNo, double position);

        bool MoveRel(int axisNo, double distance);

        bool MoveInPos(int axisNo);

        bool Jog(int axisno, bool direction);

        bool Stop(int axisno);

        bool ResetAxis(int axisno);
        
        bool ResetAll();

        double GetPos(int axisNo);

        double GetVel(int axisNo);

        double GetTorque(int axisNo);
    }
}
 