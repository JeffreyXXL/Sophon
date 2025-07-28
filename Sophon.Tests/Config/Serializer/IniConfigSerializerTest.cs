using Common;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Tests.Config.Serializer
{
    public class MotorTestClass
    {
        [IniConfigInstanceName]
        public string Name { get; set; }
        public int Speed { get; set; }
        public bool Enabled { get; set; }
    }

    public class NoAttributeClass
    {
        public string Name { get; set; }
    }


    [TestFixture]
    public class IniConfigSerializerTest
    {
        private IniConfigSerializer _serializer = new IniConfigSerializer();

        /// <summary>
        /// 测试是否可以正常序列化/反序列化单类
        /// </summary>
        [Test]
        public void Serialize_Deserialize_Test()
        {
            var motor = new MotorTestClass() { Name = "MotorX", Speed = 100, Enabled = true };

            var iniContent = _serializer.Serialize(motor);
            var deserialized = _serializer.Deserialize<MotorTestClass>(iniContent);

            Assert.That(deserialized.Name, Is.EqualTo("MotorX"));
            Assert.That(deserialized.Speed, Is.EqualTo(100));
            Assert.That(deserialized.Enabled, Is.EqualTo(true));
        }

        /// <summary>
        /// 测试是否可以正常序列化/反序列化集合
        /// </summary>
        [Test]
        public void Serialize_Deserialize_List_Test()
        {
            var listMotor = new List<MotorTestClass>()
            {
                new MotorTestClass() { Name = "MotorX", Speed = 100, Enabled = true },
                new MotorTestClass() { Name = "MotorY", Speed = 200, Enabled = false },
                new MotorTestClass() { Name = "MotorZ", Speed = 300, Enabled = true },
            };

            var iniContent = _serializer.Serialize(listMotor);
            var deserialized = _serializer.Deserialize<List<MotorTestClass>>(iniContent);

            Assert.That(typeof(IEnumerable).IsAssignableFrom(deserialized.GetType()));

            Assert.That(deserialized.Count, Is.EqualTo(3));
            Assert.That(deserialized[0].Name, Is.EqualTo("MotorX"));
            Assert.That(deserialized[0].Speed, Is.EqualTo(100));
            Assert.That(deserialized[0].Enabled, Is.EqualTo(true));
            Assert.That(deserialized[1].Name, Is.EqualTo("MotorY"));
            Assert.That(deserialized[1].Speed, Is.EqualTo(200));
            Assert.That(deserialized[1].Enabled, Is.EqualTo(false));
            Assert.That(deserialized[2].Name, Is.EqualTo("MotorZ"));
            Assert.That(deserialized[2].Speed, Is.EqualTo(300));
            Assert.That(deserialized[2].Enabled, Is.EqualTo(true));
        }

        /// <summary>
        /// 测试是否可以正确抛出异常
        /// </summary>
        [Test]
        public void Serialize_Deserialize_Throw_Test()
        {
            var noattr = new NoAttributeClass() { Name = "MotorX" };
            var listnoattr = new List<NoAttributeClass>()
            {
                new NoAttributeClass() { Name = "MotorX" },
                new NoAttributeClass() { Name = "MotorY" }
            };
            Assert.Throws<IniConfigException>(() => _serializer.Serialize(noattr));
            Assert.Throws<IniConfigException>(() => _serializer.Serialize(listnoattr));
        }
    }
}
