using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter sequence elements:");
        int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int n = arr.Length + 1;
        int expectedSum = n * (n + 1) / 2;
        int actualSum = arr.Sum();

        int missing = expectedSum - actualSum;

        Console.WriteLine("Missing number: " + missing);
    }
}
