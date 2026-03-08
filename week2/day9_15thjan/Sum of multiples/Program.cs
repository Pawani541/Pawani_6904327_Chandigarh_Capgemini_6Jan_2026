using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter input1: ");
        int x = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter input2: ");
        int n = Convert.ToInt32(Console.ReadLine());

        if (x < 0) 
        {
            Console.WriteLine(-1); return;
        }
        if (n > 32627) 
        {
            Console.WriteLine(-2); return; 
        }

        int sum = 0;
        for (int i = x; i <= n; i += x)
            sum += i;

        Console.WriteLine(sum);
    }
}
