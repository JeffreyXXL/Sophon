namespace Sophon.Infrastructure
{
    public interface IHardwareProvider
    {
        IAxisController Axis { get; }
        IIoController Io { get; }
    }
}