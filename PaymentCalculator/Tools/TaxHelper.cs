using PaymentCalculator.Models;
using PaymentCalculator.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Tools
{
    internal class TaxHelper
    {
        public static Response GetTaxCalcuations(decimal grossPackage, PayFrequency payFrequency, Settings settings)
        {
            Response response = new Response();
            response.GrossPackage = grossPackage;
            response.PayFrequency = payFrequency;

            // round to the nearest cent
            response.Superannuation = Math.Round((0.095M * grossPackage) / 1.095M, 2, MidpointRounding.AwayFromZero);

            // example is showing cents, but document says round to the nearest dollar
            response.TaxableIncome = Math.Round(grossPackage - response.Superannuation, 0, MidpointRounding.AwayFromZero);

            // use the strategy pattern to calculate the tax
            TaxCalculationContext context = new TaxCalculationContext(new IncomeTaxStrategy(settings.IncomeTax));
            response.IncomeTax = context.CalculateAmount(response.TaxableIncome);
            response.MedicareLevy = new TaxCalculationContext(new MedicareLevyStrategy(settings.MedicareLevy)).CalculateAmount(response.TaxableIncome);
            response.BudgetRepairLevy = new TaxCalculationContext(new IncomeTaxStrategy(settings.BudgetRepairLevy)).CalculateAmount(response.TaxableIncome);

            return response;
        }
    }
}
