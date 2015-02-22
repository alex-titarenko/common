using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TAlex.Common.Extensions;


namespace TAlex.Common.Test.Extensions
{
    [TestClass]
    public class StringExtensionsTest
    {
        #region Cut Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CutTest_NegativeLength()
        {
            //arrange
            string text = "If you spend too much time thinking about a thing, you'll never get it done.";
            int length = -1;

            //act
            text.Cut(length);
        }

        [TestMethod]
        public void CutTest_EmptyString()
        {
            //arrange
            string text = String.Empty;
            int length = 200;
            string expected = String.Empty;
            
            //act
            string actual = text.Cut(length);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CutTest()
        {
            //arrange
            string text = "The universe is commonly defined as the totality of everything that exists, including all matter and energy, the planets, stars, galaxies, and the contents of intergalactic space.";
            int length = 30;
            string expected = "The universe is commonly"; 
            
            //act
            string actual = text.Cut(length);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Cut_AddEllipsis()
        {
            //arrange
            string text = "The universe is commonly defined as the totality of everything that exists, including all matter and energy, the planets, stars, galaxies, and the contents of intergalactic space.";
            int length = 30;
            string expected = "The universe is commonly ...";

            //act
            string actual = text.Cut(length, true);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CutTest2()
        {
            //arrange
            string text = "The possession of anything begins in the mind";
            int length = 26;
            string expected = "The possession of anything";
            
            //act
            string actual = text.Cut(length);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CutTest3()
        {
            //arrange
            string text = "Knowledge will give you power, but character respect.";
            int length = 29;
            string expected = "Knowledge will give you power";
            
            //act
            string actual = text.Cut(length);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CutTest_ExcessLength()
        {
            //arrange
            string text = "640K ought to be enough for anybody.";
            int length = 200;
            string expected = "640K ought to be enough for anybody.";
            
            //act
            string actual = text.Cut(length);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CutTest_ExcessLengthAddEllipsis()
        {
            //arrange
            string text = "640K ought to be enough for anybody.";
            int length = 200;
            string expected = "640K ought to be enough for anybody.";

            //act
            string actual = text.Cut(length, true);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CutTest_TrimmingEndingComma()
        {
            //arrange
            string text = "If you love life, don't waste time, for time is what life is made up of.";
            int length = 38;
            string expected = "If you love life, don't waste time";
            
            //act
            string actual = text.Cut(length);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ExtractTextFromHtml Tests

        [TestMethod]
        public void ExtractTextFromHtmlTest()
        {
            //arrange
            string text = "<p style=\"color:red\">Some <strong>text</strong></p>";
            string expected = "Some  text";

            //act
            var actual = text.ExtractTextFromHtml();

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region SplitByLines Tests

        [TestMethod]
        public void SplitByLinesTest_LF()
        {
            //arrange
            string text = "On July 4 2012, CERN announced the formal confirmation that a particle 'consistent with the Higgs boson' exists with a very high likelihood of 99.99994% (five sigma);\nhowever, scientists still need to verify that it is indeed the expected boson and not some other new particle.";

            string[] expected = new string[]
            {
                "On July 4 2012, CERN announced the formal confirmation that a particle 'consistent with the Higgs boson' exists with a very high likelihood of 99.99994% (five sigma);",
                "however, scientists still need to verify that it is indeed the expected boson and not some other new particle."
            };

            //act
            string[] actual = text.SplitByLines();

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SplitByLinesTest_CRLF()
        {
            //arrange
            string text = "The Higgs boson is an elementary particle within the Standard Model of particle physics.\r\nIt belongs to a class of particles known as bosons.";

            string[] expected = new string[]
            {
                "The Higgs boson is an elementary particle within the Standard Model of particle physics.",
                "It belongs to a class of particles known as bosons."
            };

            //act
            string[] actual = text.SplitByLines();

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region CamelToRegular Tests

        [TestMethod]
        public void CamelToRegularTest()
        {
            //arrange
            string text = "CamelText";
            string expected = "Camel Text";

            //act
            string actual = StringExtensions.CamelToRegular(text);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CamelToRegularTest_NullString()
        {
            //arrange
            string text = null;
            string expected = null;

            //act
            string actual = StringExtensions.CamelToRegular(text);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CamelToRegularTest_EmptyString()
        {
            //arrange
            string text = String.Empty;
            string expected = String.Empty;

            //act
            string actual = StringExtensions.CamelToRegular(text);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CamelToRegularTest_TextWithAbbreviation()
        {
            //arrange
            string text = "TDDTest";
            string expected = "TDD Test";

            //act
            string actual = StringExtensions.CamelToRegular(text);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CamelToRegularTest_OnlyAbbreviation()
        {
            //arrange
            string text = "SDK";
            string expected = "SDK";

            //act
            string actual = StringExtensions.CamelToRegular(text);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
