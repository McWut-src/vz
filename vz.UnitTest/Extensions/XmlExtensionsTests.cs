using Microsoft.VisualStudio.TestTools.UnitTesting;
using vz.Extensions;

namespace vz.UnitTest.Extensions
{
    [TestClass]
    public class XmlExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToXml_NullSource_ThrowsArgumentNullException()
        {
            // Arrange & Act
            IEnumerable<TestClass>? nullItems = null;
            _ = nullItems.ToXml();

            // Assert will check for the exception
        }

        [TestMethod]
        public void ToXml_WithEmptyList_ReturnsValidEmptyRoot()
        {
            // Arrange
            List<TestClass> emptyItems = [];

            // Act
            string xml = emptyItems.ToXml();

            // Assert
            Assert.IsTrue(xml.Contains("<root />"));
        }

        [TestMethod]
        public void ToXml_WithObjectHavingNullProperties()
        {
            // Arrange
            List<TestClass> items =
            [
                new TestClass { Id = 1, Name = null },  // Name is null
                new TestClass { Id = 0, Name = "Test" } // Id is 0, which might be treated as empty by some
            ];

            // Act
            string xml = items.ToXml();

            // Assert
            Assert.IsNotNull(xml);
            Assert.IsTrue(xml.Contains("<root>"));
            Assert.IsTrue(xml.Contains("</root>"));
            Assert.IsTrue(xml.Contains("<TestClass>"));
            Assert.IsTrue(xml.Contains("<Id>1</Id>"));
            Assert.IsTrue(xml.Contains("<Name></Name>")); // Name element is empty
            Assert.IsTrue(xml.Contains("<Id>0</Id>"));
            Assert.IsTrue(xml.Contains("<Name>Test</Name>"));
        }

        [TestMethod]
        public void ToXml_WithValidData_ReturnsXmlString()
        {
            // Arrange
            List<TestClass> items =
            [
                new TestClass { Id = 1, Name = "Test1" },
                new TestClass { Id = 2, Name = "Test2" }
            ];

            // Act
            string xml = items.ToXml();

            // Assert
            Assert.IsNotNull(xml);
            Assert.IsTrue(xml.Contains("<root>"));
            Assert.IsTrue(xml.Contains("</root>"));
            Assert.IsTrue(xml.Contains("<TestClass>"));
            Assert.IsTrue(xml.Contains("</TestClass>"));
            Assert.IsTrue(xml.Contains("<Id>1</Id>"));
            Assert.IsTrue(xml.Contains("<Id>2</Id>"));
            Assert.IsTrue(xml.Contains("<Name>Test1</Name>"));
            Assert.IsTrue(xml.Contains("<Name>Test2</Name>"));
        }

        // TestClass for testing purposes
        private class TestClass
        {
            public int Id { get; set; }

            public string? Name { get; set; }
        }
    }
}