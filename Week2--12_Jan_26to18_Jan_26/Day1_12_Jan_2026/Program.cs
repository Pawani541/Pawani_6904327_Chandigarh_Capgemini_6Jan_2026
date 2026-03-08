using System;
using System.Collections.Generic;

class RemoveRepeats
{
    static void Main()
    {
        Console.Write("Enter size of array: ");
        int size = int.Parse(Console.ReadLine());
        int[] output;

        if (size < 0)
        {
            output = new int[] { -1 };
        }
        else
        {
            int[] arr = new int[size];
            bool neg = false;
            List<int> unique = new List<int>();

            Console.WriteLine("Enter elements:");
            for (int i = 0; i < size; i++)
            {
                Console.Write("arr[" + i + "] = ");
                arr[i] = int.Parse(Console.ReadLine());
                if (arr[i] < 0) neg = true;
            }

            if (neg)
            {
                output = new int[] { -1 };
            }
            else
            {
                foreach (int v in arr)
                    if (!unique.Contains(v))
                        unique.Add(v);
                output = unique.ToArray();
            }
        }

        Console.Write("Output: ");
        foreach (int v in output) Console.Write(v + " ");
    }
}
