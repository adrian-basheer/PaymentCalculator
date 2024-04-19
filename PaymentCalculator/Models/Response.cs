using PaymentCalculator.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Models
{
    internal class Response
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

        public string GetCsvString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ConsoleHelpers.FormatNumber(GrossPackage) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(Superannuation) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(TaxableIncome) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(MedicareLevy) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(BudgetRepairLevy) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(IncomeTax) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(NetIncome) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(PayPacket) + ",");

            string frequencyDescription = PayFrequency switch
            {
                PayFrequency.Weekly => "week",
                PayFrequency.Fortnightly => "fortnight",
                PayFrequency.Monthly => "month",
                _ => throw new Exception("Invalid pay frequency")
            };

            sb.Append(frequencyDescription);

            return sb.ToString();
        }

        public void WriteToConsole()
        {
            Console.WriteLine("Calculating salary details...");

            Console.WriteLine("");
            Console.WriteLine($"Gross Pacakge: {ConsoleHelpers.FormatNumber(GrossPackage)}");
            Console.WriteLine($"Superannuation: {ConsoleHelpers.FormatNumber(Superannuation)}");

            Console.WriteLine("");
            Console.WriteLine($"Taxable Income: {ConsoleHelpers.FormatNumber(TaxableIncome)}. Doc says to nearest dollar.");


            Console.WriteLine("");
            Console.WriteLine("Deductions:");
            Console.WriteLine($"Medicare levy: {ConsoleHelpers.FormatNumber(MedicareLevy)}");
            Console.WriteLine($"Budget repair levy: {ConsoleHelpers.FormatNumber(BudgetRepairLevy)}");
            Console.WriteLine($"Income tax: {ConsoleHelpers.FormatNumber(IncomeTax)}");

            Console.WriteLine("");
            Console.WriteLine($"Net Income: {ConsoleHelpers.FormatNumber(NetIncome)}");

            string frequencyDescription = PayFrequency switch
            {
                PayFrequency.Weekly => "week",
                PayFrequency.Fortnightly => "fortnight",
                PayFrequency.Monthly => "month",
                _ => throw new Exception("Invalid pay frequency")
            };

            Console.WriteLine($"Pay Packet: {ConsoleHelpers.FormatNumber(PayPacket)} per {frequencyDescription}");
        }
    }
}
