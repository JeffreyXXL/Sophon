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
        /// <summary>
        /// 测试所有日志方法
        /// </summary>
        [Test]
        public void Logger_Test()
        {
            string path = $"D:/SophonDATA/logs/Test_Log/{DateTime.Now:yyyy-MM-dd}.log";
            string path_debug = $"D:/SophonDATA/logs_debug/Test_Log/{DateTime.Now:yyyy-MM-dd}.log";
            NlogManager nlog = new NlogManager("Test_Log");
            string time = DateTime.Now.ToString();
            nlog.Trace("Test_Log_Trace" + time);
            nlog.Debug("Test_Log_Debug" + time);
            nlog.Info("Test_Log_Info" + time);
            nlog.Warn("Test_Log_Warn" + time);
            nlog.Error("Test_Log_Error" + time);
            nlog.Fatal("Test_Log_Fatal" + time);

            NLog.LogManager.Flush();
            NLog.LogManager.Shutdown();

            Assert.That(File.Exists(path));
            Assert.That(File.Exists(path_debug));

            string content = File.ReadAllText(path);
            string content_debug = File.ReadAllText(path_debug);
            Assert.That(!content_debug.Contains("Test_Log_Trace" + time));
            Assert.That(content_debug.Contains("Test_Log_Debug" + time));
            Assert.That(content.Contains("Test_Log_Info" + time));
            Assert.That(content.Contains("Test_Log_Warn" + time));
            Assert.That(content.Contains("Test_Log_Error" + time));
            Assert.That(content.Contains("Test_Log_Fatal" + time));
        }
    }
}
