using ModelsAndProps.Historical;
using ModelsAndProps.ValueStructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndPropsTest.HistoricalTest
{
    [TestFixture]
    public class HistoricalPropertyTest
    {
        [Test]
        [TestCase(Codes.CODE_ANALOG)]
        [TestCase(Codes.CODE_CONSUMER)]
        [TestCase(Codes.CODE_CUSTOM)]
        [TestCase(Codes.CODE_DIGITAL)]
        [TestCase(Codes.CODE_LIMITSET)]
        [TestCase(Codes.CODE_MOTION)]
        [TestCase(Codes.CODE_MULTIPLENODE)]
        [TestCase(Codes.CODE_SENSOR)]
        [TestCase(Codes.CODE_SINGLENODE)]
        [TestCase(Codes.CODE_SOURCE)]
        public void ConstructorGoodParameters(Codes code)
        {
            Mock<Value> value = new Mock<Value>();
            value.Object.Consumption = 14.0;
            value.Object.Timestamp = DateTime.Now;
            value.Object.GeographicalLocationId = Guid.NewGuid().ToString();
            HistoricalProperty hp = new HistoricalProperty(code, value.Object);

            Assert.AreEqual(code, hp.Code);
            Assert.AreEqual(value.Object, hp.HistoricalValue);
        }
        [Test]
        [TestCase(-1)]
        public void ConstructorBadParameters(Codes code)
        {
            Mock<Value> value = new Mock<Value>();
            value.Object.Consumption = 14.0;
            value.Object.Timestamp = DateTime.Now;
            value.Object.GeographicalLocationId = Guid.NewGuid().ToString();
            Assert.Throws<ArgumentException>(() =>
            {
                HistoricalProperty hp = new HistoricalProperty(code, value.Object);
            });
        }


        [Test]
        [TestCase(Codes.CODE_ANALOG)]
        [TestCase(null)]
        public void ConstructorBadParameters1(Codes code)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                HistoricalProperty hp = new HistoricalProperty(code, null);
            });
        }
    }
}
