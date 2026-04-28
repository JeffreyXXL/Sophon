namespace Sophon.Infrastructure
{
    public interface IMotionProvider
    {
        IAxisController Axis { get; }
        IIoController Io { get; }
    }
}