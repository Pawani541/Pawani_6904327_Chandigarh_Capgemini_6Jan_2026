using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter time (hh:mm am/pm):");
        string time = Console.ReadLine();

        DateTime dt;

        // Checking time format
        if (DateTime.TryParseExact(time, "hh:mm tt", null,
            System.Globalization.DateTimeStyles.None, out dt))
            Console.WriteLine("Valid Time");
        else
            Console.WriteLine("Invalid Time");
    }
}

/*
Input: 09:45 am
Output: Valid Time
*/
