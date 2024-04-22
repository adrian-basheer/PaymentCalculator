using Microsoft.Extensions.Configuration;
using PaymentCalculator.Models;
using PaymentCalculator.Tools;

// Load settings from appsettings.json. Tax brackets can easily be changed in this file.
IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Settings? settings = config.GetSection("Settings").Get<Settings>();
if (settings == null)
{
    throw new Exception("Settings not found");
}

Console.WriteLine("Press Ctrl+C to exit at any time.");
Console.WriteLine();

// get the arguments from the command line, if command line arguments are
// not provided, the user will be prompted
decimal grossPackage = ConsoleHelpers.GetPackageAmount(args);
PayFrequency payFrequency = ConsoleHelpers.GetPayFrequency(args);

// Return all data needed in a response structure. The reason for this is that we can add a feature to
// take a range of income (for example 100_000 to 200_000, step by 1000) and return a list of responses.
// the response can print to the console, or can return a CSV string. for the above range example, we 
// can put the csv string into a file, then open it in Excel and graph the results.
Response response = TaxHelper.GetTaxCalcuations(grossPackage, payFrequency, settings);

response.WriteToConsole();

