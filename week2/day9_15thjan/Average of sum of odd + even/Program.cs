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

        int[] arr = new int[n];
        int odd = 0, even = 0;

        Console.WriteLine("Enter numbers:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
            if (arr[i] < 0) 
            {
                Console.WriteLine(-1);
                return;
            }

            if (arr[i] % 2 == 0)
                even += arr[i];
            else odd += arr[i];
        }

        Console.WriteLine((odd + even) / 2);
    }
}
