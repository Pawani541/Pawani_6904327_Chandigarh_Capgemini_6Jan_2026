using System;

class SortCharacterOfStringAlphabetically
{
    static void Main()
    {
        string str = "programming";
        char[] arr = str.ToCharArray();

        Array.Sort(arr);

        Console.WriteLine(new string(arr));
    }
}