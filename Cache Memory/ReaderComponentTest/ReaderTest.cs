using ModelsAndProps.Historical;
using ModelsAndProps.ValueStructure;
using Moq;
using NUnit.Framework;
using ReaderComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderComponentTest
{
    [TestFixture]
    public class ReaderTest
    {
        Mock<Reader> readerMock;
        Mock<Value> valueMock;

        [SetUp]
        public void SetUp()
        {
            readerMock = new Mock<Reader>();
            valueMock = new Mock<Value>();
            valueMock.Object.Consumption = 10.0;
            valueMock.Object.GeographicalLocationId = Guid.NewGuid().ToString();
            valueMock.Object.Timestamp = DateTime.Now;
        }


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
        public void ReadCodeGoodParameters(Codes code)
        {
            Mock<ListDescription> list = new Mock<ListDescription>();
            Mock<HistoricalDescription> hd = new Mock<HistoricalDescription>();
            Mock<HistoricalProperty> hp = new Mock<HistoricalProperty>();
            hp.Object.Code = Codes.CODE_ANALOG;
            hp.Object.HistoricalValue = valueMock.Object;
            hd.Object.Id = 1;
            hd.Object.HistoricalProperties.Add(hp.Object);
            list.Object.Id = 1;
            list.Object.HistoricalDescriptions.Add(hd.Object);
            Assert.DoesNotThrow(() =>
            {
                readerMock.Object.ReadCode(code, list.Object);
            });
        }

        [Test]
        [TestCase(-1)]
        [TestCase(15)]
        [TestCase(10)]
        public void ReadCodeBadParameters(Codes code)
        {
            Mock<ListDescription> list = new Mock<ListDescription>();
            Mock<HistoricalDescription> hd = new Mock<HistoricalDescription>();
            Mock<HistoricalProperty> hp = new Mock<HistoricalProperty>();
            hp.Object.Code = Codes.CODE_ANALOG;
            hp.Object.HistoricalValue = valueMock.Object;
            hd.Object.Id = 1;
            hd.Object.HistoricalProperties.Add(hp.Object);
            list.Object.Id = 1;
            list.Object.HistoricalDescriptions.Add(hd.Object);
            Assert.Throws<ArgumentException>(() =>
            {
                readerMock.Object.ReadCode(code, list.Object);
            });
        }

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
        public void ReadCodeBadParameters1(Codes code)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                readerMock.Object.ReadCode(code, null);
            });
        }

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
        public void GetChangesForIntervalGoodParameters(Codes code)
        {
            Assert.DoesNotThrow(() =>
            {
                readerMock.Object.GetChangesForInterval(code);
            });
        }

        [Test]
        [TestCase(-1)]
        [TestCase(15)]
        [TestCase(10)]
        public void getChangesForIntervalBadParameters(Codes code)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                readerMock.Object.GetChangesForInterval(code);
            });
        }





    }
}
