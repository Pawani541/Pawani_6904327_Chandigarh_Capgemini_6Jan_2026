using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter N (array size):");
        int N = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter array elements:");
        int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int count = 0;

        for (int i = 0; i < arr.Length - 1; i++)
        {
            int sum = arr[i] + arr[i + 1];

            if (sum % N == 0)
                count++;
        }

        Console.WriteLine("Total couples divisible by N: " + count);
    }
}
