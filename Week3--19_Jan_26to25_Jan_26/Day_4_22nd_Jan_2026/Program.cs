using System;
using System.Threading;

class DataImport
{
    static void Main()
    {
        Console.WriteLine("Enter total records:");
        int total = int.Parse(Console.ReadLine());

        int imported = 0;

        for (int i = 1; i <= total; i++)
        {
            Thread.Sleep(10);
            imported++;

            if (imported % 100 == 0)
                Console.WriteLine("Imported: " + imported);

            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
                Console.WriteLine("\nImport Cancelled!");
                return;
            }
        }

        Console.WriteLine("\nImport Completed!");
        Console.WriteLine("Total Imported = " + imported);
    }
}
