using Common;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Tests.Config
{
    public class TestConfig
    {
        public string Name { get; set; }
    }

    [TestFixture]
    public class ConfigManagerTest
    {
        private Mock<IConfigSerializer> _mockSerializer = new Mock<IConfigSerializer>();
        private ConfigManager _manager;
        private string _tempFile;

        [SetUp]
        public void SetUp()
        {
            _tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".ini");
            _manager = new ConfigManager(_mockSerializer.Object, _tempFile);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_tempFile)) File.Delete(_tempFile);
        }

        /// <summary>
        /// 测试是否可以正确加载文本
        /// </summary>
        [Test]
        public void LoadConfig_Test()
        {
            File.WriteAllText(_tempFile, "fake-content");
            var expected = new TestConfig { Name = "Loaded" };
            _mockSerializer.Setup(s => s.Deserialize<Test欸之Config>("fake-content")).Returns(expected);

            var result = _manager.LoadConfig<TestConfig>();
            Assert.That(result.Name, Is.EqualTo("Loaded"));
        }

        /// <summary>
        /// 测试是否可以正确写入文本
        /// </summary>
        [Test]
        public void SaveConfig_Test()
        {
            var testObj = new TestConfig { Name = "Test" };
            _mockSerializer.Setup(s => s.Serialize(testObj)).Returns(testObj.Name);

            _manager.SaveConfig(testObj);

            Assert.That(File.ReadAllText(_tempFile), Is.EqualTo(testObj.Name));
        }
    }
}
