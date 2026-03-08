using System;
class InserCharacter
{
    static void Main()
    {
        string str = "Hello World";
        char ch = 'A';
        int position = 3;
        string result = str.Substring(0, position) + ch + str.Substring(position);
        Console.WriteLine("Original String: " + str);
        Console.WriteLine("After Inserting Character: " + result);
    }
}