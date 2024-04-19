using Microsoft.Extensions.Configuration;
using PaymentCalculator.Models;
using PaymentCalculator.Strategies;
using PaymentCalculator.Tools;

// See https://aka.ms/new-console-template for more information

// Load settings from appsettings.json. Tax brackets can easily be changed in this file.
IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Settings? settings = config.GetSection("Settings").Get<Settings>();
if (settings == null)
{
    throw new Exception("Settings not found");
}

decimal grossPackage = ConsoleHelpers.GetPackageAmount(args);
PayFrequency payFrequency = ConsoleHelpers.GetPayFrequency(args);


// round to the nearest cent
decimal superannuation = (0.095M * grossPackage) / 1.095M;
superannuation = Math.Round(superannuation, 2, MidpointRounding.AwayFromZero);

// example is showing cents, but document says round to the nearest dollar
decimal taxableIncome = grossPackage - superannuation;
taxableIncome = Math.Round(taxableIncome, 0, MidpointRounding.AwayFromZero);


TaxCalculationContext context = new TaxCalculationContext(new IncomeTaxStrategy(settings.IncomeTax));

decimal incomeTax = context.CalculateAmount(taxableIncome);
decimal medicareLevy = new TaxCalculationContext(new MedicareLevyStrategy(settings.MedicareLevy)).CalculateAmount(taxableIncome);
decimal budgetRepairLevy = new TaxCalculationContext(new IncomeTaxStrategy(settings.BudgetRepairLevy)).CalculateAmount(taxableIncome);

decimal deductions = incomeTax + medicareLevy + budgetRepairLevy;
decimal netIncome = grossPackage - superannuation - deductions;

decimal divisor = payFrequency switch
{
    PayFrequency.Weekly => 52.0M,
    PayFrequency.Fortnightly => 26.0M,
    PayFrequency.Monthly => 12.0M,
    _ => throw new Exception("Invalid pay frequency")
};

string frequencyDescription = payFrequency switch
{
    PayFrequency.Weekly => "week",
    PayFrequency.Fortnightly => "fortnight",
    PayFrequency.Monthly => "month",
    _ => throw new Exception("Invalid pay frequency")
};

decimal payPacket = Math.Round(netIncome / divisor, 2, MidpointRounding.AwayFromZero);

Console.WriteLine("Calculating salary details...");

Console.WriteLine("");
Console.WriteLine($"Gross Pacakge: {ConsoleHelpers.FormatNumber(grossPackage)}");
Console.WriteLine($"Superannuation: {ConsoleHelpers.FormatNumber(superannuation)}");

Console.WriteLine("");
Console.WriteLine($"Taxable Income: {ConsoleHelpers.FormatNumber(taxableIncome)}. Doc says to nearest dollar.");

Console.WriteLine("");
Console.WriteLine("Deductions:");
Console.WriteLine($"Medicare levy: {ConsoleHelpers.FormatNumber(medicareLevy)}");
Console.WriteLine($"Budget repair levy: {ConsoleHelpers.FormatNumber(budgetRepairLevy)}");
Console.WriteLine($"Income tax: {ConsoleHelpers.FormatNumber(incomeTax)}");

Console.WriteLine("");
Console.WriteLine($"Net Income: {ConsoleHelpers.FormatNumber(netIncome)}");
Console.WriteLine($"Pay Packet: {ConsoleHelpers.FormatNumber(payPacket)} per {frequencyDescription}");
