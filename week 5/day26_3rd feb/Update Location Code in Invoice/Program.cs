using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Input values
        string currentInvoice = "CAP-HYD-1234";
        string newLocation = "BAN";

        // Regex pattern:
        // ^(CAP-)   -> captures the constant prefix
        // ([A-Z]{3}) -> captures the location code (3 uppercase letters)
        // (-\d{4})$ -> captures the numeric part with dash
        string pattern = @"^(CAP-)([A-Z]{3})(-\d{4})$";

        // Check if invoice matches the pattern
        if (Regex.IsMatch(currentInvoice, pattern))
        {
            // Replace only the location part
            string updatedInvoice = Regex.Replace(
                currentInvoice,
                pattern,
                "$1" + newLocation + "$3"
            );

            Console.WriteLine("Updated Invoice Number: " + updatedInvoice);
        }
        else
        {
            Console.WriteLine("Invalid invoice format.");
        }
    }
}
