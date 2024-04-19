using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Models
{
    public class TaxBracket
    {
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public decimal Rate { get; set; }

        public TaxBracket(decimal lowerBound, decimal upperBound, decimal rate)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Rate = rate;
        }
    }
}
