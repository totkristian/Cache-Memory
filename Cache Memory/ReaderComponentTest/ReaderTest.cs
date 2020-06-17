using ModelsAndProps.ValueStructure;
using Moq;
using NUnit.Framework;
using ReaderComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderComponentTest
{
    [TestFixture]
    public class ReaderTest
    {
        Mock<Reader> readerMock;
        Mock<Value> valueMock;

        [SetUp]
        public void SetUp()
        {
            readerMock = new Mock<Reader>();
            valueMock = new Mock<Value>();
            valueMock.Object.Consumption = 10.0;
            valueMock.Object.GeographicalLocationId = Guid.NewGuid().ToString();
            valueMock.Object.Timestamp = DateTime.Now;
        }
    }
}
