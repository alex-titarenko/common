using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TAlex.Common.Extensions;


namespace TAlex.Common.Test.Extensions
{
    [TestClass]
    public class ListExtensionsTest
    {
        [TestMethod]
        public void ShuffleTest()
        {
            List<int> orderedList = Enumerable.Range(1, 10).ToList();
            List<int> actual = Enumerable.Range(1, 10).ToList();

            CollectionAssert.AreEqual(orderedList, actual);

            actual.Shuffle();

            CollectionAssert.AreNotEqual(orderedList, actual);
            CollectionAssert.AreEquivalent(orderedList, actual);
        }

        [TestMethod]
        public void ShuffleTest_OneElemen()
        {
            List<int> orderedList = new List<int>() { 5 };
            List<int> actual = new List<int>() { 5 };

            actual.Shuffle();
            CollectionAssert.AreEqual(orderedList, actual);
        }

        [TestMethod]
        public void ShuffleTest_EmptyList()
        {
            List<int> actual = new List<int>();
            actual.Shuffle();
            Assert.IsTrue(actual.Count == 0);
        }
    }
}
