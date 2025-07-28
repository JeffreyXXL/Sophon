using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Tests.Config.Serializer
{
    public class StepTestClass
    {
        public string Name { get; set; }
        public int Delay { get; set; }
        public bool Enabled { get; set; }
    }


    [TestFixture]
    public class JsonConfigSerializerTest
    {
        private JsonConfigSerializer _serializer = new JsonConfigSerializer();

        /// <summary>
        /// 测试是否可以正常序列化/反序列化
        /// </summary>
        [Test]
        public void Serialize_Deserialize_Test()
        {
            var step = new StepTestClass() { Name = "MotorMove", Delay = 10, Enabled = true };

            var jsoncontent = _serializer.Serialize(step);
            var deserialized = _serializer.Deserialize<StepTestClass>(jsoncontent);

            Assert.That(deserialized.Name == "MotorMove");
            Assert.That(deserialized.Delay == 10);
            Assert.That(deserialized.Enabled == true);
        }

        /// <summary>
        /// 测试是否可以正常抛出异常
        /// </summary>
        [Test]
        public void Serialize_Deserialize_Throw_Test()
        {
            Assert.Throws<ConfigDeserializeException>(() => _serializer.Deserialize<StepTestClass>("123123"));
        }
    }
}
