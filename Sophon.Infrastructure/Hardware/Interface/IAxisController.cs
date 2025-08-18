using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface IAxisController
    {
        bool ServeOn(int cardNo, int axisNo);

        bool ServeOff(int cardNo, int axisNo);

        bool Home(int axisNo);

        bool MoveAbs(int axisNo, double position);

        bool MoveRel(int axisNo, double distance);

        double GetPos(int axisNo);

        double GetTorque(int axisNo);
    }
}
