using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Tests.Config
{
    [TestFixture]
    public class ConfigManagerFactoryTest
    {
        private ConfigManagerFactory factory = new ConfigManagerFactory();

        /// <summary>
        /// 测试是否可以返回正确的序列化器
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expectedType"></param>
        [TestCase(ConfigType.ini, typeof(IniConfigSerializer))]
        [TestCase(ConfigType.json, typeof(JsonConfigSerializer))]
        [TestCase(ConfigType.xml, typeof(XmlConfigSerializer))]
        public void CreateSerializer_Test(ConfigType type, Type expectedType)
        {
            var serializer = factory.CreateSerializer(type);
            Assert.That(serializer, Is.Not.Null);
            Assert.That(serializer.GetType(), Is.EqualTo(expectedType));
        }
        /// <summary>
        /// 测试是否可以正确抛出异常
        /// </summary>
        [Test]
        public void CreateSerializer_Throw_Test()
        {
            Assert.Throws<NotSupportedException>(() => factory.CreateSerializer((ConfigType)999));
        }
    }

    [TestFixture]
    public class ConfigManagerFactory_CreateTests
    {
        private string testRoot;
        private ConfigManagerFactory factory = new ConfigManagerFactory();

        [SetUp]
        public void Setup()
        {
            //创建测试路径
            testRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(testRoot);

            //设置测试路径
            typeof(ConfigManagerFactory)
                .GetField("_configPath", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(factory, testRoot);
        }

        [TearDown]
        public void Cleanup()
        {
            if (Directory.Exists(testRoot))
            {
                Directory.Delete(testRoot, true);
            }
        }

        /// <summary>
        /// 测试是否可以在正确的位置创建正确的文件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expectedType"></param>
        /// <param name="filetype"></param>
        [TestCase(ConfigType.ini, typeof(IniConfigSerializer), "ini")]
        [TestCase(ConfigType.json, typeof(JsonConfigSerializer), "json")]
        [TestCase(ConfigType.xml, typeof(XmlConfigSerializer), "xml")]
        public void CreateConfigManager_Test(ConfigType type, Type expectedType, string filetype)
        {
            var filename = "testfile"+ type.ToString();
            var secondPath = "sub";
            var expectedPath = Path.Combine(testRoot, secondPath, filename + "." + filetype);

            var manager = factory.CreateConfigManager(type, filename, secondPath);

            Assert.That(manager, Is.TypeOf<ConfigManager>());

            var serializerField = typeof(ConfigManager)
                .GetField("_serializer", BindingFlags.NonPublic | BindingFlags.Instance);
            var pathField = typeof(ConfigManager)
                .GetField("_path", BindingFlags.NonPublic | BindingFlags.Instance);

            var serializer = serializerField.GetValue(manager);
            var fullPath = pathField.GetValue(manager) as string;

            Assert.That(serializer.GetType(), Is.EqualTo(expectedType));
            Assert.That(fullPath, Is.EqualTo(expectedPath));
        }
    }
}
