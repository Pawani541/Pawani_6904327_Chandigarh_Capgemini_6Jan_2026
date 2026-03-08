using System;

class Program
{
    static void Main()
    {
        int[] arr = { 5, 7, 9, 12 };
        int size = arr.Length;
        int search = 7;
        int output = -3;

        if (size < 0)
        {
            output = -2;
        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                if (arr[i] < 0)
                {
                    output = -1;
                    break;
                }
                if (arr[i] == search)
                {
                    output = 1;
                }
            }
        }
        Console.WriteLine("Output: " + output);
    }
}
