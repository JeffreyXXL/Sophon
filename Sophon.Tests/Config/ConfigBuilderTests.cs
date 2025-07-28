using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Tests.Config
{
    public class DummyConfig
    {
        public string Name { get; set; }
        public int Speed { get; set; }
    }

    [TestFixture]
    public class ConfigBuilderTests
    {
        /// <summary>
        /// 测试是否可以正常build
        /// </summary>
        [Test]
        public void BuildConfig_Test()
        {
            var builder = new ConfigBuilder();
            builder.SetValue("Name", "MotorX")
                   .SetValue("Speed", 120);

            var config = builder.BuildConfig<DummyConfig>();
            Assert.That(config is DummyConfig);
            Assert.That(config.Name, Is.EqualTo("MotorX"));
            Assert.That(config.Speed, Is.EqualTo(120));
        }
    }
}
