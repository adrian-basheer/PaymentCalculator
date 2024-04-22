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

        /// <summary>
        /// To keep data entry in the json file simple, we use the same structure. However, the 
        /// logic below 'knows' which brackets to use and does not generically go through a sorted list
        /// like in the other strategies.
        /// 
        /// Then again, doing things differently and cleanly is the advantage point of the strategy pattern.
        /// </summary>
        /// <param name="taxableAmount"></param>
        /// <param name="taxBrackets"></param>
        /// <returns></returns>
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
