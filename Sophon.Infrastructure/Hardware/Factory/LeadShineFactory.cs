namespace Sophon.Infrastructure
{
    public class LeadShineFactory : IHardwareFactory
    {
        private IAxisController axisController;
        private IIoController ioController;

        public IAxisController CreateAxisController()
        {
            if (axisController == null)
            {
                axisController = new LeadShineAxisController();
            }
            return axisController;
        }

        public IIoController CreateIoController()
        {
            if (ioController == null)
            {
                var controller = CreateAxisController();
                ioController = new LeadShineIoController(controller);
            }
            return ioController;
        }
    }
}