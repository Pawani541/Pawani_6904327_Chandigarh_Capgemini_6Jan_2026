using System;
using System.Collections.Generic;

class UserProgramCode
{
    public static int getDonation(string[] input1, int input2)
    {
        HashSet<string> set = new HashSet<string>();

        foreach (string s in input1)
        {
            // duplicate check
            if (!set.Add(s))
                return -1;

            // special character check
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return -2;
            }
        }

        int sum = 0;

        foreach (string s in input1)
        {
            int location = Convert.ToInt32(s.Substring(3, 3));
            int donation = Convert.ToInt32(s.Substring(6, 3));

            if (location == input2)
                sum += donation;
        }

        return sum;
    }
}
