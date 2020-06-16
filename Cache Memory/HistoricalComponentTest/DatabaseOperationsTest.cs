using HistoricalComponent;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalComponentTest
{
    [TestFixture]
    public class DatabaseOperationsTest
    {
        
        Mock<DatabaseOperations> dataMock;

        [SetUp]
        public void SetUp()
        {
            dataMock = new Mock<DatabaseOperations>();
        }
        #region checkGeoId
        [Test]
        [TestCase("ASDASDAS")]
        [TestCase("AAAA")]
        [TestCase("BBB")]
        public void CheckGeoIdGoodParameters(string id)
        {
            Assert.DoesNotThrow(() =>
            {
                dataMock.Object.CheckGeoId(id);
            });
        }
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("       ")]
        public void CheckGeoIdBadParameters(string id)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                dataMock.Object.CheckGeoId(id);
            });
        }
        #endregion

        #region checkDeadBand

        #endregion
    }
}
