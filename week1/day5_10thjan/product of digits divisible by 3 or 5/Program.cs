using System;

class ProductDigit
{
    static void Main()
    {
        Console.Write("Enter number: ");
        int n = int.Parse(Console.ReadLine());
        int output = 0;

        if (n < 0) output = -1;
        else if (n % 3 == 0 || n % 5 == 0) output = -2;
        else
        {
            int product = 1, num = n;
            while (num > 0)
            {
                product *= num % 10;
                num /= 10;
            }
            output = (product % 3 == 0 || product % 5 == 0) ? 1 : 0;
        }
        Console.WriteLine("Output: " + output);
    }
}
