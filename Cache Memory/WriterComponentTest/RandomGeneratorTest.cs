using ModelsAndProps.Historical;
using ModelsAndProps.ValueStructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriterComponent;

namespace WriterComponentTest
{
    [TestFixture]
    public class RandomGeneratorTest
    {
        Mock<RandomGenerator> randomGeneratorMock;
        Mock<List<HistoricalProperty>> hp;
        Mock<HistoricalProperty> hpp;
        Mock<Value> valueMock;

        [SetUp]
        public void SetUp()
        {
            randomGeneratorMock = new Mock<RandomGenerator>();
            hp = new Mock<List<HistoricalProperty>>();
            hpp = new Mock<HistoricalProperty>();
            hpp.Object.Code = Codes.CODE_ANALOG;
            valueMock = new Mock<Value>();
            valueMock.Object.Consumption = 10.0;
            valueMock.Object.GeographicalLocationId = Guid.NewGuid().ToString();
            valueMock.Object.Timestamp = DateTime.Now;
            hpp.Object.HistoricalValue = valueMock.Object;
        }

        [Test]
        public void GetRandomHistoricalPropertyForUpdateOrRemoveNull()
        {
            Assert.IsNull(randomGeneratorMock.Object.getRandomPropertyForUpdateOrRemove(hp.Object));
        }

        [Test]
        public void GetRandomHistoricalPropertyForUpdateOrRemoveNotNull()
        {
            hp.Object.Add(hpp.Object);
            Assert.IsNotNull(randomGeneratorMock.Object.getRandomPropertyForUpdateOrRemove(hp.Object));
        }

        [Test]
        public void GenerateRandomOperationOk()
        {
            Assert.LessOrEqual((int)randomGeneratorMock.Object.GenerateRandomOperation(), 2);
            Assert.GreaterOrEqual((int)randomGeneratorMock.Object.GenerateRandomOperation(), 0);
        }

        [Test]
        public void GenerateRandomCodeOk()
        {
            Assert.LessOrEqual((int)randomGeneratorMock.Object.GenerateRandomCode(), 9);
            Assert.GreaterOrEqual((int)randomGeneratorMock.Object.GenerateRandomCode(), 0);
        }


        [Test]
        public void GenerateRandomValueOk()
        {
            Assert.IsNotNull(randomGeneratorMock.Object.RandomNewValueGenerator());
        }
    }
}
