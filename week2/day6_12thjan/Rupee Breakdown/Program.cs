using System;

class RupeeBreak
{
    static void Main()
    {
        Console.Write("Enter amount: ");
        int n = int.Parse(Console.ReadLine());
        int output = 0;

        if (n < 0) output = -1;
        else
        {
            int[] denom = { 500, 100, 50, 10, 1 };

            foreach (int d in denom)
            {
                int count = n / d;
                output += count;
                n = n % d;
            }
        }

        Console.WriteLine("Output: " + output);
    }
}
