using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class LeadShineAxisControl : IAxisController
    {
        public bool IsInitialize { get; set; }

        /// <summary>
        /// 板卡数量
        /// </summary>
        private int _cardCount;

        public bool Initialize()
        {
            _cardCount = LTDMC.dmc_board_init();
            IsInitialize = _cardCount > 0;
            return IsInitialize;
        }

        public bool GetAxisCount(int cardNo, ref uint axisCount)
        {
            if (IsInitialize)
            {
                return LTDMC.dmc_get_total_axes((ushort)cardNo, ref axisCount) == 0;
            }
            return false;
        }

        public bool ServeOn(int cardNo, int axisNo)
        {
            return false;
        }

        public bool ServeOff(int cardNo, int axisNo)
        {
            return false;
        }

        public bool Home(int axisNo)
        {
            return false;
        }

        public bool MoveAbs(int axisNo, double position)
        {
            return false;
        }

        public bool MoveRel(int axisNo, double distance)
        {
            return false;
        }

        public bool MoveInPos(int axisNo)
        {
            return false;
        }

        public bool Jog(int axisno, bool direction)
        {
            return false;
        }

        public bool Stop(int axisno)
        {
            return false;
        }

        public bool ResetAxis(int axisno)
        {
            return false;
        }

        public bool ResetAll()
        {
            return false;
        }

        public double GetPos(int axisNo)
        {
            return 0;
        }

        public double GetVel(int axisNo)
        {
            return 0;
        }

        public double GetTorque(int axisNo)
        {
            return 0;
        }
    }
}