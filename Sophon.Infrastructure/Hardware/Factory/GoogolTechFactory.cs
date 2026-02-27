using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class GoogolTechFactory : IHardwareFactory
    {
        private IAxisController axisController;
        private IIoController ioController;

        public IAxisController CreateAxisController()
        {
            if (axisController == null)
            {
                axisController = new GoogolTechAxisController();
            }
            return axisController;
        }

        public IIoController CreateIoController()
        {
            if (ioController == null)
            {
                ioController = new GoogolTechIoController();
            }
            return ioController;
        }
    }
}
