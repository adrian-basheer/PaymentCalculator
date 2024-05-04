using PaymentCalculator.Models;
using PaymentCalculator.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.OutputGenerators
{
    internal class ConsoleOutputGenerator : IOutputGenerator
    { 
        public string GenerateOutput(Response response)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Calculating salary details...");

            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"Gross Pacakge: {ConsoleHelpers.FormatNumber(response.GrossPackage)}");
            stringBuilder.AppendLine($"Superannuation: {ConsoleHelpers.FormatNumber(response.Superannuation)}");

            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"Taxable Income: {ConsoleHelpers.FormatNumber(response.TaxableIncome)}. Doc says to nearest dollar.");

            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("Deductions:");
            stringBuilder.AppendLine($"Medicare levy: {ConsoleHelpers.FormatNumber(response.MedicareLevy)}");
            stringBuilder.AppendLine($"Budget repair levy: {ConsoleHelpers.FormatNumber(response.BudgetRepairLevy)}");
            stringBuilder.AppendLine($"Income tax: {ConsoleHelpers.FormatNumber(response.IncomeTax)}");

            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"Net Income: {ConsoleHelpers.FormatNumber(response.NetIncome)}");

            string frequencyDescription = response.PayFrequency switch
            {
                PayFrequency.Weekly => "week",
                PayFrequency.Fortnightly => "fortnight",
                PayFrequency.Monthly => "month",
                _ => throw new Exception("Invalid pay frequency")
            };

            stringBuilder.AppendLine($"Pay Packet: {ConsoleHelpers.FormatNumber(response.PayPacket)} per {frequencyDescription}");

            return stringBuilder.ToString();
        }

        public string[] GenerateOutput(Response[] responses)
        {
            string[] output = new string[responses.Length];
            for (int i = 0; i < responses.Length;)
            {
                output[i++] = GenerateOutput(responses[i]);
                output[i++] = Environment.NewLine;
            }

            return output;
        }
    }
}
