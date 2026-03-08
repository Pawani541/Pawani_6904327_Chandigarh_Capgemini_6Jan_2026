using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter date (dd/MM/yyyy):");
        string date = Console.ReadLine();

        DateTime dt;

        // Checking exact date format
        if (!DateTime.TryParseExact(date, "dd/MM/yyyy", null,
            System.Globalization.DateTimeStyles.None, out dt))
        {
            Console.WriteLine("-1");
            return;
        }

        // Adding 1 year and finding day
        Console.WriteLine(dt.AddYears(1).DayOfWeek);
    }
}
