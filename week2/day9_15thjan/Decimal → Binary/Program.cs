using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Enter number: ");
        int n = Convert.ToInt32(Console.ReadLine());

        if (n < 0) 
        { 
            Console.WriteLine(-1); 
            return;
        }
        if (n == 0) 
        {
            Console.WriteLine(0);
            return;
        }

        List<int> bits = new List<int>();
        while (n > 0)
        {
            bits.Add(n % 2);
            n /= 2;
        }
        bits.Reverse();

        for (int i = 0; i < bits.Count; i++)
            Console.Write(bits[i] + " ");
    }
}
