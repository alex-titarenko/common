using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TAlex.Common.Extensions;


namespace TAlex.Common.Test.Extensions
{
    [TestClass]
    public class EnumerableExtensionsTest
    {
        [TestMethod]
        public void RandomizeTest()
        {
            List<int> orderedList = Enumerable.Range(1, 10).ToList();
            List<int> randomizedList = orderedList.Randomize().ToList();

            CollectionAssert.AreEquivalent(orderedList, randomizedList);
            CollectionAssert.AreNotEqual(orderedList, randomizedList);
        }
    }
}
