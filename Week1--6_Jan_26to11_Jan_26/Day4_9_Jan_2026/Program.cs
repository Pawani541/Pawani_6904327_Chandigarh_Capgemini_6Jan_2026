using System;

class EvenDigitSum
{
    static void Main()
    {
        Console.Write("Enter number: ");
        int n = int.Parse(Console.ReadLine());
        int output1 = 0;

        if (n < 0) output1 = -1;
        else if (n > 32767) output1 = -2;
        else
        {
            while (n > 0)
            {
                int d = n % 10;
                if (d % 2 == 0)
                    output1 += d;
                n /= 10;
            }
        }

        Console.WriteLine("\nOutput: " + output1);
    }
}
