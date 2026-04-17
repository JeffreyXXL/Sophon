namespace Common
{
    public interface ILoggerManager
    {
        void Trace(string msg);

        void Debug(string msg);

        void Info(string msg);

        void Warn(string msg);

        void Error(string msg);

        void Fatal(string msg);
    }
}