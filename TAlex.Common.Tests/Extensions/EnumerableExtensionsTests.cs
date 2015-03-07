using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using TAlex.Common.Extensions;
using NUnit.Framework;


namespace TAlex.Common.Tests.Extensions
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        #region Randomize

        [Test]
        public void Randomize_OrderedList_RandomizedList()
        {
            //arrange
            List<int> orderedList = Enumerable.Range(1, 10).ToList();
            
            //action
            List<int> randomizedList = orderedList.Randomize().ToList();

            //assert
            CollectionAssert.AreEquivalent(orderedList, randomizedList);
            CollectionAssert.AreNotEqual(orderedList, randomizedList);
        }

        #endregion
    }

    [TestFixture]
    public class EnumerableSearchExtensionsTests
    {
        private List<TestClass> _testCollection;

        private List<Func<TestClass, object>> _textPropertySelector = new List<Func<TestClass, object>>()
        {
            x => x.Text
        };

        private List<Func<TestClass, object>> _allPropertySelectors = new List<Func<TestClass, object>>()
        {
            x => x.Integer,
            x => x.Text
        };


        [SetUp]
        public void Initialize()
        {
            _testCollection = new List<TestClass>()
            {
                new TestClass() { Integer = 55, Text = "Test word define" },
                new TestClass() { Integer = 111, Text = "This is the text" },
                new TestClass() { Integer = -46, Text = "The universe is commonly defined as the totality of everything that exists,[1] including all matter and energy, the planets, stars, galaxies, and the contents of intergalactic space." },
                new TestClass() { Integer = 999, Text = "support@talex-soft.com some e-mail" },
                new TestClass() { Integer = 999, Text = "info@talex-soft.com some e-mail" }
            };
        }


        #region Search

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Search_NullQuery_ThrowNullReferenceException()
        {
            //arrange
            string query = null;

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);
            
            //assert
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Search_NullConditions_ThrowNullReferenceException()
        {
            //arrange
            string query = "word";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, null);

            //assert
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void Search_NullFieldValue_SearchResult()
        {
            //arrange
            string query = "text123456789";
            List<TestClass> coll = new List<TestClass>(_testCollection);
            coll.Add(new TestClass() { Integer = 5, Text = null });

            //action
            IEnumerable<TestClass> actual = coll.Search(query, _allPropertySelectors);

            //assert
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void Search_NullItem_SearchResult()
        {
            //arrange
            string query = "text123456789";
            List<TestClass> coll = new List<TestClass>(_testCollection);
            coll.Add(null);

            //action
            IEnumerable<TestClass> actual = coll.Search(query, _allPropertySelectors);

            //assert
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void Search_NoResults_SearchResult()
        {
            //arrange
            string query = "apple";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            //assert
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void Search_Success_SearchResult()
        {
            //arrange
            string query = "text";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            //assert
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[1], actual.First());
        }

        [Test]
        public void Search_CaseSensitive_SearchResult()
        {
            //arrange
            string query = "TEST";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            //assert
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[0], actual.First());
        }

        [Test]
        public void Search_CleaningDelimiters_SearchResult()
        {
            //arrange
            string query = "energy";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultComplianceType.Strict);

            //assert
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[2], actual.First());
        }

        [Test]
        public void Search_NumericalSearch_SearchResult()
        {
            //arrange
            string query = "111";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            //assert
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[1], actual.First());
        }

        [Test]
        public void Search_WildcardQueryQuestionMark_SearchResult()
        {
            //arrange
            string query = "t?e";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            //assert
            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(_testCollection[1], actual.First());
            Assert.AreEqual(_testCollection[2], actual.ElementAt(1));
        }

        [Test]
        public void Search_WildcardQueryAsteriskCharacter_SearchResult()
        {
            //arrange
            string query = "total*";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            //assert
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[2], actual.First());
        }

        [Test]
        public void Search_ComplianceTypeBeginning_SearchResult()
        {
            //arrange
            string query = "common includ ord";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultComplianceType.Beginning);

            //assert
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[2], actual.First());
        }

        [Test]
        public void Search_ComplianceTypeEnding_SearchResult()
        {
            //arrange
            string query = "ext includ ord";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultComplianceType.Ending);

            //assert
            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(_testCollection[0], actual.First());
            Assert.AreEqual(_testCollection[1], actual.ElementAt(1));
        }

        [Test]
        public void Search_ComplianceTypeOccurrence_SearchResult()
        {
            //arrange
            string query = "very";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultComplianceType.Occurrence);

            //assert
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[2], actual.First());
        }

        [Test]
        public void Search_ComplianceTypeStrict_SearchResult()
        {
            //arrange
            string query = "define";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultComplianceType.Strict);

            //assert
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[0], actual.First());
        }

        [Test]
        public void Search_DefaultOperatorOr_SearchResult()
        {
            //arrange
            string query = "The totality";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultOperator.Or, DefaultComplianceType.Strict);

            //assert
            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(_testCollection[1], actual.First());
            Assert.AreEqual(_testCollection[2], actual.ElementAt(1));
        }

        [Test]
        public void Search_DefaultOperatorAnd_SearchResult()
        {
            //arrange
            string query = "is 111";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors, DefaultOperator.And, DefaultComplianceType.Strict);

            //assert
            Assert.AreEqual(1, actual.Count());
        }

        [Test]
        public void Search_DefaultOperatorAndDirectOrderWord_SearchResult()
        {
            //arrange
            string query = "The totality";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultOperator.And, DefaultComplianceType.Strict);

            //assert
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[2], actual.First());
        }

        [Test]
        public void Search_DefaultOperatorAndBackOrderWord_SearchResult()
        {
            //arrange
            string query = "totality as";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultOperator.And, DefaultComplianceType.Strict);

            //assert
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[2], actual.First());
        }

        [Test]
        public void Search_DefaultOperatorAndNoResults_SearchResult()
        {
            //arrange
            string query = "The totality text";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultOperator.And, DefaultComplianceType.Strict);

            //assert
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void Search_SearchForDifferentField_SearchResult()
        {
            //arrange
            string query = "111";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _textPropertySelector);

            //assert
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void Search_EmailSearch_SearchResult()
        {
            //arrange
            string query = "support@talex-soft.com";

            //action
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            //assert
            Assert.AreEqual(1, actual.Count());
        }

        #endregion

        #region Design Datas

        private class TestClass
        {
            public int Integer { get; set; }
            public string Text { get; set; }
        }

        #endregion
    }
}
