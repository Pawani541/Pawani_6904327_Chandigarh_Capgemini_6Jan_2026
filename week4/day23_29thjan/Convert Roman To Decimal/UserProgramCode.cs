using System;
using System.Collections.Generic;

class UserProgramCode
{
    public static int convertRomanToDecimal(string input)
    {
        Dictionary<char, int> map = new Dictionary<char, int>()
        {
            {'I',1}, {'V',5}, {'X',10},
            {'L',50}, {'C',100}, {'D',500}, {'M',1000}
        };

        int sum = 0;

        foreach (char ch in input)
        {
            if (!map.ContainsKey(ch))
                return -1;

            sum += map[ch];
        }

        return sum;
    }
}
