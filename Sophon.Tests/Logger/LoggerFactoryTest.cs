using Common;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Tests.Logger
{
    [TestFixture]
    public class LoggerFactoryTest
    {
        private LoggerFactory _factory;
        private readonly Func<string, ILoggerManager> _loggerManager;

        /// <summary>
        /// 测试是否能正确创建日志,是否同名只创建一个日志，是否能创建不同名称的日志
        /// </summary>
        [Test]
        public void CreateLogger_Test()
        {
            var factory = new LoggerFactory(name => { return new Mock<ILoggerManager>().Object; });
            var logger1 = factory.CreateLogger("test");
            Assert.That(logger1, Is.Not.Null);

            var logger2 = factory.CreateLogger("test");
            Assert.That(logger1 == logger2);
            var loggercacheField = factory.GetType()
                .GetField("_loggercache", BindingFlags.NonPublic | BindingFlags.Instance);
            var loggercache = loggercacheField
                .GetValue(factory) as ConcurrentDictionary<string, ILoggerManager>;
            Assert.That(loggercache.Count == 1);

            var logger3 = factory.CreateLogger("test2");
            loggercache = loggercacheField
                .GetValue(factory) as ConcurrentDictionary<string, ILoggerManager>;
            Assert.That(logger3, Is.Not.Null);
            Assert.That(loggercache.Count == 2);
        }
    }
}
