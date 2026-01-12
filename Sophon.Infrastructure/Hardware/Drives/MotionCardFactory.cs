using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class MotionCardFactory : IMotionCardFactory
    {
        public IAxisController CreateAxisController()
        {
            throw new NotImplementedException();
        }

        public IIOController CreateIOController()
        {
            throw new NotImplementedException();
        }
    }
}
