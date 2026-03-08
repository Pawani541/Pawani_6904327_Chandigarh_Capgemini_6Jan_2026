using System;
class DeleteAlternatingCharacters
{
    static void Main()
    {
        string s = "abcdef";
        string result = "";
        for(int i =0; i<s.Length; i++)
        {
            if(i%2 ==0)
            {
                result += s[i];
            }
        }
        Console.WriteLine("Input: " + s);
        Console.WriteLine("Output: " + result);
    }
}