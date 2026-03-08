using System;

class CountCharFrequency
{
    static void Main()
    {
        string str = "programming";

        for (char c = 'a'; c <= 'z'; c++)
        {
            int count = 0;

            foreach (char ch in str)
            {
                if (ch == c)
                    count++;
            }

            if (count > 0)
                Console.WriteLine(c + " : " + count);
        }
    }
}