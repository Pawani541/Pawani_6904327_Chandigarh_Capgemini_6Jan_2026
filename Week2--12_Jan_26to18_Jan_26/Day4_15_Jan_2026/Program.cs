using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter size: ");
        int n = Convert.ToInt32(Console.ReadLine());

        if (n < 0) { 
            Console.WriteLine(-2); return; 
        }

        int[] arr = new int[n];
        Console.WriteLine("Enter elements:");

        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
            if (arr[i] < 0) 
            {
                Console.WriteLine(-1); return; 
            }
        }

        int min = arr[0];
        int max = arr[0];

        for (int i = 1; i < n; i++)
        {
            if (arr[i] < min) min = arr[i];
            if (arr[i] > max) max = arr[i];
        }

        Console.WriteLine(max * min);
    }
}
