using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEDOL_Checker
{
    public class SedolValidator : ISedolValidator
    {
        public ISedolValidationResult ValidateSedol(string input)
        {
            var result = new SedolValidationResult
            {
                InputString = input,
                IsUserDefined = false,
                IsValidSedol = false,
                ValidationDetails = null
            };

            List<int> inputStringList = Sedol.getintSedolValue(input);

            //Scenario:**  Null, empty string or string other than 7 characters long
            if (string.IsNullOrEmpty(input) | input.Length != 7)
            {
                result.ValidationDetails = "Input string is not 7-characters long.";
                return result;
            }

            //**Scenario** Invaid characters found
            if (!Regex.IsMatch(input, "^[a-zA-Z0-9]*$"))
            {
                result.ValidationDetails = "SEDOL contains invalid characters";
                return result;
            }

            //**Scenario:**  Invalid Checksum non user defined SEDOL
            if (Sedol.checkDigit(input) != inputStringList[6])
            {
                result.ValidationDetails = "Checksum digit does not agree with the rest of the input.";
                return result;
            }

            //**Scenario * *Invalid Checksum user defined SEDOL
            if (inputStringList[0]!=9)
            {
                result.ValidationDetails = "The first character of a user defined SEDOL should be 9.";
                return result;
            }

            return result;
        }
    }
}
