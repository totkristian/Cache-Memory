using DumpingBufferComponent;
using ModelsAndProps.Dumping_buffer;
using ModelsAndProps.ValueStructure;
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
    public class DumpingBufferTest
    {
        DumpingBuffer db;
        Mock<Dictionary<int, CollectionDescription>> collectionDescriptionsMock;
        Mock<Dictionary<int, List<Operations>>> operationAndIdMock;
        Mock<DeltaCD> deltaCDMock;
        Mock<Value> valueMock;

        [SetUp]
        public void SetUp()
        {
            db = DumpingBuffer.GetInstance();
            collectionDescriptionsMock = new Mock<Dictionary<int, CollectionDescription>>();
            operationAndIdMock = new Mock<Dictionary<int, List<Operations>>>();
            deltaCDMock = new Mock<DeltaCD>();
            valueMock = new Mock<Value>();
            valueMock.Object.Consumption = 10.0;
            valueMock.Object.GeographicalLocationId = Guid.NewGuid().ToString();
            valueMock.Object.Timestamp = DateTime.Now;

            for (int i = 1; i < 6; i++)
            {
                collectionDescriptionsMock.Object.Add(i, new CollectionDescription());
                operationAndIdMock.Object.Add(i, new List<Operations>());
            }
        }

        #region WriteToDumpingBuffer
        [Test]
        [TestCase(Operations.ADD,Codes.CODE_ANALOG)]
        [TestCase(Operations.UPDATE, Codes.CODE_CONSUMER)]
        [TestCase(Operations.ADD, Codes.CODE_CUSTOM)]
        [TestCase(Operations.ADD, Codes.CODE_DIGITAL)]
        [TestCase(Operations.REMOVE, Codes.CODE_LIMITSET)]
        public void WriteToDumpingBufferGoodParameters(Operations op, Codes code)
        {
           
            Assert.DoesNotThrow(() =>
            {
                db.WriteToDumpingBuffer(op, code, valueMock.Object);
            });
        }

        [Test]
        [TestCase(-1,15)]
        [TestCase(-5,10)]
        [TestCase(3,-5)]
        [TestCase(5,-1)]

        public void WriteToDumpingBufferBadParameters(Operations op, Codes code)
        {
           
            Assert.Throws<ArgumentException>(() =>
            {
                db.WriteToDumpingBuffer(op,code,valueMock.Object);
            });
        }
        [Test]
        [TestCase(-1, 15)]
        [TestCase(-5, 10)]
        [TestCase(3, -5)]
        [TestCase(5, -1)]
        [TestCase(null, null)]
        public void WriteToDumpingBufferBadParameters1(Operations op, Codes code)
        {
            
            Assert.Throws<ArgumentNullException>(() =>
            {
                db.WriteToDumpingBuffer(op, code, null);
            });
        }
        #endregion
        
        #region AddOperationsAndId
        [Test]
        [TestCase(1,Operations.ADD)]
        [TestCase(2, Operations.REMOVE)]
        [TestCase(3, Operations.UPDATE)]
        [TestCase(4, Operations.ADD)]
        [TestCase(5, Operations.REMOVE)]
        public void AddOperationAndIdGoodParameters(int id, Operations operation)
        {
            Assert.DoesNotThrow(() =>
            {
                db.AddToOperationsAndId(id, operation);
            });
        }
        [Test]
        [TestCase(0, Operations.ADD)]
        [TestCase(-1, -5)]
        [TestCase(6, 6)]
        [TestCase(8, 3)]
        [TestCase(-5, Operations.REMOVE)]
        public void AddOperationAndIdBadParameters(int id, Operations operation)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                db.AddToOperationsAndId(id, operation);
            });
        }

        #endregion

        #region checkUpdate
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void CheckUpdateGoodParameters(int dataset)
        {
            Mock<DumpingProperty> dp = new Mock<DumpingProperty>();
            dp.Object.Code = Codes.CODE_ANALOG;
            dp.Object.DumpingValue = valueMock.Object;
            Assert.DoesNotThrow(() =>
            {
                db.CheckUpdate(dataset, dp.Object);
            });
        }
        [Test]
        [TestCase(0)]
        [TestCase(6)]
        [TestCase(-5)]
        public void CheckUpdateBadParameters(int dataset)
        {
            Mock<DumpingProperty> dp = new Mock<DumpingProperty>();
            dp.Object.Code = Codes.CODE_ANALOG;
            dp.Object.DumpingValue = valueMock.Object;
            Assert.Throws<ArgumentException>(() =>
            {
                db.CheckUpdate(dataset, dp.Object);
            });
        }
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void CheckUpdateBadParameters1(int dataset)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                db.CheckUpdate(dataset, null);
            });
        }

        #endregion

        #region checkDumpingPropertyCount
        [Test]
        public void CheckDumpingPropertyTrueFalse()
        {
            db.WriteToDumpingBuffer(Operations.ADD, Codes.CODE_ANALOG, valueMock.Object);
            Assert.IsFalse(db.CheckDumpingPropertyCount());
            Mock<Value>valueMock1 = new Mock<Value>();
            valueMock1.Object.Consumption = 15.0;
            valueMock1.Object.GeographicalLocationId = Guid.NewGuid().ToString();
            valueMock1.Object.Timestamp = DateTime.Now;
            Assert.Throws<ArgumentNullException>(() =>
            {
                db.WriteToDumpingBuffer(Operations.ADD, Codes.CODE_ANALOG, valueMock1.Object);
            });
        }

        #endregion
    }
}
