using Microsoft.VisualStudio.TestTools.UnitTesting;
using vz.Extensions;

namespace vz.UnitTest.Extensions
{
    [TestClass]
    public class CsvExtensionsTests
    {
        [TestMethod]
        public void ToCsv_WhenIncludeHeaderIsFalse_ShouldNotIncludeHeader()
        {
            // Arrange
            var items = new[]
            {
            new { Name = "Charlie", Score = 95 }
        };

            // Act
            string[] result = items.ToCsv(includeHeader: false).ToArray();

            // Assert
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("Charlie,95", result[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToCsv_WhenNullSource_ShouldThrowArgumentNullException()
        {
            // Arrange
            IEnumerable<object>? nullSource = null;

            // Act
            _ = nullSource.ToCsv().ToArray(); // Using _ to ignore result since we expect an exception
        }

        [TestMethod]
        public void ToCsv_WithBasicTypes_ShouldProduceCorrectCsv()
        {
            // Arrange
            var items = new[]
            {
            new { Name = "Alice", Age = 30 },
            new { Name = "Bob", Age = 25 }
        };

            // Act
            string[] result = items.ToCsv().ToArray();

            // Assert
            Assert.AreEqual(3, result.Length); // Header + 2 rows
            Assert.AreEqual("Name,Age", result[0]); // Header
            Assert.AreEqual("Alice,30", result[1]);
            Assert.AreEqual("Bob,25", result[2]);
        }

        [TestMethod]
        public void ToCsv_WithCommaInData_ShouldEscapeCorrectly()
        {
            // Arrange
            var items = new[]
            {
            new { Description = "Hello, World!", Value = 100 }
        };

            // Act
            string[] result = items.ToCsv().ToArray();

            // Assert
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("Description,Value", result[0]); // Header
            Assert.AreEqual("\"Hello, World!\",100", result[1]); // Escaped comma in data
        }

        [TestMethod]
        public void ToCsv_WithEmptyCollection_ShouldReturnOnlyHeader()
        {
            // Arrange
            IEnumerable<object> emptyList = Enumerable.Empty<object>();

            // Act
            string[] result = emptyList.ToCsv(includeHeader: true).ToArray();

            // Assert
            Assert.AreEqual(1, result.Length); // Only the header row
            Assert.AreEqual("", result[0]); // Adjust this based on what your method does for empty collections
        }
    }
}