// File: EnumExtensionsTests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using vz.Extensions;

namespace vz.UnitTest.Extensions
{
    [TestClass]
    public class EnumExtensionsTests
    {
        private enum TestEnum
        {
            Value1,
            Value2,
            Value3
        }

        [TestMethod]
        public void In_ReturnsTrue_WhenValueIsInSet()
        {
            // Arrange
            var value = TestEnum.Value2;

            // Act
            bool result = value.In(TestEnum.Value1, TestEnum.Value2, TestEnum.Value3);

            // Assert
            Assert.IsTrue(result, "Expected In() to return true when the value is in the set.");
        }

        [TestMethod]
        public void In_ReturnsFalse_WhenValueIsNotInSet()
        {
            // Arrange
            var value = TestEnum.Value1;

            // Act
            bool result = value.In(TestEnum.Value2, TestEnum.Value3);

            // Assert
            Assert.IsFalse(result, "Expected In() to return false when the value is not in the set.");
        }

        [TestMethod]
        public void In_ReturnsFalse_WhenNoValuesProvided()
        {
            // Arrange
            var value = TestEnum.Value1;

            // Act
            bool result = value.In();

            // Assert
            Assert.IsFalse(result, "Expected In() to return false when no values are provided.");
        }
    }
}
