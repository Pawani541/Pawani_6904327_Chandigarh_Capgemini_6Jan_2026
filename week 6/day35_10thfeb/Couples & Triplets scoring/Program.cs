using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter array elements:");
        int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int score = 0;

        // Couples
        for (int i = 0; i < arr.Length - 1; i++)
        {
            int sum = arr[i] + arr[i + 1];
            if (sum % 2 == 0)
                score += 5;
        }

        // Triplets
        for (int i = 0; i < arr.Length - 2; i++)
        {
            int sum = arr[i] + arr[i + 1] + arr[i + 2];
            int product = arr[i] * arr[i + 1] * arr[i + 2];

            if (sum % 2 != 0 && product % 2 == 0)
                score += 10;
        }

        Console.WriteLine("Final Score: " + score);
    }
}
