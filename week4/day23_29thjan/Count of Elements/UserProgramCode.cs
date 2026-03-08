using System;

class UserProgramCode
{
    public static int GetCount(int size, string[] input1, char input2)
    {
        int count = 0;

        foreach (string s in input1)
        {
            foreach (char ch in s)
            {
                if (!char.IsLetter(ch))
                    return -2;
            }

            if (char.ToLower(s[0]) == char.ToLower(input2))
                count++;
        }

        if (count == 0)
            return -1;

        return count;
    }
}
