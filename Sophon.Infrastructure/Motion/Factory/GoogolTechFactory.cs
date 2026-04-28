using Sophon.Common;

namespace Sophon.Infrastructure
{
    [InjectableAttribute(DependencyLifetime.Delegate)]
    public class GoogolTechFactory : IMotionFactory
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