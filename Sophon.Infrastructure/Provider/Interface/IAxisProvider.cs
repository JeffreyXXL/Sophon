namespace Sophon.Infrastructure
{
    public interface IAxisProvider
    {
        IAxisController Axis { get; }
        IIoController Io { get; }
    }
}