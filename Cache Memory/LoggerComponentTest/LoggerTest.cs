using LoggerComponent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerComponentTest
{
    [TestFixture]
    public class LoggerTest
    {
        [Test]
        [TestCase("message","className", "method")]
        public void GoodLog(string msg, string className, string method) {
            Assert.DoesNotThrow(() =>
            {
                Logger.WriteLog(msg, className, method);
            });
        }
        [Test]
        [TestCase(null,"className","method")]
        [TestCase(null,null,"method")]
        [TestCase(null,null,null)]
        [TestCase(null,"className",null)]
        [TestCase(null,null,"method")]
        [TestCase("Message",null,"method")]
        [TestCase("Message",null,null)]
        public void ArgumentExceptionLog(string msg, string className, string method)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Logger.WriteLog(msg, className, method);
            });
        }
        [Test]
        public void GoodReadLog()
        {
            Assert.DoesNotThrow(()=>
            {
                Logger.ReadLog();
            });
        }
    }
}
