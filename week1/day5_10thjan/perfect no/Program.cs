using System;

class PerfectNum
{
    static void Main()
    {
        Console.Write("Enter number: ");
        int n = int.Parse(Console.ReadLine());
        int output;

        if (n < 0) output = -2;
        else
        {
            int sum = 0;
            for (int i = 1; i < n; i++)
                if (n % i == 0) sum += i;
            output = (sum == n) ? 1 : -1;
        }
        Console.WriteLine("Output: " + output);
    }
}
