using System;

class LuckyString
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        string str = Console.ReadLine();

        // Invalid case
        if (n > str.Length)
        {
            Console.WriteLine("Invalid");
            return;
        }

        int required = n / 2;

        // Check all substrings of length n
        for (int i = 0; i <= str.Length - n; i++)
        {
            string sub = str.Substring(i, n);

            // Check condition 1: only P, S, G
            bool validChars = true;
            foreach (char c in sub)
            {
                if (c != 'P' && c != 'S' && c != 'G')
                {
                    validChars = false;
                    break;
                }
            }

            if (!validChars)
                continue;

            // Check condition 2: at least n/2 consecutive P or S or G
            int count = 1;
            for (int j = 1; j < sub.Length; j++)
            {
                if (sub[j] == sub[j - 1])
                {
                    count++;
                    if (count >= required)
                    {
                        Console.WriteLine("Yes");
                        return;
                    }
                }
                else
                {
                    count = 1;
                }
            }
        }

        // If no lucky substring found
        Console.WriteLine("No");
    }
}
