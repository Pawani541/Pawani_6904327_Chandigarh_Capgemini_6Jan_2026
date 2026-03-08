using System;

class Program
{
    static bool IsPrime(int num)
    {
        if (num <= 1) return false;
        for (int i = 2; i * i <= num; i++)
        {
            if (num % i == 0) return false;
        }
        return true;
    }

    static void Main()
    {
        int input = 20;
        long output = 0;

        if (input < 0)
        {
            output = -1;
        }
        else if (input > 32767)
        {
            output = -2;
        }
        else
        {
            for (int i = 2; i <= input; i++)
            {
                if (IsPrime(i))
                {
                    output += (long)i * i * i;
                }
            }
        }

        Console.WriteLine("Output: " + output);
    }
}
