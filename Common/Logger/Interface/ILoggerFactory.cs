namespace Common
{
    public interface ILoggerFactory
    {
        ILoggerManager CreateLogger(string loggername);
    }
}