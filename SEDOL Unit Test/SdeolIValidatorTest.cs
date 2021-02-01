using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SEDOL_Checker;
using Assert = NUnit.Framework.Assert;

namespace SEDOL_Unit_Test
{
    [TestFixture]
    public class SdeolIValidatorTest
    {
        [TestCase(null, "Null, empty string or string other than 7 characters long")]
        public void sedolIsNotSevenChar(string inputValue)
        { 
            var actual = new SedolValidator().ValidateSedol(inputValue);
            var expected = new SedolValidationResult(inputValue, false, false, "Input string is not 7 - characters long.");
            AssertValidationResult(expected, actual);
        }

        [TestCase("Invalid string")]
        public void SedolsContainingNonAlphanumericCharacters(string inputValue)
        {
            var actual = new SedolValidator().ValidateSedol(inputValue);
            var expected = new SedolValidationResult(inputValue, false, false, "SEDOL contains invalid characters");
            AssertValidationResult(expected, actual);
        }
        [TestCase("Check Sum is invalid")]
        public void UserDefinedSedolsWithIncorrectChecksum(string inputValue)
        {
            var actual = new SedolValidator().ValidateSedol(inputValue);
            var expected = new SedolValidationResult(inputValue, false, true, "Checksum digit does not agree with the rest of the input.");
            AssertValidationResult(expected, actual);
        }

        private static void AssertValidationResult(ISedolValidationResult actual, ISedolValidationResult expected)
        {
            Assert.AreEqual(expected.InputString, actual.InputString, "Input String Is Invalid");
            Assert.AreEqual(expected.IsValidSedol, actual.IsValidSedol, "Is Valid Failed");
            Assert.AreEqual(expected.IsUserDefined, actual.IsUserDefined, "Is User Defined Failed");
            Assert.AreEqual(expected.ValidationDetails, actual.ValidationDetails, "Validation Details is not as expected");
        }
    }
}
