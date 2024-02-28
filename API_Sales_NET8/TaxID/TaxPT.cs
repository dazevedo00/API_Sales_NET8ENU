using System;
using System.Linq;

namespace Strategy_Pattern
{
    // Algoritmo de validação do NIF de acordo com
    // http://pt.wikipedia.org/wiki/N%C3%BAmero_de_identifica%C3%A7%C3%A3o_fiscal

    internal class TaxPT : ITax
    {
        const string TaxIDTestes = "123456789"; //Apenas para testes, para não colocar dados reais no git
        public bool IsValid(string taxId)
        {
            return ValidaContribuinte(taxId) || taxId == TaxIDTestes;
        }

        /// <summary>
        /// Validates a Portuguese taxpayer identification number (NIF) for its prefix and check digit.
        /// </summary>
        /// <param name="contribuinte">The taxpayer identification number to be validated.</param>
        static bool ValidaContribuinte(string contribuinte)
        {
            // Check if the prefix is valid
            if (contribuinte.Length == 9 && !IsValidPrefix(contribuinte) || !IsValidCheckDigit(contribuinte))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the prefix of the taxpayer identification number is valid.
        /// </summary>
        /// <param name="contribuinte">The taxpayer identification number.</param>
        /// <returns>True if the prefix is valid; otherwise, false.</returns>
        static bool IsValidPrefix(string contribuinte)
        {
            // Define valid prefixes
            string[] validPrefixes = ["1", "2", "3", "45", "5", "6", "70", "71", "72", "77", "79", "8", "90", "91", "98", "99"];

            // Check if the prefix is not in the list of valid prefixes
            return !validPrefixes.Contains(contribuinte[..2]);
        }

        /// <summary>
        /// Checks if the check digit of the taxpayer identification number is valid.
        /// </summary>
        /// <param name="contribuinte">The taxpayer identification number.</param>
        /// <returns>True if the check digit is valid; otherwise, false.</returns>
        static bool IsValidCheckDigit(string contribuinte)
        {
            // Define weights for each digit
            int[] weights = { 9, 8, 7, 6, 5, 4, 3, 2 };

            // Calculate the weighted sum of the first 8 digits
            int total = 0;
            for (int i = 0; i < 8; i++)
            {
                total += int.Parse(contribuinte[i].ToString()) * weights[i];
            }

            // Calculate the modulo 11 of the sum
            int modulo11 = total % 11;

            // Calculate the check digit comparator
            int comparador = modulo11 == 1 || modulo11 == 0 ? 0 : 11 - modulo11;

            // Check if the last digit is equal to the check digit comparator
            return int.Parse(contribuinte[8].ToString()) == comparador;
        }
    }
}
