using Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace vz.UnitTest.Extensions
{
    [TestClass]
    public class MappingExtensionsTests
    {
        private class Source
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Value { get; set; }
            public string Description { get; set; }
            public string CaseInsensitive { get; set; }
            public string ToStringProperty { get; set; }
        }

        private class Destination
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Value { get; set; }
            public string Description { get; set; }
            public string CaseInsensitive { get; set; }
            public string ToStringProperty { get; set; }
        }

        private class PartialDestination
        {
            public string Name { get; set; }
            public string CaseInsensitive { get; set; }
            public string ToStringProperty { get; set; }
            public string NonMatchingProperty { get; set; }
        }

        [TestMethod]
        public void MapTo_SameNameAndTypeCaseSensitive()
        {
            // Arrange
            var source = new Source { Id = 1, Name = "Test", Value = 10.5, Description = "Sample Description" };

            // Act
            var result = source.MapTo<Source, Destination>();

            // Assert
            Assert.AreEqual(source.Id, result.Id);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Value, result.Value);
            Assert.AreEqual(source.Description, result.Description);
        }

        [TestMethod]
        public void MapTo_SameNameAndTypeCaseInsensitive()
        {
            // Arrange
            var source = new Source { CaseInsensitive = "InsensitiveMatch" };

            // Act
            var result = source.MapTo<Source, PartialDestination>();

            // Assert
            Assert.AreEqual(source.CaseInsensitive, result.CaseInsensitive);
        }

        [TestMethod]
        public void MapTo_SameNameCaseSensitive_ToString()
        {
            // Arrange
            var source = new Source { ToStringProperty = "StringValue" };

            // Act
            var result = source.MapTo<Source, PartialDestination>();

            // Assert
            Assert.AreEqual(source.ToStringProperty, result.ToStringProperty);
        }

        [TestMethod]
        public void MapTo_SameNameCaseInsensitive_ToString()
        {
            // Arrange
            var source = new Source { ToStringProperty = "AnotherValue" };

            // Act
            var result = source.MapTo<Source, PartialDestination>();

            // Assert
            Assert.AreEqual(source.ToStringProperty, result.ToStringProperty);
        }

        [TestMethod]
        public void MapTo_NoMatchingProperties()
        {
            // Arrange
            var source = new Source { Id = 1, Name = "Test" };

            // Act
            var result = source.MapTo<Source, PartialDestination>();

            // Assert
            Assert.IsNull(result.NonMatchingProperty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MapTo_NullSource_ThrowsException()
        {
            // Arrange
            Source source = null;

            // Act
            source.MapTo<Source, Destination>();

            // Assert is handled by ExpectedException
        }
    }
}
