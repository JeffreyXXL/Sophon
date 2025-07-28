using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Tests.Logger
{
    [TestFixture]
    public class NlogManagerTest
    {
        string directory = "D:/SophonDATA/logs/Test_Log";
        string directory_debug = "D:/SophonDATA/logs_Debug/Test_Log";
        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, recursive: true);
            }
            if (Directory.Exists(directory_debug))
            {
                Directory.Delete(directory_debug, recursive: true);
            }
        }

        /// <summary>
        /// 测试所有日志方法
        /// </summary>
        [Test]
        public void Logger_Test()
        {
            string path = Path.Combine(directory, $"{DateTime.Now:yyyy-MM-dd}.log");
            string path_debug = Path.Combine(directory_debug, $"{DateTime.Now:yyyy-MM-dd}.log");
            NlogManager nlog = new NlogManager("Test_Log");

            nlog.Trace("Test_Log_Trace");
            nlog.Debug("Test_Log_Debug");
            nlog.Info("Test_Log_Info");
            nlog.Warn("Test_Log_Warn");
            nlog.Error("Test_Log_Error");
            nlog.Fatal("Test_Log_Fatal");

            NLog.LogManager.Flush();

            Assert.That(File.Exists(path));
            Assert.That(File.Exists(path_debug));
        }
    }
}
