using PaymentCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Tools;

internal class ConsoleHelpers
{
    public static string FormatNumber(decimal number)
    {
        return "$" + number.ToString("N");
    }

    public static decimal GetPackageAmount(string[] args)
    {
        int packageAmount = GetPackageAmountFromArgs(args);
        if (packageAmount > 0)
        {
            return packageAmount;
        }

        // keep trying till you get a valid package amount
        decimal decimalAmount = -1;
        while (decimalAmount < 0)
        {
            Console.Write("Enter valid package positive amount: $");
            string? strAmount = Console.ReadLine();

            if (String.IsNullOrEmpty(strAmount))
            {
                continue;
            }

            if (!decimal.TryParse(strAmount, out decimalAmount))
            {
               Console.WriteLine("Invalid amount entered.");
               decimalAmount = -1;
            }
        }

        return decimalAmount;
    }
    public static PayFrequency GetFrequencyFromArgs(string[] args)
    {
        int argumentIndex = Array.IndexOf(args, "--pay-frequency");

        if (argumentIndex == -1)
        {
            return PayFrequency.Unknown;
        }

        if (argumentIndex + 1 >= args.Length)
        {
            return PayFrequency.Unknown;
        }

        return args[argumentIndex + 1].ToLower() switch
        {
            "w" or "W" => PayFrequency.Weekly,
            "f" or "F" => PayFrequency.Fortnightly,
            "m" or "M" => PayFrequency.Monthly,
            _ => PayFrequency.Unknown
        };
    }
    public static PayFrequency GetPayFrequency(string[] args)
    {
        PayFrequency frequency = GetFrequencyFromArgs(args);
        if (frequency != PayFrequency.Unknown)
        {
            return frequency;
        }

        // keep trying till you get a valid valid payment frequency,
        // the user should enter a single letter, either 'w', 'f' or 'm'.

        char payFrequency = ' ';
        do
        {
            Console.Write("Enter pay frequency (w for weekly, f for fortnightly, m for monthly): ");
            string? line = Console.ReadLine();

            if (line == null || line.Length == 0)
            {
                continue;
            }

            payFrequency = line.ToLower()[0];
        } while (payFrequency != 'w' && payFrequency != 'f' && payFrequency != 'm');

        // becasue of the while loop above, payFrequency will always be 'w', 'f' or 'm'
        return payFrequency switch
        {
            'w' or 'W' => PayFrequency.Weekly,
            'f' or 'F' => PayFrequency.Fortnightly,
            'm' or 'M' => PayFrequency.Monthly,
            _ => PayFrequency.Weekly
        };
    }


    /// <summary>
    /// if there is any error (value does not exist, value is invalid) trying
    /// to read the package amount from the arguments, return -1. This will 
    /// cause the program to prompt the user to enter the package amount manually.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static int GetPackageAmountFromArgs(string[] args)
    {
        int index = Array.IndexOf(args, "--package-amount");

        if (index == -1)
        {
            return -1;
        }

        if (index + 1 >= args.Length)
        {
            return -1;
        }

        string strAmount = args[index + 1];

        if (!int.TryParse(strAmount, out int intAmount))
        {
            return -1;
        }

        return intAmount;
    }

}
