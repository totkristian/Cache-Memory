using HistoricalComponent;
using ModelsAndProps.Dumping_buffer;
using ModelsAndProps.ValueStructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace HistoricalComponentTest
{
    [TestFixture]
    public class HistoricalTest
    {
        Mock<Historical> historyMock;
        Mock<CollectionDescription> cdMock;
        Mock<Value> valueMock;
        Mock<DeltaCD> deltaCDMock;
        [SetUp]
        public void SetUp()
        {
            historyMock = new Mock<Historical>();
            cdMock = new Mock<CollectionDescription>();
            valueMock = new Mock<Value>();
            valueMock.Object.Consumption = 10.0;
            valueMock.Object.GeographicalLocationId = Guid.NewGuid().ToString();
            valueMock.Object.Timestamp = DateTime.Now;
            deltaCDMock = new Mock<DeltaCD>();
        }
        [Test]
        [TestCase(null)]
        [TestCase("       ")]
        public void CheckIfIdIsUniqueBadParameters(string id)
        {
            Assert.IsTrue(historyMock.Object.CheckIfIdIsUnique(id));
        }
        #region checkIfTheresDataInCollection
        [Test]
        public void checkIfTheresDataInCollectionDescriptionGoodParameters()
        {
            cdMock.Object.Dataset = 1;
            cdMock.Object.Id = 1;
            cdMock.Object.DumpingPropertyCollection.DumpingProperties.Add(new DumpingProperty(Codes.CODE_ANALOG, valueMock.Object));
            Assert.IsTrue(historyMock.Object.checkIfTheresDataInCollectionDescription(cdMock.Object));
        }

        [Test]
        public void checkIfTheresDataInCollectionDescriptionBadParameters()
        {
            cdMock.Object.Dataset = 1;
            cdMock.Object.Id = 1;
            Assert.IsFalse(historyMock.Object.checkIfTheresDataInCollectionDescription(cdMock.Object));
        }
        [Test]
        public void checkIfTheresDataInCollectionDescriptionBadParameters1()
        {
            cdMock.Object.Dataset = 1;
            Assert.IsFalse(historyMock.Object.checkIfTheresDataInCollectionDescription(cdMock.Object));
        }
        [Test]
        public void checkIfTheresDataInCollectionDescriptionBadParameters2()
        {
            Assert.IsFalse(historyMock.Object.checkIfTheresDataInCollectionDescription(cdMock.Object));
        }
        [Test]
        public void checkIfTheresDataInCollectionDescriptionBadParameters3()
        {
            Assert.IsFalse(historyMock.Object.checkIfTheresDataInCollectionDescription(null));
        }
        #endregion

        #region readFromDumpingBuffer
        [Test]
        public void ReadFromDumpingBufferGoodParameters()
        {
            deltaCDMock.Object.Add = new Dictionary<int, CollectionDescription>();
            deltaCDMock.Object.Update = new Dictionary<int, CollectionDescription>();
            deltaCDMock.Object.Remove = new Dictionary<int, CollectionDescription>();
            deltaCDMock.Object.TransactionID = Guid.NewGuid().ToString();

            for (int i = 1; i < 6; i++)
            {
                deltaCDMock.Object.Add.Add(i, new CollectionDescription());
                deltaCDMock.Object.Remove.Add(i, new CollectionDescription());
                deltaCDMock.Object.Update.Add(i, new CollectionDescription());
            }
            Assert.DoesNotThrow(() =>
            {
                historyMock.Object.ReadFromDumpingBuffer(deltaCDMock.Object);
            });

        }
        [Test]
        public void ReadFromDumpingBufferBadParameters() {
            deltaCDMock.Object.Add = new Dictionary<int, CollectionDescription>();
            deltaCDMock.Object.Update = new Dictionary<int, CollectionDescription>();
            deltaCDMock.Object.Remove = new Dictionary<int, CollectionDescription>();
            deltaCDMock.Object.TransactionID = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                historyMock.Object.ReadFromDumpingBuffer(deltaCDMock.Object);
            });
        }
        [Test]
        public void ReadFromDumpingBufferBadParameters1()
        {
            deltaCDMock.Object.Add = new Dictionary<int, CollectionDescription>();
            deltaCDMock.Object.Update = new Dictionary<int, CollectionDescription>();
            deltaCDMock.Object.Remove = null;
            deltaCDMock.Object.TransactionID = Guid.NewGuid().ToString();
            Assert.Throws<ArgumentNullException>(() =>
            {
                historyMock.Object.ReadFromDumpingBuffer(deltaCDMock.Object);
            });
        }
        [Test]
        public void ReadFromDumpingBufferBadParameters2()
        {
            deltaCDMock.Object.Add = new Dictionary<int, CollectionDescription>();
            deltaCDMock.Object.Update = null;
            deltaCDMock.Object.Remove = new Dictionary<int, CollectionDescription>();
            deltaCDMock.Object.TransactionID = Guid.NewGuid().ToString();
            Assert.Throws<ArgumentNullException>(() =>
            {
                historyMock.Object.ReadFromDumpingBuffer(deltaCDMock.Object);
            });
        }
        [Test]
        public void ReadFromDumpingBufferBadParameters3()
        {
            deltaCDMock.Object.Add = null;
            deltaCDMock.Object.Update = new Dictionary<int, CollectionDescription>();
            deltaCDMock.Object.Remove = new Dictionary<int, CollectionDescription>();
            deltaCDMock.Object.TransactionID = Guid.NewGuid().ToString();
            Assert.Throws<ArgumentNullException>(() =>
            {
                historyMock.Object.ReadFromDumpingBuffer(deltaCDMock.Object);
            });
        }
        [Test]
        public void ReadFromDumpingBufferBadParameters4()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                historyMock.Object.ReadFromDumpingBuffer(null);
            });
        }
        #endregion

        #region ReadOneLDFromDB
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void ReadOneLDFromDBGoodParameters(int dataset)
        {
            Assert.DoesNotThrow(() =>
            {
                historyMock.Object.ReadOneLDFromDB(dataset);
            });
        }

        [Test]
        [TestCase(0)]
        [TestCase(6)]
        public void ReadOneLDFromDBBadParameters(int dataset)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                historyMock.Object.ReadOneLDFromDB(dataset);
            });
        }

        #endregion

        #region manualWriteToHistory
       /* [Test]
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
        public void ManualWriteToHistoryGoodParameters(Codes code)
        {
            Assert.DoesNotThrow(() =>
            {
                historyMock.Object.ManualWriteToHistory(code, valueMock.Object);
            }); 
        } */
        [Test]
        [TestCase(-1)]
        [TestCase(-9)]
        public void ManualWriteToHistoryBadParameters(Codes code)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                historyMock.Object.ManualWriteToHistory(code, valueMock.Object);
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
        public void ManualWriteToHistoryBadParameters1(Codes code)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                historyMock.Object.ManualWriteToHistory(code, null);
            });
        }

        #endregion

        #region checkDataset
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
        public void CheckDatasetGoodParameters(Codes code)
        {
            Assert.DoesNotThrow(() =>
            {
                historyMock.Object.CheckDataset(code);
            });
            int dataset = historyMock.Object.CheckDataset(code);
            if ((int)code == 0 || (int)code == 1)
            {
                Assert.AreEqual(dataset, 1);
            }
            else if ((int)code == 2 || (int)code == 3)
            {
                Assert.AreEqual(dataset, 2);
            }
            else if ((int)code == 4 || (int)code == 5)
            {
                Assert.AreEqual(dataset, 3);
            }
            else if ((int)code == 6 || (int)code == 7)
            {
                Assert.AreEqual(dataset, 4);
            }
            else
            {
                Assert.AreEqual(dataset, 5);
            }
        }
        [Test]
        [TestCase(-5)]
        [TestCase(15)]
        public void CheckDatasetBadParameters(Codes code)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                historyMock.Object.CheckDataset(code);
            });
        }
        #endregion
    }
}
