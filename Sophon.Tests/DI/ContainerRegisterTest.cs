using Autofac;
using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Tests.DI
{

    [TestFixture]
    public class ContainerRegisterTest
    {
        string directory = "D:/SophonDATA/logs/IOC_Test_Log";

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, recursive: true);
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
            var logger = loggerFactory.CreateLogger("IOC_Test_Log");

            Assert.That(logger, Is.Not.Null);

            logger.Info("This is a test log from container.");
            NLog.LogManager.Flush();

            Assert.That(File.Exists(path));          
        }
    }
}
