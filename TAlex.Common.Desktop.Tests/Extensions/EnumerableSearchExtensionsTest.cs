using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TAlex.Common.Extensions;


namespace TAlex.Common.Test.Extensions
{
    /// <summary>
    /// Summary description for EnumerableExtensions
    /// </summary>
    [TestClass]
    public class EnumerableSearchExtensionsTest
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


        [TestInitialize]
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


        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void SearchTest_NullQuery()
        {
            string query = null;
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);
            Assert.AreEqual(0, actual.Count());
        }

        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void SearchTest_NullConditions()
        {
            string query = "word";
            IEnumerable<TestClass> actual = _testCollection.Search(query, null);
            Assert.AreEqual(0, actual.Count());
        }

        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_NullFieldValue()
        {
            string query = "text123456789";
            List<TestClass> coll = new List<TestClass>(_testCollection);
            coll.Add(new TestClass() { Integer = 5, Text = null });
            IEnumerable<TestClass> actual = coll.Search(query, _allPropertySelectors);

            Assert.AreEqual(0, actual.Count());
        }

        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_NullItem()
        {
            string query = "text123456789";
            List<TestClass> coll = new List<TestClass>(_testCollection);
            coll.Add(null);
            IEnumerable<TestClass> actual = coll.Search(query, _allPropertySelectors);

            Assert.AreEqual(0, actual.Count());
        }


        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_NoResults()
        {
            string query = "apple";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            Assert.AreEqual(0, actual.Count());
        }

        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_Success()
        {
            string query = "text";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[1], actual.First());
        }

        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_CaseSensitive()
        {
            string query = "TEST";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[0], actual.First());
        }

        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_CleaningDelimiters()
        {
            string query = "energy";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultComplianceType.Strict);

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[2], actual.First());
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_NumericalSearch()
        {
            string query = "111";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[1], actual.First());
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_WildcardQueryQuestionMark()
        {
            string query = "t?e";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(_testCollection[1], actual.First());
            Assert.AreEqual(_testCollection[2], actual.ElementAt(1));
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_WildcardQueryAsteriskCharacter()
        {
            string query = "total*";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[2], actual.First());
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_ComplianceTypeBeginning()
        {
            string query = "common includ ord";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultComplianceType.Beginning);

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[2], actual.First());
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_ComplianceTypeEnding()
        {
            string query = "ext includ ord";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultComplianceType.Ending);

            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(_testCollection[0], actual.First());
            Assert.AreEqual(_testCollection[1], actual.ElementAt(1));
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_ComplianceTypeOccurrence()
        {
            string query = "very";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultComplianceType.Occurrence);

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[2], actual.First());
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_ComplianceTypeStrict()
        {
            string query = "define";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultComplianceType.Strict);

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[0], actual.First());
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_DefaultOperatorOr()
        {
            string query = "The totality";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultOperator.Or, DefaultComplianceType.Strict);

            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(_testCollection[1], actual.First());
            Assert.AreEqual(_testCollection[2], actual.ElementAt(1));
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_DefaultOperatorAnd()
        {
            string query = "is 111";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors, DefaultOperator.And, DefaultComplianceType.Strict);

            Assert.AreEqual(1, actual.Count());
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_DefaultOperatorAndDirectOrderWord()
        {
            string query = "The totality";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultOperator.And, DefaultComplianceType.Strict);

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[2], actual.First());
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_DefaultOperatorAndBackOrderWord()
        {
            string query = "totality as";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultOperator.And, DefaultComplianceType.Strict);

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(_testCollection[2], actual.First());
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_DefaultOperatorAndNoResults()
        {
            string query = "The totality text";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors,
                DefaultOperator.And, DefaultComplianceType.Strict);

            Assert.AreEqual(0, actual.Count());
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_SearchForDifferentField()
        {
            string query = "111";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _textPropertySelector);

            Assert.AreEqual(0, actual.Count());
        }

        /// <summary>
        /// A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest_EmailSearch()
        {
            string query = "support@talex-soft.com";
            IEnumerable<TestClass> actual = _testCollection.Search(query, _allPropertySelectors);

            Assert.AreEqual(1, actual.Count());
        }


        private class TestClass
        {
            public int Integer { get; set; }
            public string Text { get; set; }
        }
    }
}
