using System;

public class UserProgramCode
{
    public static string replaceString(string input1, int input2, char input3)
    {
        // Check for invalid string
        foreach (char c in input1)
            if (!char.IsLetter(c) && c != ' ')
                return "-1";

        // Check positive number
        if (input2 <= 0)
            return "-2";

        // Check special character
        if (char.IsLetterOrDigit(input3))
            return "-3";

        string[] words = input1.Split(' ');

        if (input2 <= words.Length)
            words[input2 - 1] = new string(input3, words[input2 - 1].Length);

        return string.Join(" ", words).ToLower();
    }
}
