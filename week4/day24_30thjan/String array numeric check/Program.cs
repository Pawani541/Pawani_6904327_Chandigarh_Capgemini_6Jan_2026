using System;

class Program
{
    static void Main()
    {
        string[] arr = { "23", "24.5" };

        foreach (string s in arr)
        {
            // If any value is non-numeric
            if (!double.TryParse(s, out _))
            {
                Console.WriteLine("-1");
                return;
            }
        }

        Console.WriteLine("1");
    }
}

