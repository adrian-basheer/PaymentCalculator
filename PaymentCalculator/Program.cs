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

Response response = TaxHelper.GetTaxCalcuations(grossPackage, payFrequency, settings);

response.WriteToConsole();

