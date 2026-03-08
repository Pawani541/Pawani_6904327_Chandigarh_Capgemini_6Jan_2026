using System;

class SwapMid
{
    static void Main()
    {
        Console.Write("Enter size of array: ");
        int size = int.Parse(Console.ReadLine());
        int[] output;

        if (size < 0)
        {
            output = new int[] { -2 };
        }
        else if (size % 2 == 0)
        {
            output = new int[] { -3 };
        }
        else
        {
            int[] arr = new int[size];
            bool neg = false;
            output = new int[size];

            Console.WriteLine("Enter elements:");
            for (int i = 0; i < size; i++)
            {
                Console.Write("arr[" + i + "] = ");
                arr[i] = int.Parse(Console.ReadLine());
                if (arr[i] < 0) neg = true;
            }

            if (neg) output[0] = -1;
            else
            {
                int mid = size / 2;
                for (int i = 0; i < size; i++)
                    output[i] = arr[size - 1 - i];
            }
        }

        Console.Write("Output: ");
        foreach (int x in output) Console.Write(x + " ");
    }
}
