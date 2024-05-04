using PaymentCalculator.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Models
{
    public enum PayFrequency
    {
        Weekly,
        Fortnightly,
        Monthly,
        Unknown
    }

    public class Response
    {
        public decimal GrossPackage { get; set; }
        public decimal Superannuation { get; set; }
        public decimal TaxableIncome { get; set; }
        public decimal MedicareLevy { get; set; }
        public decimal BudgetRepairLevy { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal NetIncome
        {
            get => GrossPackage - Superannuation - (IncomeTax + MedicareLevy + BudgetRepairLevy);
        }
        public PayFrequency PayFrequency { get; set; } = PayFrequency.Monthly;
        public decimal PayPacket {
            get
            {
                decimal divisor = PayFrequency switch
                {
                    PayFrequency.Weekly => 52.0M,
                    PayFrequency.Fortnightly => 26.0M,
                    PayFrequency.Monthly => 12.0M,
                    _ => throw new Exception("Invalid pay frequency")
                };

                return Math.Round(NetIncome / divisor, 2, MidpointRounding.AwayFromZero);
            }
        }
    }
}
