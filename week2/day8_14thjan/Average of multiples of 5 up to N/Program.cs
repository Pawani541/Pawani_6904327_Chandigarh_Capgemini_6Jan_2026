using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter the value of N: ");
        int input1 = Convert.ToInt32(Console.ReadLine());
        int sum = 0, count = 0;
        int result;

        if (input1 < 0)
            result = -1;
        else if (input1 > 500)
            result = -2;
        else
        {
            for (int i = 1; i <= input1; i++)
            {
                if (i % 5 == 0)
                {
                    sum += i;
                    count++;
                }
            }
            result = (count == 0) ? 0 : sum / count;
        }

        Console.WriteLine("Output = " + result);
    }
}
