using System;

class ConcatenateStringWithoutFunctions
{
    static void Main()
    {
        char[] a = { 'H', 'e', 'l', 'l', 'o' };
        char[] b = { 'W', 'o', 'r', 'l', 'd' };

        char[] c = new char[a.Length + b.Length];

        int i;
        for (i = 0; i < a.Length; i++)
            c[i] = a[i];

        for (int j = 0; j < b.Length; j++)
            c[i + j] = b[j];

        Console.WriteLine(new string(c));
    }
}