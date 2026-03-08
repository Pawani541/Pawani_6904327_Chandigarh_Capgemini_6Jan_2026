using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter array elements:");
        int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

        Console.WriteLine("Enter divisor d:");
        int d = int.Parse(Console.ReadLine());

        int count = 0;
        int n = arr.Length;

        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                for (int k = j + 1; k < n; k++)
                    if ((arr[i] + arr[j] + arr[k]) % d == 0)
                        count++;

        Console.WriteLine("Triplets count: " + count);
    }
}
