namespace Sophon.Infrastructure
{
    public class HardwareProvider : IHardwareProvider
    {
        public IAxisController Axis { get; }
        public IIoController Io { get; }

        public HardwareProvider(IAxisController axis, IIoController io)
        {
            Axis = axis;
            Io = io;
        }
    }
}