using System;
class RemoveLastOccurence
{
    static void Main()
    {
        string input = "I am a programmer. I learn at Codeforwin.";
        string word = "I";

        int lastIndex = input.LastIndexOf(word);

        if (lastIndex != -1)
        {
            input = input.Remove(lastIndex, word.Length);
        }
        Console.WriteLine("Result string");
        Console.WriteLine(input);
    }
}