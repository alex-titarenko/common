using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TAlex.Common.Extensions;


namespace TAlex.Common.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        #region SplitByLines

        [Test]
        public void SplitByLines_LFSeparatedString_SplittedString()
        {
            //arrange
            string text = "On July 4 2012, CERN announced the formal confirmation that a particle 'consistent with the Higgs boson' exists with a very high likelihood of 99.99994% (five sigma);\nhowever, scientists still need to verify that it is indeed the expected boson and not some other new particle.";

            string[] expected = new string[]
            {
                "On July 4 2012, CERN announced the formal confirmation that a particle 'consistent with the Higgs boson' exists with a very high likelihood of 99.99994% (five sigma);",
                "however, scientists still need to verify that it is indeed the expected boson and not some other new particle."
            };

            //acttion
            string[] actual = text.SplitByLines();

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SplitByLines_CRLFSeparatedString_SplittedString()
        {
            //arrange
            string text = "The Higgs boson is an elementary particle within the Standard Model of particle physics.\r\nIt belongs to a class of particles known as bosons.";

            string[] expected = new string[]
            {
                "The Higgs boson is an elementary particle within the Standard Model of particle physics.",
                "It belongs to a class of particles known as bosons."
            };

            //action
            string[] actual = text.SplitByLines();

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region Cut

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Cut_NegativeLength_ThrowArgumentOutOfRangeException()
        {
            //arrange
            string text = "If you spend too much time thinking about a thing, you'll never get it done.";
            int length = -1;

            //action
            text.Cut(length);
        }

        [Test]
        public void Cut_EmptyString_EmptyString()
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

        [TestCase("The universe is commonly defined as the totality of everything that exists, including all matter and energy", 30, "The universe is commonly")]
        [TestCase("The possession of anything begins in the mind", 26, "The possession of anything")]
        [TestCase("Knowledge will give you power, but character respect.", 29, "Knowledge will give you power")]
        public void Cut_FullText_CuttedText(string text, int length, string expected)
        {
            //action
            string actual = text.Cut(length);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(30)]
        [TestCase(26)]
        [TestCase(24)]
        public void Cut_FullText_CuttedTextWithEllipsis(int length)
        {
            //arrange
            string text = "The universe is commonly defined as the totality of everything that exists, including all matter and energy, the planets, stars, galaxies, and the contents of intergalactic space.";
            string expected = "The universe is commonly ...";

            //action
            string actual = text.Cut(length, true);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Cut_FullTextWithPunctuation_CuttedTextWithEllipsis()
        {
            //arrange
            string text = "The Universe is the totality of existence. This includes planets, stars, galaxies, the contents of intergalactic space, the smallest subatomic particles, and all matter and energy, the majority of which are most likely in the form of dark matter and dark energy.";
            string expected = "The Universe is the totality of existence ...";

            //action
            string actual = text.Cut(41, true);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Cut_FullTextWithExcessLength_FullText()
        {
            //arrange
            string text = "640K ought to be enough for anybody.";
            int length = 200;
            string expected = "640K ought to be enough for anybody.";

            //action
            string actual = text.Cut(length);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Cut_FullTextWithExcessLengthAddEllipsisTrue_FullTextWithoutEllipsis()
        {
            //arrange
            string text = "640K ought to be enough for anybody.";
            int length = 200;
            string expected = "640K ought to be enough for anybody.";

            //action
            string actual = text.Cut(length, true);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Cut_FullTextWithComma_CuttedAndTrimmededTExt()
        {
            //arrange
            string text = "If you love life, don't waste time, for time is what life is made up of.";
            int length = 38;
            string expected = "If you love life, don't waste time";

            //action
            string actual = text.Cut(length);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ExtractTextFromHtml

        [Test]
        public void ExtractTextFromHtml_HtmlText_RegularText()
        {
            //arrange
            string text = "<p style=\"color:red\">Some <strong>text</strong></p>";
            string expected = "Some  text";

            //action
            var actual = text.ExtractTextFromHtml();

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region CamelToRegular

        [Test]
        public void CamelToRegular_CamelText_RegularText()
        {
            //arrange
            string text = "CamelText";
            string expected = "Camel Text";

            //action
            string actual = StringExtensions.CamelToRegular(text);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CamelToRegular_NullString_NullString()
        {
            //arrange
            string text = null;
            string expected = null;

            //action
            string actual = StringExtensions.CamelToRegular(text);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CamelToRegular_EmptyString_EmptyString()
        {
            //arrange
            string text = String.Empty;
            string expected = String.Empty;

            //action
            string actual = StringExtensions.CamelToRegular(text);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CamelToRegular_TextWithAbbreviation_RegularTextWithAbbreviation()
        {
            //arrange
            string text = "TDDTest";
            string expected = "TDD Test";

            //action
            string actual = StringExtensions.CamelToRegular(text);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CamelToRegular_OnlyAbbreviation_Abbreviation()
        {
            //arrange
            string text = "SDK";
            string expected = "SDK";

            //action
            string actual = StringExtensions.CamelToRegular(text);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region EncodeHtml

        [TestCase("email@domain.com", "&#101;&#109;&#097;&#105;&#108;&#064;&#100;&#111;&#109;&#097;&#105;&#110;&#046;&#099;&#111;&#109;")]
        public void EncodeHtml_SourceString_EncodedString(string source, string expected)
        {
            //action
            var actual = source.EncodeHtml();

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
