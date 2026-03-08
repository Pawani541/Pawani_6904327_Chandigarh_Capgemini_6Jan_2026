using System;
using System.Collections.Generic;

class UserProgramCode
{
    public static List<int> GetElements(List<int> input1, int input2)
    {
        List<int> result = new List<int>();

        foreach (int num in input1)
        {
            if (num < input2)
                result.Add(num);
        }

        if (result.Count == 0)
        {
            result.Add(-1);
            return result;
        }

        result.Sort();
        result.Reverse();

        return result;
    }
}
