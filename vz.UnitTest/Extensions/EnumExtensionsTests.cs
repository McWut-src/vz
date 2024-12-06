// File: EnumExtensionsTests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vz.Extensions;

namespace vz.UnitTest.Extensions
{
    [TestClass]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void In_ReturnsFalse_WhenNoValuesProvided()
        {
            // Arrange
            TestEnum value = TestEnum.Value1;

            // Act
            bool result = value.In();

            // Assert
            Assert.IsFalse(result, "Expected In() to return false when no values are provided.");
        }

        [TestMethod]
        public void In_ReturnsFalse_WhenValueIsNotInSet()
        {
            // Arrange
            TestEnum value = TestEnum.Value1;

            // Act
            bool result = value.In(TestEnum.Value2, TestEnum.Value3);

            // Assert
            Assert.IsFalse(result, "Expected In() to return false when the value is not in the set.");
        }

        [TestMethod]
        public void In_ReturnsTrue_WhenValueIsInSet()
        {
            // Arrange
            TestEnum value = TestEnum.Value2;

            // Act
            bool result = value.In(TestEnum.Value1, TestEnum.Value2, TestEnum.Value3);

            // Assert
            Assert.IsTrue(result, "Expected In() to return true when the value is in the set.");
        }

        private enum TestEnum
        {
            Value1,

            Value2,

            Value3
        }
    }
}