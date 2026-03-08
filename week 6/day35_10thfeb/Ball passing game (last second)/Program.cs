using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter number of friends:");
        int n = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter total seconds:");
        int t = int.Parse(Console.ReadLine());

        int from = (t - 1) % n + 1;
        int to = t % n + 1;

        Console.WriteLine("Last pass: Friend " + from + " -> Friend " + to);
    }
}
