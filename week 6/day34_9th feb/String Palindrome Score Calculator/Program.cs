using System;

class PalindromeScore
{
    static bool IsPalindrome(string s)
    {
        int i = 0, j = s.Length - 1;
        while (i < j)
        {
            if (s[i] != s[j])
                return false;
            i++;
            j--;
        }
        return true;
    }

    static void Main()
    {
        Console.Write("Enter string: ");
        string str = Console.ReadLine();

        int score = 0;

        // Check palindromes of length 4 and 5
        for (int len = 4; len <= 5; len++)
        {
            for (int i = 0; i <= str.Length - len; i++)
            {
                string sub = str.Substring(i, len);

                if (IsPalindrome(sub))
                {
                    if (len == 4)
                        score += 5;
                    else if (len == 5)
                        score += 10;
                }
            }
        }

        Console.WriteLine("Final Score: " + score);
    }
}
