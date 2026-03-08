using System;
using System.Text;

class UserProgramCode
{
    public static string nextString(string input1)
    {
        StringBuilder result = new StringBuilder();
        string vowels = "aeiouAEIOU";

        foreach (char ch in input1)
        {
            if (!char.IsLetter(ch))
                return "Invalid input";

            if (vowels.Contains(ch))
            {
                char next = ch;
                do
                {
                    next++;
                    if (next > 'z' && ch >= 'a') next = 'a';
                    if (next > 'Z' && ch <= 'Z') next = 'A';
                }
                while (vowels.Contains(next));

                result.Append(next);
            }
            else
            {
                char[] vowelArr = ch >= 'a'
                    ? new char[] { 'a', 'e', 'i', 'o', 'u' }
                    : new char[] { 'A', 'E', 'I', 'O', 'U' };

                foreach (char v in vowelArr)
                {
                    if (v > ch)
                    {
                        result.Append(v);
                        goto done;
                    }
                }

                result.Append(vowelArr[0]);
            done:;
            }
        }

        return result.ToString();
    }
}

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();
        Console.WriteLine(UserProgramCode.nextString(input));
    }
}
