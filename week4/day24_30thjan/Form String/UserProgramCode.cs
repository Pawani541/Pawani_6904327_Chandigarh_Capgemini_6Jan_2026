using System;

public class UserProgramCode
{
    public static string formString(string[] input1, int input2)
    {
        string result = "";

        foreach (string s in input1)
        {
            foreach (char c in s)
                if (!char.IsLetter(c))
                    return "-1";

            if (s.Length >= input2)
                result += s[input2 - 1];
            else
                result += "$";
        }
        return result;
    }
}
