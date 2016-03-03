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
        public void Shuffle_Null_ThrowArgumentNullException()
        {
            //action
            TestDelegate action = () => ListExtensions.Shuffle<int>(null);

            //assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Test]
        public void Shuffle_OrderedArray_ShuffledArray()
        {
            //arrange
            var deck = Enumerable.Range(1, 52).ToArray();

            //action
            deck.Shuffle();

            //assert
            for (var i = 0; i < deck.Length; i++)
            {
                Assert.AreNotEqual(i + 1, deck[i]);
            }
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
