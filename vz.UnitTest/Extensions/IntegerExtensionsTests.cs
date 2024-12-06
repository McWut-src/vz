using Microsoft.VisualStudio.TestTools.UnitTesting;
using vz.Extensions;

namespace vz.UnitTest.Extensions
{
    [TestClass]
    public class IntegerExtensionsTests
    {
        [TestMethod]
        public void Add_WhenAddingTwoIntegers_ReturnsCorrectSum()
        {
            int a = 5;
            int b = 3;
            int expected = 8;
            int result = a.Add(b);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Add_WhenAddingZero_ReturnsSameNumber()
        {
            int number = 10;
            int toAdd = 0;
            Assert.AreEqual(number, number.Add(toAdd));
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Add_WhenSumOverflows_ThrowsOverflowException()
        {
            int.MaxValue.Add(1);
        }

        [TestMethod]
        public void Divide_ByOne_ReturnsSameNumber()
        {
            int number = 10;
            Assert.AreEqual(number, number.Divide(1));
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            10.Divide(0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Divide_MinValueByNegativeOne_ThrowsOverflowException()
        {
            int.MinValue.Divide(-1);
        }

        [TestMethod]
        public void Divide_PositiveByPositive_ReturnsCorrectQuotient()
        {
            int a = 10;
            int b = 2;
            int expected = 5;
            int result = a.Divide(b);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Multiply_ByOne_ReturnsSameNumber()
        {
            int number = 10;
            int multiplier = 1;
            Assert.AreEqual(number, number.Multiply(multiplier));
        }

        [TestMethod]
        public void Multiply_ByZero_ReturnsZero()
        {
            int number = 10;
            int multiplier = 0;
            Assert.AreEqual(0, number.Multiply(multiplier));
        }

        [TestMethod]
        public void Multiply_WhenMultiplyingTwoIntegers_ReturnsCorrectProduct()
        {
            int a = 5;
            int b = 3;
            int expected = 15;
            int result = a.Multiply(b);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Multiply_WhenProductOverflows_ThrowsOverflowException()
        {
            int.MaxValue.Multiply(2);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Subtract_WhenResultUnderflows_ThrowsOverflowException()
        {
            int.MinValue.Subtract(1);
        }

        [TestMethod]
        public void Subtract_WhenSubtractingTwoIntegers_ReturnsCorrectResult()
        {
            int a = 10;
            int b = 4;
            int expected = 6;
            int result = a.Subtract(b);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Subtract_WhenSubtractingZero_ReturnsSameNumber()
        {
            int number = 10;
            int toSubtract = 0;
            Assert.AreEqual(number, number.Subtract(toSubtract));
        }
    }
}