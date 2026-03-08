using System;
class Armstrong
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int output1 = 0;

        if (n < 0) output1 = -1;
        else if (n > 999) output1 = -2;
        else
        {
            int temp = n, sum = 0;
            while (temp > 0)
            {
                int digit = temp % 10;
                sum += digit * digit * digit;
                temp /= 10;
            }
            output1 = (sum == n) ? 1 : 0;
        }
        Console.WriteLine(output1);
    }
}
