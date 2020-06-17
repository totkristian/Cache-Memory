using ModelsAndProps.Dumping_buffer;
using NUnit.Framework;

namespace ModelsAndPropsTest.DumpingBufferTest
{
    [TestFixture]
    public class DeltaCDTest
    {
        [Test]
        public void Constructor()
        {
            DeltaCD deltaCD = new DeltaCD();
            Assert.IsTrue(deltaCD.Add.Count == 5);
            Assert.IsTrue(deltaCD.Update.Count == 5);
            Assert.IsTrue(deltaCD.Remove.Count == 5);
            Assert.IsNotNull(deltaCD.TransactionID);
            Assert.IsNotEmpty(deltaCD.TransactionID);
        }
    }
}
