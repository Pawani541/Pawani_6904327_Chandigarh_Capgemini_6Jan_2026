using System;

class Program
{
    static void Main()
    {
       
        Console.WriteLine("Enter date (dd/MM/yyyy):");
        string date = Console.ReadLine();

        Console.WriteLine("Enter number of years to add:");
        int years = int.Parse(Console.ReadLine());

        // Calling method
        string result = AddYearsToDate(date, years);

        // Printing output
        Console.WriteLine("Output:");
        Console.WriteLine(result);
    }

    static string AddYearsToDate(string date, int years)
    {
        DateTime dt;

        //  Check whether date format is correct or not
        // If date is invalid, TryParse will fail
        if (!DateTime.TryParse(date, out dt))
        {
            // -1 means invalid date format
            return "-1";
        }

        //  Check whether years is negative
        if (years < 0)
        {
            // -2 means negative years value
            return "-2";
        }

        //  Add given years to the date
        DateTime newDate = dt.AddYears(years);

        //  Return date in dd/MM/yyyy format
        return newDate.ToString("dd/MM/yyyy");
    }
}