using System;

class Program
{
    static void Main()
    {
        string s = "babad";
        Console.WriteLine(LongestPalindromeLength(s));
    }

    static int LongestPalindromeLength(string s)
    {
        int maxLen = 1;

        for (int center = 0; center < s.Length; center++)
        {
            int l = center, r = center;
            while (l >= 0 && r < s.Length && s[l] == s[r])
            {
                maxLen = Math.Max(maxLen, r - l + 1);
                l--; r++;
            }

            l = center; r = center + 1;
            while (l >= 0 && r < s.Length && s[l] == s[r])
            {
                maxLen = Math.Max(maxLen, r - l + 1);
                l--; r++;
            }
        }
        return maxLen;
    }
}
