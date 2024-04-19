using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Tools;

enum PayFrequency
{
    Weekly,
    Fortnightly,
    Monthly,
    Unknown
}

internal class ConsoleHelpers
{
    public static string FormatNumber(decimal number)
    {
        return "$" + number.ToString("N");
    }

    public static int GetPackageAmount(string[] args)
    {
        int packageAmount = GetPackageAmountFromArgs(args);
        if (packageAmount > 0)
        {
            return packageAmount;
        }

        // keep trying till you get a valid package amount
        int intAmount = -1;
        while (intAmount < 0)
        {
            Console.Write("Enter package amount: $");
            string? readLine = Console.ReadLine();

            if (readLine == null)
            {
                continue;
            }

            intAmount = StringToInt(readLine);
        }

        return intAmount;
    }
    public static PayFrequency GetFrequencyFromArgs(string[] args)
    {
        int argumentIndex = Array.IndexOf(args, "--pay-frequency");

        if (argumentIndex == -1)
        {
            return PayFrequency.Unknown;
        }

        int index = Array.IndexOf(args, "--pay-frequency") + 1;
        if (index >= args.Length)
        {
            return PayFrequency.Unknown;
        }

        return args[index].ToLower() switch
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
        while (payFrequency != 'w' && payFrequency != 'f' && payFrequency != 'm')
        {
            Console.Write("Enter pay frequency (w for weekly, f for fortnightly, m for monthly): ");
            string? readLine = Console.ReadLine();

            if (readLine == null || readLine.Length != 1)
            {
                continue;
            }

            payFrequency = readLine[0];
        }

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
        int argumentIndex = Array.IndexOf(args, "--package-amount");

        if (argumentIndex == -1)
        {
            return -1;
        }

        int index = Array.IndexOf(args, "--package-amount") + 1;
        if (index >= args.Length)
        {
            return -1;
        }

        return StringToInt(args[index]);
    }

    public static int StringToInt(string strAmount)
    {
        strAmount = strAmount.Trim();
        if (strAmount.Length > int.MaxValue.ToString().Length - 1)
        {
            return -1;
        }

        if (!int.TryParse(strAmount, out int intAmount))
        {
            return -1;
        }

        return intAmount;
    }

}
