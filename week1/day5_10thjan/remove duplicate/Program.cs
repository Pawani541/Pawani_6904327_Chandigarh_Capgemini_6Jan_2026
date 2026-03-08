using System;
using System.Collections.Generic;

class RemoveDup
{
    static void Main()
    {
        Console.Write("Enter size: ");
        int size = int.Parse(Console.ReadLine());
        List<int> unique = new List<int>();

        if (size < 0)
        {
            Console.WriteLine("-2");
            return;
        }

        int[] arr = new int[size];
        bool neg = false;
        Console.WriteLine("Enter elements:");
        for (int i = 0; i < size; i++)
        {
            Console.Write("arr[" + i + "] = ");
            arr[i] = int.Parse(Console.ReadLine());
            if (arr[i] < 0) neg = true;
        }

        if (neg)
        {
            Console.WriteLine("-1");
            return;
        }

        foreach (int v in arr)
            if (!unique.Contains(v))
                unique.Add(v);

        Console.Write("Output: ");
        foreach (int x in unique) Console.Write(x + " ");
    }
}
