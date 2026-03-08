using System;

class Program
{
    static void Main()
    {
        int[] input1 = { 1, 2, 2, 3, 3 };
        int input2 = 5;
        int input3 = 2;

        int output = 0;

        if (input2 < 0)
        {
            output = -2;
        }
        else if (input3 < 0)
        {
            output = -3;
        }
        else
        {
            for (int i = 0; i < input2; i++)
            {
                if (input1[i] < 0)
                {
                    output = -1;
                    break;
                }

                if (input1[i] == input3)
                {
                    output++;
                }
            }
        }

        Console.WriteLine("Output: " + output);
    }
}
