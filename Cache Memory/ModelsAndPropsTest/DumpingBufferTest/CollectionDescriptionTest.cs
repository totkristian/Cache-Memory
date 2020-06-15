using ModelsAndProps.Dumping_buffer;
using NUnit.Framework;
using System;

namespace ModelsAndPropsTest.DumpingBufferTest
{
    [TestFixture]
    public class CollectionDescriptionTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void ConstructorGoodParameters(int dataset)
        {
            CollectionDescription cd = new CollectionDescription(dataset);
            Assert.AreEqual(cd.Dataset, dataset);
        }

        [Test]
        [TestCase(0)]
        [TestCase(6)]
        public void ConstructorBadParameters(int dataset)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                CollectionDescription cd = new CollectionDescription(dataset);
            });

        }
    }
}
