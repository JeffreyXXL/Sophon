using NLog;

namespace Common
{
    public class NlogManager : ILoggerManager
    {
        private readonly ILogger _logger;

        /// <summary>
        /// 根据传入的名称创建日志
        /// </summary>
        /// <param name="loggername"></param>
        public NlogManager(string loggername)
        {
            _logger = LogManager.GetLogger(loggername);
        }

        public void Trace(string msg) => _logger.Trace(msg);

        public void Debug(string msg) => _logger.Debug(msg);

        public void Info(string msg) => _logger.Info(msg);

        public void Warn(string msg) => _logger.Warn(msg);

        public void Error(string msg) => _logger.Error(msg);

        public void Fatal(string msg) => _logger.Fatal(msg);
    }
}