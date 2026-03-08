using System;

class LuckyNumber
{
    // Function to calculate sum of digits
    static int SumOfDigits(int num)
    {
        int sum = 0;
        while (num > 0)
        {
            sum += num % 10;
            num /= 10;
        }
        return sum;
    }

    // Function to check prime
    static bool IsPrime(int num)
    {
        if (num <= 1) return false;
        for (int i = 2; i <= Math.Sqrt(num); i++)
        {
            if (num % i == 0)
                return false;
        }
        return true;
    }

    static void Main()
    {
        int m = int.Parse(Console.ReadLine());
        int n = int.Parse(Console.ReadLine());

        int count = 0;

        for (int x = m; x <= n; x++)
        {
            // Only non-prime numbers
            if (!IsPrime(x))
            {
                int sumX = SumOfDigits(x);
                int sumXSq = SumOfDigits(x * x);

                if (sumXSq == sumX * sumX)
                {
                    count++;
                }
            }
        }

        Console.WriteLine(count);
    }
}
