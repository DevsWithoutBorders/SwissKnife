using NUnit.Framework;
using SwissKnife.Ext;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissKnife.Ext.Tests
{
    [TestFixture]
    public class StringExtTests
    {
        public class FormatSafe
        {
            [Test]
            public void Correct_format()
            {
                string result = "Test {0}".FormatSafe("value1");
                Assert.AreEqual("Test value1", result);
            }

            [Test]
            public void Insufficient_arguments()
            {
                string result1 = "Test {0}".FormatSafe();
                Assert.AreEqual("Test {0} ()", result1);

                string result2 = "Test {0} {1}".FormatSafe("value1");
                Assert.AreEqual("Test {0} {1} (value1)", result2);

                string result3 = "Test {0} {1} {2}".FormatSafe("value1", "value2");
                Assert.AreEqual("Test {0} {1} {2} (value1, value2)", result3);
            }

            [Test]
            public void Incorrect_format_string()
            {
                string result1 = "Test {0".FormatSafe();
                Assert.AreEqual("Test {0 ()", result1);
            }
        }
    }
}