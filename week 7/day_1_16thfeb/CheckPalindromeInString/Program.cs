using System;

class CheckPalindromeInString
{
    static void Main()
    {
        string str = "madam";
        string rev = "";

        for (int i = str.Length - 1; i >= 0; i--)
            rev += str[i];

        if (str == rev)
            Console.WriteLine("Palindrome");
        else
            Console.WriteLine("Not Palindrome");
    }
}