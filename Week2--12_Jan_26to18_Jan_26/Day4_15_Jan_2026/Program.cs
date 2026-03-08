using System;

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

        int temp = n, rev = 0;
        while (temp > 0)
        {
            rev = rev * 10 + temp % 10;
            temp /= 10;
        }

        if (rev == n) Console.WriteLine(1);
        else Console.WriteLine(-2);
    }
}
