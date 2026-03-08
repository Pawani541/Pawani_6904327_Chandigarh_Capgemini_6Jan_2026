using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a number:");
        int n = int.Parse(Console.ReadLine());

        int root = (int)Math.Sqrt(n);

        int lower = root * root;
        int upper = (root + 1) * (root + 1);

        int result = (n - lower < upper - n) ? lower : upper;

        Console.WriteLine("Closest perfect square: " + result);
    }
}
