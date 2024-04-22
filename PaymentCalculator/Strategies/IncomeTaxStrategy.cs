using PaymentCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Strategies
{
    internal class IncomeTaxStrategy(List<TaxBracket> taxBrackets) : IStrategy
    {
        public decimal CalculateAmount(decimal taxableAmount)
        {
            return CalculateIncomeTax(taxableAmount, taxBrackets);
        }

        /// <summary>
        /// Since 
        /// </summary>
        /// <param name="taxableAmount"></param>
        /// <param name="taxBrackets"></param>
        /// <returns></returns>
        private static decimal CalculateIncomeTax(decimal taxableAmount, List<TaxBracket> taxBrackets)
        {
            decimal totalTax = 0;
            for (int i = 0; i < taxBrackets.Count; i++)
            {
                TaxBracket taxBracket = taxBrackets[i];
                if (taxableAmount >= taxBracket.LowerBound && taxableAmount < taxBracket.UpperBound)
                {
                    totalTax += Math.Ceiling((taxableAmount - taxBracket.LowerBound) * taxBracket.Rate);
                } else if (taxableAmount >= taxBracket.UpperBound)
                {
                    totalTax += Math.Ceiling((taxBracket.UpperBound - taxBracket.LowerBound) * taxBracket.Rate);
                }
            }

            return totalTax;
        }
    }
}
