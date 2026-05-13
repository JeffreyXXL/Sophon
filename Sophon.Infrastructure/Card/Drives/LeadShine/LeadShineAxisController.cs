using System;

namespace Sophon.Infrastructure
{
    public class LeadShineAxisController : IAxisController
    {
        public bool IsInitialize { get; set; }

        public int CardCount => _cardCount;

        public int AxisCount => _axisCount;

        private int _cardCount;
        private int _axisCount;

        public LeadShineAxisController()
        {
            if (Initialize())
            {
                //todo
            }
            else
            {
                throw new Exception("板卡初始化失败");
            }
        }

        public bool Initialize()
        {
            _cardCount = LTDMC.dmc_board_init();
            IsInitialize = _cardCount > 0;
            return IsInitialize;
        }

        public void GetAxisCount(int cardNo, ref uint axisCount)
        {
            if (LTDMC.dmc_get_total_axes((ushort)cardNo, ref axisCount) == 0)
            {
                _axisCount = (int)axisCount;
            }
            else
            {
                _axisCount = -1;
            }
        }

        public bool ServeOn(int cardNo, int axisNo)
        {
            return false;
        }

        public bool ServeOff(int cardNo, int axisNo)
        {
            return false;
        }

        public bool Home(int cardNo, int axisNo)
        {
            return LTDMC.dmc_home_move((ushort)cardNo, (ushort)axisNo) == 0;
        }

        public bool MoveAbs(int cardNo, int axisNo, double position)
        {
            return false;
        }

        public bool MoveRel(int cardNo, int axisNo, double distance)
        {
            return false;
        }

        public bool MoveInPos(int cardNo, int axisNo)
        {
            return false;
        }

        public bool Jog(int cardNo, int axisNo, bool direction)
        {
            return false;
        }

        public bool Stop(int cardNo, int axisNo)
        {
            return false;
        }

        public bool ResetAxis(int cardNo, int axisNo)
        {
            return false;
        }

        public bool ResetAll()
        {
            return false;
        }

        public double GetPos(int cardNo, int axisNo)
        {
            return 0;
        }

        public double GetVel(int cardNo, int axisNo)
        {
            return 0;
        }

        public double GetTorque(int cardNo, int axisNo)
        {
            return 0;
        }
    }
}