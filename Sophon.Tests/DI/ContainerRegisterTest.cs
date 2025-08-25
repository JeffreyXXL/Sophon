using Autofac;
using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Tests.DI
{
    public class DITestClass
    {
        private readonly ILoggerFactory _loggerFactory;
        public DITestClass(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public ILoggerManager GetLogger()
        {
            return _loggerFactory.CreateLogger("DITestClass");
        }
    }

    [TestFixture]
    public class DITest
    {
        private readonly string directory = "D:/SophonDATA/logs/DITestClass";

        [TearDown]
        public void TearDown()
        {
            var directoriesToDelete = new[]
            {
                "D:/SophonDATA/logs/DITestClass",
                "D:/SophonDATA/logs_Debug/DITestClass"
            };
            foreach (var dir in directoriesToDelete)
            {
                if (Directory.Exists(dir))
                {
                    Directory.Delete(dir, recursive: true);
                }
            }
        }

        [Test]
        public void DI_Test()
        {
            string path = Path.Combine(directory, $"{DateTime.Now:yyyy-MM-dd}.log");
            var builder = new ContainerBuilder();
            builder.RegisterAllModuleExt();
            builder.RegisterType<DITestClass>();
            var container = builder.Build();
            var testclass = container.Resolve<DITestClass>();
            Assert.That(testclass, Is.Not.Null);

            var logger = testclass.GetLogger();
            logger.Info("DI test class write info log");
            NLog.LogManager.Flush();
            Assert.That(File.Exists(path));
        }
    }

    [TestFixture]
    public class ContainerRegisterTest
    {
        private readonly string directory = "D:/SophonDATA/logs/IOC_Test_Log";

        [TearDown]
        public void TearDown()
        {
            var directoriesToDelete = new[]
            {
                "D:/SophonDATA/logs/IOC_Test_Log",
                "D:/SophonDATA/logs_Debug/IOC_Test_Log"
            };
            foreach (var dir in directoriesToDelete)
            {
                if (Directory.Exists(dir))
                {
                    Directory.Delete(dir, recursive: true);
                }
            }
        }

        /// <summary>
        /// 测试容器注册是否成功，resolve后是否能正常使用
        /// </summary>
        [Test]
        public void Register_Test()
        {
            string path = Path.Combine(directory, $"{DateTime.Now:yyyy-MM-dd}.log");
            var builder = new ContainerBuilder();
            builder.RegisterAllModuleExt();
            var container = builder.Build();

            var loggerFactory = container.Resolve<ILoggerFactory>();
            Assert.That(loggerFactory, Is.Not.Null);
            Assert.That(loggerFactory, Is.InstanceOf<LoggerFactory>());

            var loggerFactory1 = container.Resolve<ILoggerFactory>();
            Assert.That(loggerFactory == loggerFactory1);

            var logger = loggerFactory.CreateLogger("IOC_Test_Log");
            Assert.That(logger, Is.Not.Null);

            logger.Info("This is a test log from container");
            NLog.LogManager.Flush();
            Assert.That(File.Exists(path));
        }
    }
}
