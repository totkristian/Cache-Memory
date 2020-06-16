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
    public class ConverterFromCdToHistoricalTest
    {
        Mock<Value> value = new Mock<Value>();
        Mock<ConverterFromCdToHistorical> converter = new Mock<ConverterFromCdToHistorical>();
        [SetUp]
        public void SetUp()
        {
            value.Object.Consumption = 10.0;
            value.Object.GeographicalLocationId = Guid.NewGuid().ToString();
            value.Object.Timestamp = DateTime.Now;
        }
        [Test]
        public void ConvertCollectionDescriptionGoodParameters()
        {
            Mock<CollectionDescription> cd = new Mock<CollectionDescription>();
            cd.Object.Dataset = 1;
            cd.Object.Id = 1;
            cd.Object.DumpingPropertyCollection.DumpingProperties.Add(new DumpingProperty(Codes.CODE_ANALOG, value.Object));
            ConverterFromCdToHistorical conv = converter.Object;
            Assert.DoesNotThrow(() =>
            {
                conv.ConvertCollectionDescription(cd.Object);
            });
        }
        [Test]
        public void ConvertCollectionDescriptionBadParameters()
        {
            ConverterFromCdToHistorical conv = converter.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                conv.ConvertCollectionDescription(null);
            });
        }
    }
}
