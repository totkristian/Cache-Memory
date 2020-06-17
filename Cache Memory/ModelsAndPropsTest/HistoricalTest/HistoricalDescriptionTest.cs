using ModelsAndProps.Historical;
using NUnit.Framework;
using System;

namespace ModelsAndPropsTest.HistoricalTest
{
    [TestFixture]
    public class HistoricalDescriptionTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void ConstructorGoodParameters(int dataset)
        {
            HistoricalDescription hd = new HistoricalDescription(dataset);
            Assert.AreEqual(hd.Dataset, dataset);
        }
        [Test]
        [TestCase(null)]
        [TestCase(6)]
        [TestCase(0)]
        public void ConstructorBadParameters(int dataset)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                HistoricalDescription hd = new HistoricalDescription(dataset);
            });
        }
    }
}
