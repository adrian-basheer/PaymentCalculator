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

        private static decimal CalculateBudgetRepairLevy(decimal taxableAmount, List<TaxBracket> taxBrackets)
        {
            for (int i = 0; i < taxBrackets.Count; i++)
            {
                TaxBracket taxBracket = taxBrackets[i];
                if (taxableAmount >= taxBracket.LowerBound)
                {
                    return Math.Ceiling(taxableAmount * taxBracket.Rate);
                }
            }

            return 0;
        }
    }
}
