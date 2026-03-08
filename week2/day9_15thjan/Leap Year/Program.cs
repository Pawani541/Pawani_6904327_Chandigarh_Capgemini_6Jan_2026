using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter year: ");
        int y = Convert.ToInt32(Console.ReadLine());

        if (y < 0) { Console.WriteLine(-1); return; }

        if ((y % 400 == 0) || (y % 4 == 0 && y % 100 != 0))
            Console.WriteLine("Leap Year");
        else
            Console.WriteLine("Not Leap Year");
    }
}
