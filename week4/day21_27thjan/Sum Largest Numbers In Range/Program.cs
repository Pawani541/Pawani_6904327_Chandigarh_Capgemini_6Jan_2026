using System;
using System.Collections.Generic;

class UserProgramCode
{
    public static int largestNumber(int[] input1)
    {
        bool hasNegative = false;
        bool hasInvalid = false;

        HashSet<int> unique = new HashSet<int>();

        foreach (int num in input1)
        {
            if (num < 0) hasNegative = true;
            if (num == 0 || num > 100) hasInvalid = true;
            unique.Add(num);
        }

        if (hasNegative && hasInvalid) return -3;
        if (hasNegative) return -1;
        if (hasInvalid) return -2;

        int sum = 0;

        for (int start = 1; start <= 91; start += 10)
        {
            int end = start + 9;
            int max = int.MinValue;
            bool found = false;

            foreach (int num in unique)
            {
                if (num >= start && num <= end)
                {
                    if (!found || num > max)
                    {
                        max = num;
                        found = true;
                    }
                }
            }

            if (found)
                sum += max;
        }

        return sum;
    }
}

class Program
{
    static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        int[] arr = new int[n];

        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine(UserProgramCode.largestNumber(arr));
    }
}
