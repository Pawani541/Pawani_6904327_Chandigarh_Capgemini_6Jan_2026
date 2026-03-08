using System;
using System.Collections.Generic;

class MaxRepeat
{
    static void Main()
    {
        Console.Write("Enter size of array: ");
        int size = int.Parse(Console.ReadLine());

        int[] arr = new int[size];
        Dictionary<int, int> freq = new Dictionary<int, int>();

        Console.WriteLine("Enter array elements:");
        for (int i = 0; i < size; i++)
        {
            Console.Write("arr[" + i + "] = ");
            arr[i] = int.Parse(Console.ReadLine());

            if (freq.ContainsKey(arr[i]))
                freq[arr[i]]++;
            else
                freq[arr[i]] = 1;
        }

        int maxCount = 0;
        foreach (var p in freq)
            if (p.Value > maxCount)
                maxCount = p.Value;

        Console.WriteLine("\nOutput elements:");
        foreach (var p in freq)
            if (p.Value == maxCount)
                Console.Write(p.Key + " ");
    }
}
