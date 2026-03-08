using System;
using System.Collections.Generic;

class UserProgramCode
{
    public static int diffIntArray(int[] input1)
    {
        int n = input1.Length;

        if (n == 1 || n > 10)
            return -2;

        HashSet<int> set = new HashSet<int>();

        foreach (int num in input1)
        {
            if (num < 0)
                return -1;

            if (!set.Add(num))
                return -3;
        }

        int min = input1[0];
        int maxDiff = 0;

        for (int i = 1; i < n; i++)
        {
            if (input1[i] > min)
                maxDiff = Math.Max(maxDiff, input1[i] - min);
            else
                min = input1[i];
        }

        return maxDiff;
    }
}
