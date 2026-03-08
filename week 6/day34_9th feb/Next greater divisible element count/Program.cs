using System;

class Program
{
    static void Main()
    {
        int[] arr = { 2, 3, 4, 9, 8 };
        int count = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[j] > arr[i] && arr[j] % arr[i] == 0)
                {
                    count++;
                    break;
                }
            }
        }

        Console.WriteLine(count);
    }
}
