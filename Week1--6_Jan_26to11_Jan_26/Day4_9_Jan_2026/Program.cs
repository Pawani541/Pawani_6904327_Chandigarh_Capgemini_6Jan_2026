using System;
class CountDigits
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int output1 = 0;

        if (n < 0) output1 = -1;
        else
        {
            output1 = n.ToString().Length;
        }
        Console.WriteLine(output1);
    }
}

