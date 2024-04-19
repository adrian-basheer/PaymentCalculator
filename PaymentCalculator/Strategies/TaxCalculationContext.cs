using PaymentCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Strategies
{
    internal class TaxCalculationContext(IStrategy strategy)
    {
        private IStrategy _strategy = strategy;

        public void SetStrategy(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public decimal CalculateAmount(decimal taxableAmount)
        {
            return _strategy.CalculateAmount(taxableAmount);
        }
    }
}
