using Microsoft.VisualStudio.TestTools.UnitTesting;
using vz.Extensions;

namespace vz.UnitTest.Extensions
{
    [TestClass]
    public class MappingExtensionsTests
    {
        [TestMethod]
        public void MapTo_NoMatchingProperties()
        {
            // Arrange
            Source source = new Source { Id = 1, Name = "Test" };

            // Act
            PartialDestination result = source.MapTo<Source, PartialDestination>();

            // Assert
            Assert.IsNull(result.NonMatchingProperty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MapTo_NullSource_ThrowsException()
        {
            // Arrange
            Source? source = null;

            // Act
            _ = source.MapTo<Source, Destination>();

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void MapTo_SameNameAndTypeCaseInsensitive()
        {
            // Arrange
            Source source = new Source { CaseInsensitive = "InsensitiveMatch" };

            // Act
            PartialDestination result = source.MapTo<Source, PartialDestination>();

            // Assert
            Assert.AreEqual(source.CaseInsensitive, result.CaseInsensitive);
        }

        [TestMethod]
        public void MapTo_SameNameAndTypeCaseSensitive()
        {
            // Arrange
            Source source = new Source { Id = 1, Name = "Test", Value = 10.5, Description = "Sample Description" };

            // Act
            Destination result = source.MapTo<Source, Destination>();

            // Assert
            Assert.AreEqual(source.Id, result.Id);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Value, result.Value);
            Assert.AreEqual(source.Description, result.Description);
        }

        [TestMethod]
        public void MapTo_SameNameCaseInsensitive_ToString()
        {
            // Arrange
            Source source = new Source { ToStringProperty = "AnotherValue" };

            // Act
            PartialDestination result = source.MapTo<Source, PartialDestination>();

            // Assert
            Assert.AreEqual(source.ToStringProperty, result.ToStringProperty);
        }

        [TestMethod]
        public void MapTo_SameNameCaseSensitive_ToString()
        {
            // Arrange
            Source source = new Source { ToStringProperty = "StringValue" };

            // Act
            PartialDestination result = source.MapTo<Source, PartialDestination>();

            // Assert
            Assert.AreEqual(source.ToStringProperty, result.ToStringProperty);
        }

        private class Destination
        {
            public string? CaseInsensitive { get; set; }

            public string? Description { get; set; }

            public int Id { get; set; }

            public string? Name { get; set; }

            public string? ToStringProperty { get; set; }

            public double Value { get; set; }
        }

        private class PartialDestination
        {
            public string? CaseInsensitive { get; set; }

            public string? Name { get; set; }

            public string? NonMatchingProperty { get; set; }

            public string? ToStringProperty { get; set; }
        }

        private class Source
        {
            public string? CaseInsensitive { get; set; }

            public string? Description { get; set; }

            public int Id { get; set; }

            public string? Name { get; set; }

            public string? ToStringProperty { get; set; }

            public double Value { get; set; }
        }
    }
}