using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using TAlex.Common.Extensions;
using NUnit.Framework;


namespace TAlex.Common.Tests.Extensions
{
    [TestFixture]
    public class ListExtensionsTest
    {
        #region Shuffle

        [Test]
        public void Shuffle_OrderedList_ShuffledList()
        {
            //arrange
            List<int> orderedList = Enumerable.Range(1, 10).ToList();
            List<int> actual = Enumerable.Range(1, 10).ToList();
            CollectionAssert.AreEqual(orderedList, actual);

            //action
            actual.Shuffle();

            //assert
            CollectionAssert.AreNotEqual(orderedList, actual);
            CollectionAssert.AreEquivalent(orderedList, actual);
        }

        [Test]
        public void Shuffle_OneElemen_OneElement()
        {
            //arrange
            List<int> orderedList = new List<int>() { 5 };
            List<int> actual = new List<int>() { 5 };

            //action
            actual.Shuffle();

            //assert
            CollectionAssert.AreEqual(orderedList, actual);
        }

        [Test]
        public void Shuffle_EmptyList_EmptyList()
        {
            //arrange
            List<int> actual = new List<int>();

            //action
            actual.Shuffle();

            //assert
            Assert.IsTrue(actual.Count == 0);
        }

        #endregion
    }
}
