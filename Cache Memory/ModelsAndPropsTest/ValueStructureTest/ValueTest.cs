using ModelsAndProps.ValueStructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndPropsTest.ValueStructureTest
{
    [TestFixture]
    public class ValueTest
    {
        [Test]
        [TestCase("ASD",2.0)]
        [TestCase("ADDDDDDDDD",1.0)]
        [TestCase("D",0.00000000001)]
        [TestCase("X",19)]
        public void ConstructorGoodParameters(string geoId, double consumption)
        {
            var timestamp = DateTime.Now;
            Value v = new Value(timestamp,geoId, consumption);
            
            Assert.AreEqual(v.Consumption, consumption);
            Assert.AreEqual(v.GeographicalLocationId, geoId);
            Assert.AreEqual(v.Timestamp, timestamp);

        }
        [Test]
        [TestCase("ASD", null)]
        [TestCase("D", null)]
        [TestCase(null, 1.0)]
        [TestCase(null, 0.00000000001)]
        [TestCase(null, null)]
        public void ConstructorBadParameters(string geoId, double consumption)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Value v = new Value(DateTime.Now, geoId, consumption);
            });

        }
        [Test]
        [TestCase("ASD", -1.1)]
        [TestCase("X", -0.00000000001)]
        [TestCase("A", -0.1)]
        [TestCase("SD", -0.1)]
        public void ConstructorBadParameters1(string geoId, double consumption)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Value v = new Value(DateTime.Now, geoId, consumption);
            });

        }
        [Test]
        [TestCase("ASD", null)]
        [TestCase("D", null)]
        [TestCase(null, 1.0)]
        [TestCase(null, 0.00000000001)]
        [TestCase(null, null)]
        public void ConstructorBadParameters2(string geoId, double consumption)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Value v = new Value(null, geoId, consumption);
            });
        }
    }
}
