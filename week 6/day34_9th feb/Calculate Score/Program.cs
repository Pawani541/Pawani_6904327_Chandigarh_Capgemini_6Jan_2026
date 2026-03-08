using System;

class Program
{
    static void Main()
    {
        string str = "ABCBAAAA";
        int score = CalculateScore(str);
        Console.WriteLine("Final Score = " + score);
    }

    static int CalculateScore(string str)
    {
        int score = 0;

        // Check palindromes of length 4
        for (int i = 0; i <= str.Length - 4; i++)
        {
            string sub = str.Substring(i, 4);
            if (IsPalindrome(sub))
                score += 5;
        }

        // Check palindromes of length 5
        for (int i = 0; i <= str.Length - 5; i++)
        {
            string sub = str.Substring(i, 5);
            if (IsPalindrome(sub))
                score += 10;
        }

        return score;
    }

    static bool IsPalindrome(string s)
    {
        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            if (s[left] != s[right])
                return false;

            left++;
            right--;
        }

        return true;
    }
}
