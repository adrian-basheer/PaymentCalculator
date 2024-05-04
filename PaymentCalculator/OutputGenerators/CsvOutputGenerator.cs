using PaymentCalculator.Models;
using PaymentCalculator.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.OutputGenerators
{
    public class CsvOutputGenerator : IOutputGenerator
    {
        public string GenerateOutput(Response response)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetCsvString(response));
            return sb.ToString();
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

        private string GetCsvColumnHeaders()
        {
            return "Gross Package,Superannuation,Taxable Income,Medicare Levy,Budget Repair Levy,Income Tax,Net Income,Pay Packet,Pay Frequency";
        }

        public string GetCsvString(Response response)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ConsoleHelpers.FormatNumber(response.GrossPackage) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(response.Superannuation) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(response.TaxableIncome) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(response.MedicareLevy) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(response.BudgetRepairLevy) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(response.IncomeTax) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(response.NetIncome) + ",");
            sb.Append(ConsoleHelpers.FormatNumber(response.PayPacket) + ",");

            string frequencyDescription = response.PayFrequency switch
            {
                PayFrequency.Weekly => "week",
                PayFrequency.Fortnightly => "fortnight",
                PayFrequency.Monthly => "month",
                _ => throw new Exception("Invalid pay frequency")
            };

            sb.AppendLine(frequencyDescription);

            return sb.ToString();
        }

    }
}
