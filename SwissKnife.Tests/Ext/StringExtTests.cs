using NUnit.Framework;
using SwissKnife.Ext;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissKnife.Tests.Ext
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
                Assert.AreEqual("Test value1", result, "Test 1");
            }

            [Test]
            public void Insufficient_arguments()
            {
                string result1 = "Test {0}".FormatSafe();
                Assert.AreEqual("Test {0} ()", result1, "Test 1");

                string result2 = "Test {0} {1}".FormatSafe("value1");
                Assert.AreEqual("Test {0} {1} (value1)", result2, "Test 2");

                string result3 = "Test {0} {1} {2}".FormatSafe("value1", "value2");
                Assert.AreEqual("Test {0} {1} {2} (value1, value2)", result3, "Test 3");
            }

            [Test]
            public void Incorrect_format_string()
            {
                string result1 = "Test {0".FormatSafe();
                Assert.AreEqual("Test {0 ()", result1);
            }
        }

        public class ToTitleCase
        {
            [Test]
            public void Uppercased_as_acronyms()
            {
                string sourceString = "Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                string expectedResult = "Title Case This String Including A BIT OF ACRONYMS And Normal Words.";

                string result = sourceString.ToTitleCase(uppercaseAsAcronyms: true);
                Assert.AreEqual(expectedResult, result, "Test 1");

                result = sourceString.ToTitleCase();
                Assert.AreEqual(expectedResult, result, "Test 2");

                result = sourceString.ToTitleCase(true, true);
                Assert.AreEqual(expectedResult, result, "Test 3");

                result = sourceString.ToTitleCase(false, true);
                Assert.AreEqual(expectedResult, result, "Test 4");
            }

            [Test]
            public void Uppercased_not_as_acronyms()
            {
                string sourceString = "Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                string expectedResult = "Title Case This String Including A Bit Of Acronyms And Normal Words.";

                string result = sourceString.ToTitleCase(uppercaseAsAcronyms: false);
                Assert.AreEqual(expectedResult, result, "Test 1");         

                result = sourceString.ToTitleCase(true, false);
                Assert.AreEqual(expectedResult, result, "Test 2");

                result = sourceString.ToTitleCase(false, false);
                Assert.AreEqual(expectedResult, result, "Test 3");
            }

            [Test]
            public void Trim_leading_spaces()
            {
                string sourceString = " Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                string expectedResultAcronymCasing = "Title Case This String Including A BIT OF ACRONYMS And Normal Words.";
                string expectedResultNormalCasing = "Title Case This String Including A Bit Of Acronyms And Normal Words.";

                string result = sourceString.ToTitleCase(trimLeadingSpaces: true);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 1");

                result = sourceString.ToTitleCase();
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 2");

                result = sourceString.ToTitleCase(true, true);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 3");

                result = sourceString.ToTitleCase(true, false);
                Assert.AreEqual(expectedResultNormalCasing, result, "Test 4");
            }

            [Test]
            public void Leave_leading_spaces()
            {
                string sourceString = " Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                string expectedResultAcronymCasing = " Title Case This String Including A BIT OF ACRONYMS And Normal Words.";
                string expectedResultNormalCasing = " Title Case This String Including A Bit Of Acronyms And Normal Words.";

                string result = sourceString.ToTitleCase(trimLeadingSpaces: false);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 1");

                result = sourceString.ToTitleCase(false, true);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 2");

                result = sourceString.ToTitleCase(false, false);
                Assert.AreEqual(expectedResultNormalCasing, result, "Test 3");
            }
        }

        public class ToPascalCase
        {
            [Test]
            public void Uppercased_as_acronyms()
            {
                string sourceString = "Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                string expectedResult = "TitleCaseThisStringIncludingABITOFACRONYMSAndNormalWords.";

                string result = sourceString.ToPascalCase(uppercaseAsAcronyms: true);
                Assert.AreEqual(expectedResult, result, "Test 1");

                result = sourceString.ToPascalCase();
                Assert.AreEqual(expectedResult, result, "Test 2");

                result = sourceString.ToPascalCase(true, true);
                Assert.AreEqual(expectedResult, result, "Test 3");

                result = sourceString.ToPascalCase(false, true);
                Assert.AreEqual(expectedResult, result, "Test 4");
            }

            [Test]
            public void Uppercased_not_as_acronyms()
            {
                string sourceString = "Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                string expectedResult = "TitleCaseThisStringIncludingABitOfAcronymsAndNormalWords.";

                string result = sourceString.ToPascalCase(uppercaseAsAcronyms: false);
                Assert.AreEqual(expectedResult, result, "Test 1");

                result = sourceString.ToPascalCase(true, false);
                Assert.AreEqual(expectedResult, result, "Test 2");

                result = sourceString.ToPascalCase(false, false);
                Assert.AreEqual(expectedResult, result, "Test 3");
            }

            [Test]
            public void Trim_leading_spaces()
            {
                string sourceString = " Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                string expectedResultAcronymCasing = "TitleCaseThisStringIncludingABITOFACRONYMSAndNormalWords.";
                string expectedResultNormalCasing = "TitleCaseThisStringIncludingABitOfAcronymsAndNormalWords.";

                string result = sourceString.ToPascalCase(trimLeadingSpaces: true);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 1");

                result = sourceString.ToPascalCase();
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 2");

                result = sourceString.ToPascalCase(true, true);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 3");

                result = sourceString.ToPascalCase(true, false);
                Assert.AreEqual(expectedResultNormalCasing, result, "Test 4");
            }

            [Test]
            public void Leave_leading_spaces()
            {
                string sourceString = " Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                string expectedResultAcronymCasing = " TitleCaseThisStringIncludingABITOFACRONYMSAndNormalWords.";
                string expectedResultNormalCasing = " TitleCaseThisStringIncludingABitOfAcronymsAndNormalWords.";

                string result = sourceString.ToPascalCase(trimLeadingSpaces: false);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 1");

                result = sourceString.ToPascalCase(false, true);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 2");

                result = sourceString.ToPascalCase(false, false);
                Assert.AreEqual(expectedResultNormalCasing, result, "Test 3");

                sourceString = "  Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                expectedResultAcronymCasing = "  TitleCaseThisStringIncludingABITOFACRONYMSAndNormalWords.";
                expectedResultNormalCasing = "  TitleCaseThisStringIncludingABitOfAcronymsAndNormalWords.";

                result = sourceString.ToPascalCase(trimLeadingSpaces: false);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 4");

                result = sourceString.ToPascalCase(false, true);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 5");

                result = sourceString.ToPascalCase(false, false);
                Assert.AreEqual(expectedResultNormalCasing, result, "Test 6");
            }
        }

        public class ToCamelCase
        {
            [Test]
            public void Uppercased_as_acronyms()
            {
                string sourceString = "Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                string expectedResult = "titleCaseThisStringIncludingABITOFACRONYMSAndNormalWords.";

                string result = sourceString.ToCamelCase(uppercaseAsAcronyms: true);
                Assert.AreEqual(expectedResult, result, "Test 1");

                result = sourceString.ToCamelCase();
                Assert.AreEqual(expectedResult, result, "Test 2");

                result = sourceString.ToCamelCase(true, true);
                Assert.AreEqual(expectedResult, result, "Test 3");

                result = sourceString.ToCamelCase(false, true);
                Assert.AreEqual(expectedResult, result, "Test 4");
            }

            [Test]
            public void Uppercased_not_as_acronyms()
            {
                string sourceString = "Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                string expectedResult = "titleCaseThisStringIncludingABitOfAcronymsAndNormalWords.";

                string result = sourceString.ToCamelCase(uppercaseAsAcronyms: false);
                Assert.AreEqual(expectedResult, result, "Test 1");

                result = sourceString.ToCamelCase(true, false);
                Assert.AreEqual(expectedResult, result, "Test 2");

                result = sourceString.ToCamelCase(false, false);
                Assert.AreEqual(expectedResult, result, "Test 3");
            }

            [Test]
            public void Trim_leading_spaces()
            {
                string sourceString = " Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                string expectedResultAcronymCasing = "titleCaseThisStringIncludingABITOFACRONYMSAndNormalWords.";
                string expectedResultNormalCasing = "titleCaseThisStringIncludingABitOfAcronymsAndNormalWords.";

                string result = sourceString.ToCamelCase(trimLeadingSpaces: true);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 1");

                result = sourceString.ToCamelCase();
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 2");

                result = sourceString.ToCamelCase(true, true);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 3");

                result = sourceString.ToCamelCase(true, false);
                Assert.AreEqual(expectedResultNormalCasing, result, "Test 4");
            }

            [Test]
            public void Leave_leading_spaces()
            {
                string sourceString = " Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                string expectedResultAcronymCasing = " titleCaseThisStringIncludingABITOFACRONYMSAndNormalWords.";
                string expectedResultNormalCasing = " titleCaseThisStringIncludingABitOfAcronymsAndNormalWords.";

                string result = sourceString.ToCamelCase(trimLeadingSpaces: false);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 1");

                result = sourceString.ToCamelCase(false, true);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 2");

                result = sourceString.ToCamelCase(false, false);
                Assert.AreEqual(expectedResultNormalCasing, result, "Test 3");

                sourceString = "  Title case THiS string IncLudinG A BIT OF ACRONYMS and Normal words.";
                expectedResultAcronymCasing = "  titleCaseThisStringIncludingABITOFACRONYMSAndNormalWords.";
                expectedResultNormalCasing = "  titleCaseThisStringIncludingABitOfAcronymsAndNormalWords.";

                result = sourceString.ToCamelCase(trimLeadingSpaces: false);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 4");

                result = sourceString.ToCamelCase(false, true);
                Assert.AreEqual(expectedResultAcronymCasing, result, "Test 5");

                result = sourceString.ToCamelCase(false, false);
                Assert.AreEqual(expectedResultNormalCasing, result, "Test 6");
            }
        }

        public class IsNullOrEmpty
        {
            [Test]
            public void Null_value()
            {
                string value = null;
                Assert.IsTrue(StringExt.IsNullOrEmpty(value));
            }

            [Test]
            public void Empty_value()
            {
                string value = "";
                Assert.IsTrue(StringExt.IsNullOrEmpty(value), "Test 1");

                value = string.Empty;
                Assert.IsTrue(StringExt.IsNullOrEmpty(value), "Test 2");
            }

            [Test]
            public void Whitespace_value()
            {
                string value = " ";
                Assert.IsFalse(StringExt.IsNullOrEmpty(value), "Test 1");
            }
        }

        public class IsNullOrWhiteSpace
        {
            [Test]
            public void Null_value()
            {
                string value = null;
                Assert.IsTrue(StringExt.IsNullOrWhiteSpace(value));
            }

            [Test]
            public void Empty_value()
            {
                string value = "";
                Assert.IsTrue(StringExt.IsNullOrWhiteSpace(value), "Test 1");

                value = string.Empty;
                Assert.IsTrue(StringExt.IsNullOrWhiteSpace(value), "Test 2");
            }

            [Test]
            public void Whitespace_value()
            {
                string value = " ";
                Assert.IsTrue(StringExt.IsNullOrWhiteSpace(value), "Test 1");
            }
        }
    }
}