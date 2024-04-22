using PaymentCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Strategies
{
    internal class BudgetRepairLevyStrategy(List<TaxBracket> taxBrackets) : IStrategy
    {
        public decimal CalculateAmount(decimal taxableAmount)
        {
            return CalculateBudgetRepairLevy(taxableAmount, taxBrackets);
        }


        /// <summary>
        /// The logic below is the same as CalculateIncomeTax. Even though the specification has only two brackets, 
        /// the logic is the same and will cater for more brackets if they are added in the future.
        /// </summary>
        /// <param name="taxableAmount"></param>
        /// <param name="taxBrackets"></param>
        /// <returns></returns>
        private static decimal CalculateBudgetRepairLevy(decimal taxableAmount, List<TaxBracket> taxBrackets)
        {
            decimal totalTax = 0;
            for (int i = 0; i < taxBrackets.Count; i++)
            {
                TaxBracket taxBracket = taxBrackets[i];
                if (taxableAmount >= taxBracket.LowerBound)
                {
                    totalTax += Math.Ceiling((taxableAmount - taxBracket.LowerBound) * taxBracket.Rate);
                }
                else if (taxableAmount >= taxBracket.UpperBound)
                {
                    totalTax += Math.Ceiling((taxBracket.UpperBound - taxBracket.LowerBound) * taxBracket.Rate);
                }
            }

            return totalTax;
        }
    }
}
