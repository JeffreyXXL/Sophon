namespace Sophon.Infrastructure
{
    public interface ICardFactory
    {
        IAxisController CreateAxisController();

        IIoController CreateIoController();
    }
}