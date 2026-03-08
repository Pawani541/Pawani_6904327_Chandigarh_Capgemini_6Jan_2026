using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Enter size: ");
        int n = Convert.ToInt32(Console.ReadLine());

        if (n < 0)
        {
            Console.WriteLine(-2);
            return;
        }

        int[] arr = new int[n];
        Console.WriteLine("Enter elements:");
        for (int i = 0; i < n; i++) arr[i] = Convert.ToInt32(Console.ReadLine());

        if (arr.Any(x => x < 0))
        {
            Console.WriteLine(-1);
            return;
        }

        int ans = arr.OrderByDescending(x => x).Distinct().Skip(1).First();
        Console.WriteLine(ans);
    }
}
