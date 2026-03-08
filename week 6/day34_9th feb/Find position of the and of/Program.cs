using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter number of lines:");
        int n = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter " + n + " lines:");

        for (int i = 0; i < n; i++)
        {
            string line = Console.ReadLine();

            int posThe = line.IndexOf("the");
            int posOf = line.IndexOf("of");

            Console.WriteLine(posThe == -1 ? -1 : posThe);
            Console.WriteLine(posOf == -1 ? -1 : posOf);
        }
    }
}
