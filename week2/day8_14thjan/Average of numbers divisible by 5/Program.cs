using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter the limit: ");
        int input1 = Convert.ToInt32(Console.ReadLine());
        int sum = 0, count = 0, avg = 0;

        if (input1 < 0)
        {
            avg = -1;
        }
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
            avg = (count == 0) ? 0 : sum / count;
        }

        Console.WriteLine("Output = " + avg);
    }
}
