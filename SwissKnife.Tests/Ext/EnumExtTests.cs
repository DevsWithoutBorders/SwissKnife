using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using SwissKnife.Attributes;
using SwissKnife.Ext;

namespace SwissKnife.Tests.Ext
{
    [TestFixture]
    public class EnumExtTests
    {
        [Flags]
        public enum TestEnumInt
        {
            [StringValue("First")]
            FirstValue = 1,

            [StringValue("Second")]
            SecondValue = 2,

            [StringValue("Third", "One")]
            [StringValue("First | Second", "Two")]
            ThirdValue = FirstValue | SecondValue,

            [StringValue("Forth")]
            ForthValue = 4,

            FiftiethValue = 50
        }

        #region ToEnum

        [Test]
        public void ToEnum_ConvertValidValue()
        {
            var resultInt = ((int)TestEnumInt.SecondValue).ToEnum<TestEnumInt>();
            Assert.AreEqual(resultInt, TestEnumInt.SecondValue);

            var resultString = ((int)TestEnumInt.SecondValue).ToString(CultureInfo.InvariantCulture).ToEnum<TestEnumInt>();
            Assert.AreEqual(resultString, TestEnumInt.SecondValue);
        }

        [Test]
        public void ToEnum_ConvertInvalidValue()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 32.ToEnum<TestEnumInt>());
            Assert.Throws<ArgumentOutOfRangeException>(() => "32".ToEnum<TestEnumInt>());
        }

        #endregion

        #region TryParse

        [Test]
        public void TryParse_ConvertValidValue()
        {
            TestEnumInt resultInt;
            Assert.IsTrue(EnumExt.TryParse(((int)TestEnumInt.FiftiethValue), out resultInt));
            Assert.AreEqual(resultInt, TestEnumInt.FiftiethValue);

            TestEnumInt resultString;
            Assert.IsTrue(EnumExt.TryParse(((int)TestEnumInt.FiftiethValue).ToString(CultureInfo.InvariantCulture), out resultString));
            Assert.AreEqual(resultString, TestEnumInt.FiftiethValue);
        }

        [Test]
        public void TryParse_ConvertInvalidValue()
        {
            TestEnumInt resultInt;
            Assert.IsFalse(EnumExt.TryParse(32, out resultInt));

            TestEnumInt resultString;
            Assert.IsFalse(EnumExt.TryParse("32", out resultString));
        }

        #endregion

        #region HasFlag

        [Test]
        public void HasFlag_FlagExists()
        {
            const TestEnumInt baseFlag = TestEnumInt.ThirdValue;
            Assert.IsTrue(baseFlag.HasFlag(TestEnumInt.FirstValue)); // True
            Assert.IsTrue(baseFlag.HasFlag(TestEnumInt.SecondValue)); // True
            Assert.IsFalse(baseFlag.HasFlag(TestEnumInt.ForthValue)); // False
        }

        [Test]
        public void GetIndividualFlags()
        {
            const TestEnumInt baseFlag = TestEnumInt.ThirdValue;

            var flagsList = new List<Enum>(baseFlag.GetIndividualFlags());
            Assert.AreEqual(flagsList.Count, 2);
            Assert.Contains(TestEnumInt.FirstValue, flagsList);
            Assert.Contains(TestEnumInt.SecondValue, flagsList);
        }

        #endregion

        #region GetStringValue

        [Test]
        public void GetStringValue_SingleAttribute()
        {
            Assert.AreEqual("First", TestEnumInt.FirstValue.GetStringValue());
        }

        [Test]
        public void GetStringValue_MultipleAttributes()
        {
            Assert.AreEqual("Third", TestEnumInt.ThirdValue.GetStringValue("One"));
            Assert.AreEqual("First | Second", TestEnumInt.ThirdValue.GetStringValue("Two"));
        }

        [Test]
        public void GetStringValue_NoAttributes()
        {
            Assert.AreEqual("FiftiethValue", TestEnumInt.FiftiethValue.GetStringValue());
        }

        #endregion
    }
}
