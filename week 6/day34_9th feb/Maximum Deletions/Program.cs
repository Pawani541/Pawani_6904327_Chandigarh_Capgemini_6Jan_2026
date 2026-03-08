using System;
class MaxConsecutiveDeletions
{
    static void Main()
    {
        string s = "aabbcc";
        int maxDeletions = s.Length / 2;

        Console.WriteLine("Input:" + s);
        Console.WriteLine("Maximum Deletions: " + maxDeletions);
    }
}