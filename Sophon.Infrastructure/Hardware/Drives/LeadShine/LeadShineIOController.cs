using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class LeadShineIOController : IIOController
    {
        public LeadShineAxisControl _leadShineAxisControl;
        public LeadShineIOController(LeadShineAxisControl leadShineAxisControl)
        {
            _leadShineAxisControl = leadShineAxisControl;
        }

        public bool Initialize()
        {
            if (!_leadShineAxisControl.IsInitialize)
            {
                _leadShineAxisControl.Initialize();
            }
            return _leadShineAxisControl.IsInitialize;
        }

        public bool GetIOCount(int cardNo, ref ushort inCount, ref ushort outCount)
        {
            if (_leadShineAxisControl.IsInitialize)
            {
                return LTDMC.dmc_get_total_ionum((ushort)cardNo, ref inCount, ref outCount) == 0;
            }
            return false;
        }

        public bool SetOut(int cardNo, int ioNo, bool state)
        {
            return LTDMC.dmc_write_outbit((ushort)cardNo, (ushort)ioNo, (ushort)(state ? 1 : 0)) == 0;
        }

        public bool ReadIn(int cardNo, int ioNo)
        {
            return LTDMC.dmc_read_inbit((ushort)cardNo, (ushort)ioNo) == 0; //npn
        }

        public bool ReadOut(int cardNo, int ioNo)
        {
            return LTDMC.dmc_read_outbit((ushort)cardNo, (ushort)ioNo) == 0;
        }

    }
}
