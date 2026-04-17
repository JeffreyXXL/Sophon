namespace Sophon.Infrastructure
{
    public interface IHardwareFactory
    {
        IAxisController CreateAxisController();

        IIoController CreateIoController();
    }
}