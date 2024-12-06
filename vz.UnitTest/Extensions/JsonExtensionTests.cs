using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using vz.Extensions;

namespace vz.UnitTest.Extensions
{
    [TestClass]
    public class JsonExtensionTests
    {
        [TestMethod]
        public void TestJsonEmptyList()
        {
            List<int> emptyList = new List<int>();
            Assert.AreEqual("[]", emptyList.ToJson());
        }

        [TestMethod]
        public void TestJsonWithNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3 };
            Assert.AreEqual("[1,2,3]", numbers.ToJson());
        }

        [TestMethod]
        public void TestJsonWithStrings()
        {
            List<string> strings = new List<string> { "Hello", "World" };
            Assert.AreEqual("[\"Hello\",\"World\"]", strings.ToJson());
        }

        [TestMethod]
        public void TestJsonWithSpecialCharacters()
        {
            List<string> specialChars = new List<string> { "He said, \"What's that?\"", "Back\\slash" };
            Assert.AreEqual("[\"He said, \\\"What's that?\\\"\",\"Back\\\\slash\"]", specialChars.ToJson());
        }

        [TestMethod]
        public void TestJsonWithBooleans()
        {
            List<bool> booleans = new List<bool> { true, false };
            Assert.AreEqual("[true,false]", booleans.ToJson());
        }

        [TestMethod]
        public void TestJsonWithCustomObject()
        {
            List<Person> people = new List<Person> { new Person { Name = "Alice", Age = 29 } };
            Assert.AreEqual("[{\"Name\":\"Alice\",\"Age\":29}]", people.ToJson());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestJsonWithNullCollection()
        {
            List<int> nullList = null;
            nullList.ToJson(); // Should throw an ArgumentNullException
        }

        [TestMethod]
        public void TestJsonWithNullObject()
        {
            List<Person> peopleWithNull = new List<Person> { null };
            Assert.AreEqual("[null]", peopleWithNull.ToJson());
        }

        // Helper class for testing complex objects
        private class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}