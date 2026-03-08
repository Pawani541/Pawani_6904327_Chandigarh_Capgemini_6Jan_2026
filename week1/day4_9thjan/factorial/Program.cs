using System;
class FactProg
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int output1 = 0;

        if (n < 0) output1 = -1;
        else if (n > 7) output1 = -2;
        else
        {
            int fact = 1;
            for (int i = 1; i <= n; i++) fact *= i;
            output1 = fact;
        }
        Console.WriteLine(output1);
    }
}

