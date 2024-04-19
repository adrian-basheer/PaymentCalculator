using PaymentCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Strategies
{
    internal class MedicareLevyStrategy(List<TaxBracket> taxBrackets) : IStrategy
    {
        public decimal CalculateAmount(decimal taxableAmount)
        {
            return CalculateMedicareLevy(taxableAmount, taxBrackets);
        }

        private static decimal CalculateMedicareLevy(decimal taxableAmount, List<TaxBracket> taxBrackets)
        {
            if (taxableAmount >= taxBrackets[0].LowerBound)
            {
                decimal result = taxableAmount * taxBrackets[0].Rate;
                return Math.Ceiling(result);
            }

            if (taxableAmount >= taxBrackets[1].LowerBound && taxableAmount <= taxBrackets[1].UpperBound)
            {
                return Math.Ceiling((taxableAmount - taxBrackets[1].LowerBound) * taxBrackets[^1].Rate);
            }

            return 0;
        }
    }
}
