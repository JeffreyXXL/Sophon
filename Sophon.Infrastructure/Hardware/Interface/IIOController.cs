using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface IIOController
    {
        void SetOut(int cardNo, int ioNo, bool state);

        bool ReadIn(int cardNo, int ioNo);

        bool ReadOut(int cardNo, int ioNo);
    }
}
