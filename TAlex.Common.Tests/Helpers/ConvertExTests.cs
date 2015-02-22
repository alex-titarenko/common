using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TAlex.Common.Helpers;

namespace TAlex.Common.Tests.Helpers
{
    [TestFixture]
    public class ConvertExTests
    {
        [TestCase(20, "20 bytes")]
        [TestCase(2049, "2 KB")]
        [TestCase(2500, "2.44 KB")]
        [TestCase(2703360, "2.58 MB")]
        [TestCase(3488145408, "3.25 GB")]
        [TestCase(298567358702347, "271.55 TB")]
        public void BytesToDisplayStringTest(long bytes, string expected)
        {
            //action
            string actual = ConvertEx.BytesToDisplayString(bytes);

            //assert
            Assert.AreEqual(actual, expected);
        }
    }
}
