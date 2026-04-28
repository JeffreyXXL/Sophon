namespace Sophon.Infrastructure
{
    public interface IMotionFactory
    {
        IAxisController CreateAxisController();

        IIoController CreateIoController();
    }
}