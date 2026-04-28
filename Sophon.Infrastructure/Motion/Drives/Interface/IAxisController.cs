namespace Sophon.Infrastructure
{
    public interface IAxisController
    {
        bool IsInitialize { get; set; }

        int CardCount { get; }

        int AxisCount { get; }

        bool Initialize();

        void GetAxisCount(int cardNo, ref uint axisCount);

        bool ServeOn(int cardNo, int axisNo);

        bool ServeOff(int cardNo, int axisNo);

        bool Home(int cardNo, int axisNo);

        bool MoveAbs(int cardNo, int axisNo, double position);

        bool MoveRel(int cardNo, int axisNo, double distance);

        bool MoveInPos(int cardNo, int axisNo);

        bool Jog(int cardNo, int axisno, bool direction);

        bool Stop(int cardNo, int axisno);

        bool ResetAxis(int cardNo, int axisno);

        bool ResetAll();

        double GetPos(int cardNo, int axisNo);

        double GetVel(int cardNo, int axisNo);

        double GetTorque(int cardNo, int axisNo);
    }
}