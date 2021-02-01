using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDOL_Checker
{
    class Sedol
    {
        public static List<int> _weights = new List<int> { 1, 3, 1, 7, 3, 9 };

        //to get last char/digit of input value
        public static char checkDigit(string inputValue)
        {
            int[] first6digit = getintSedolValue(inputValue).Take(6).ToArray();
            int checkSum = 0;

            for (int i = 0; i <= getintSedolValue(inputValue).Count - 2; i++)
            {
                checkSum = checkSum + ((first6digit[i] - 55) * _weights[i]);
            }
            return Convert.ToChar((10 - (checkSum % 10)) % 10);
        }

        //to get integer value for all the input values /charachet+9 in a list
        public static List<int> getintSedolValue(string inputValue)
        {
            List<int> intValue = new List<int>();
            foreach (char ch in inputValue.ToArray())
            {
                intValue.Add(getIntFromChar(ch));
            }
            return intValue;
        }

        //get integer value for a character
        public static int getIntFromChar(char inputValue)
        {
            if (Char.IsLetter(inputValue))
                return Char.ToUpper(inputValue) - 55;
            return inputValue - 48;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter SEDOL Value");
            string input = Console.ReadLine();

            SedolValidator sdeol = new SedolValidator();
            sdeol.ValidateSedol(input);
            Console.WriteLine(sdeol.ValidateSedol(input).InputString.ToString());
            Console.WriteLine(sdeol.ValidateSedol(input).IsUserDefined.ToString());
            Console.WriteLine(sdeol.ValidateSedol(input).IsValidSedol.ToString());
            Console.WriteLine(sdeol.ValidateSedol(input).ValidationDetails.ToString());

            Console.ReadKey();
        }
    }
}
