using System;
using System.Collections.Generic;
using System.Linq;

class UniqueWords
{
    static void Main()
    {
        List<string> words = new List<string>
        {
            "listen", "silent", "hello", "world", "abc", "cba"
        };

        Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

        // Group words by sorted characters
        foreach (string word in words)
        {
            char[] chars = word.ToCharArray();
            Array.Sort(chars);
            string key = new string(chars);

            if (!map.ContainsKey(key))
                map[key] = new List<string>();

            map[key].Add(word);
        }

        // Collect unique words
        List<string> result = new List<string>();

        foreach (var entry in map)
        {
            if (entry.Value.Count == 1)
            {
                result.Add(entry.Value[0]);
            }
        }

        Console.WriteLine("Unique words:");
        foreach (string word in result)
        {
            Console.WriteLine(word);
        }
    }
}
