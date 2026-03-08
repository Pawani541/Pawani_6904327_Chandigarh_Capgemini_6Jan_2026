using System;

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

        int[] a = new int[n];
        int[] b = new int[n];
        int[] result = new int[n];

        Console.WriteLine("Enter array1:");
        for (int i = 0; i < n; i++)
        {
            a[i] = Convert.ToInt32(Console.ReadLine());
            if (a[i] < 0) 
            {
                Console.WriteLine(-1); 
                return;
            }
        }

        Console.WriteLine("Enter array2:");
        for (int i = 0; i < n; i++)
        {
            b[i] = Convert.ToInt32(Console.ReadLine());
            if (b[i] < 0) 
            {
                Console.WriteLine(-1); 
                return;
            }
        }

        for (int i = 0; i < n; i++)
            result[i] = a[i] + b[n - 1 - i];

        Console.WriteLine(string.Join(", ", result));
    }
}
