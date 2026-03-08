using System;

class CubePrimeSum
{
    static bool IsPrime(int n)
    {
        if (n < 2) return false;
        for (int i = 2; i * i <= n; i++)
            if (n % i == 0) return false;
        return true;
    }

    static void Main()
    {
        Console.Write("Enter n: ");
        int n = int.Parse(Console.ReadLine());
        int output = 0;

        if (n < 0) output = -1;
        else if (n > 32676) output = -2;
        else
        {
            for (int i = 1; i <= n; i++)
                if (IsPrime(i))
                    output += i * i * i;
        }

        Console.WriteLine("Output: " + output);
    }
}
