using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class LoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// 传入名称，返回ILoggerFactory
        /// </summary>
        private readonly Func<string, ILoggerManager> _loggerCreator;

        private readonly string _path = ConfigurationManager.AppSettings["LoggerPath"];

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
            string path = $"{_path}/{loggername}/";
            string path_debug = $"{_path}_debug/{loggername}/";
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(path_debug);

            return _loggercache.GetOrAdd(loggername, _loggerCreator);
        }
    }
}
