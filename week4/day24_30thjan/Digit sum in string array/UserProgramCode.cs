using System;

public class UserProgramCode
{
    // This method calculates sum of all digits in string array
    public static int sumOfDigits(string[] input1)
    {
        int sum = 0;

        foreach (string s in input1)
        {
            foreach (char c in s)
            {
                // If special character found
                if (!char.IsLetterOrDigit(c) && c != ' ')
                    return -1;

                // If digit found, add its value
                if (char.IsDigit(c))
                    sum += c - '0';
            }
        }
        return sum;
    }
}
