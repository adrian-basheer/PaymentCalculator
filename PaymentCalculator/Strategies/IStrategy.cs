using PaymentCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Strategies
{
    internal interface IStrategy
    {
        decimal CalculateAmount(decimal taxableAmount);
    }
}
