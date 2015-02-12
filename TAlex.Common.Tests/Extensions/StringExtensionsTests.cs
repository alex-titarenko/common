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
    }
}
