using Microsoft.VisualStudio.TestTools.UnitTesting;
using vz.Extensions;

namespace vz.UnitTest.Extensions
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void FirstDayOfWeek_NotMonday_ReturnsMonday()
        {
            DateTime date = new DateTime(2024, 1, 4);  // Thursday
            DateTime firstDay = date.FirstDayOfWeek();
            Assert.AreEqual(new DateTime(2024, 1, 1), firstDay); // Should be Monday
        }

        [TestMethod]
        public void FirstDayOfWeek_NullDate_ReturnsMinValue()
        {
            DateTime? nullDate = null;
            Assert.AreEqual(DateTime.MinValue, nullDate.FirstDayOfWeek());
        }

        [TestMethod]
        public void IsWeekday_NullDate_ReturnsFalse()
        {
            DateTime? nullDate = null;
            Assert.IsFalse(nullDate.IsWeekday());
        }

        [TestMethod]
        public void IsWeekday_Weekday_ReturnsTrue()
        {
            Assert.IsTrue(new DateTime(2024, 1, 1).IsWeekday()); // Monday
        }

        [TestMethod]
        public void IsWeekday_Weekend_ReturnsFalse()
        {
            Assert.IsFalse(new DateTime(2024, 1, 6).IsWeekday()); // Saturday
        }

        [TestMethod]
        public void LastDayOfWeek_Test()
        {
            DateTime date = new DateTime(2024, 1, 1); // Monday
            DateTime lastDay = date.LastDayOfWeek();
            Assert.AreEqual(new DateTime(2024, 1, 7), lastDay); // Sunday
        }

        [TestMethod]
        public void ToWindowsFileName_EmptyFileName_ReturnsTimestamp()
        {
            DateTime dt = new DateTime(2024, 1, 1, 12, 0, 0);
            string result = dt.ToWindowsFileName();
            Assert.AreEqual("2024-01-01_12-00-00-000", result);
        }

        [TestMethod]
        public void ToWindowsFileName_LongFileName_TruncatesProperly()
        {
            DateTime dt = new DateTime(2024, 1, 1, 12, 0, 0);
            string longFileName = new string('a', 260) + ".txt";
            string result = dt.ToWindowsFileName(longFileName);
            Assert.IsTrue(result.Length <= 255);
            Assert.IsTrue(result.EndsWith("_2024-01-01_12-00-00-000.txt"));
        }

        [TestMethod]
        public void ToWindowsFileName_WhitespaceFileName_ReturnsTimestamp()
        {
            DateTime dt = new DateTime(2024, 1, 1, 12, 0, 0);
            string result = dt.ToWindowsFileName("   ");
            Assert.AreEqual("2024-01-01_12-00-00-000", result); // Adjust this to match the exact expected timestamp format
        }

        [TestMethod]
        public void ToWindowsFileName_WithFileName_AppendsTimestamp()
        {
            DateTime dt = new DateTime(2024, 1, 1, 12, 0, 0);
            string result = dt.ToWindowsFileName("file.txt");
            Assert.AreEqual("file_2024-01-01_12-00-00-000.txt", result);
        }
    }
}