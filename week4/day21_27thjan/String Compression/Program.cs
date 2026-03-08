using System;
using System.Text;

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        StringBuilder result = new StringBuilder();

        int count = 1;

        for (int i = 1; i <= s.Length; i++)
        {
            if (i < s.Length && s[i] == s[i - 1])
            {
                count++;
            }
            else
            {
                result.Append(s[i - 1]);
                result.Append(count);
                count = 1;
            }
        }

        Console.WriteLine(result.ToString());
    }
}
