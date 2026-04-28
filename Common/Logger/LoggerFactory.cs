using Sophon.Common;
using System;
using System.Collections.Concurrent;

namespace Common
{
    [InjectableAttribute(DependencyLifetime.Delegate)]
    public class LoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// 传入名称，返回ILoggerFactory
        /// </summary>
        private readonly Func<string, ILoggerManager> _loggerCreator;

        /// <summary>
        /// 存储所有日志缓存
        /// </summary>
        private readonly ConcurrentDictionary<string, ILoggerManager> _loggercache = new ConcurrentDictionary<string, ILoggerManager>();

        /// <summary>
        /// 传入带名字的委托
        /// </summary>
        /// <param name="loggerCreator"></param>
        public LoggerFactory(Func<string, ILoggerManager> loggerCreator)
        {
            _loggerCreator = loggerCreator;
        }

        /// <summary>
        /// 工厂创建日志
        /// 如果cache有，则直接给出，如果没有，则调用传入的委托，并把新的放入cache
        /// </summary>
        /// <param name="loggername"></param>
        /// <returns></returns>
        public ILoggerManager CreateLogger(string loggername)
        {
            return _loggercache.GetOrAdd(loggername, _loggerCreator);
        }
    }
}