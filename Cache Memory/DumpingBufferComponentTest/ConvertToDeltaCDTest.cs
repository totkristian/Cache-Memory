using DumpingBufferComponent;
using HistoricalComponent;
using ModelsAndProps.Dumping_buffer;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpingBufferComponentTest
{
    [TestFixture]
    public class ConvertToDeltaCDTest
    {
        Mock<Dictionary<int, CollectionDescription>> collectionDescriptionsMock;
        Mock<Dictionary<int, List<Operations>>> operationAndIdMock;
        Mock<ConvertToDeltaCD> converterMock;
        Mock<DeltaCD> deltaCDMock;
        [SetUp]
        public void SetUp()
        {
            collectionDescriptionsMock = new Mock<Dictionary<int, CollectionDescription>>();
            operationAndIdMock = new Mock<Dictionary<int, List<Operations>>>();
            deltaCDMock = new Mock<DeltaCD>();
            converterMock = new Mock<ConvertToDeltaCD>();
            for (int i = 1; i < 6; i++)
            {
                collectionDescriptionsMock.Object.Add(i, new CollectionDescription());
                operationAndIdMock.Object.Add(i, new List<Operations>());
            }
        }
        [Test]
        public void FillDeltaCDGoodParamaters()
        {
            Assert.DoesNotThrow(() =>
            {
                converterMock.Object.FillDeltaCD(operationAndIdMock.Object,collectionDescriptionsMock.Object);
            });
        }
        [Test]
        public void FillDeltaCDBadParamaters()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                converterMock.Object.FillDeltaCD(null, collectionDescriptionsMock.Object);
            });
        }
        [Test]
        public void FillDeltaCDBadParamaters1()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                converterMock.Object.FillDeltaCD(operationAndIdMock.Object, null);
            });
        }
        [Test]
        public void FillDeltaCDBadParamaters2()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                converterMock.Object.FillDeltaCD(null, null);
            });
        }


    }
}
