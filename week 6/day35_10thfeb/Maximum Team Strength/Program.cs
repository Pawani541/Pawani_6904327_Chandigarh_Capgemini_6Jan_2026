using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter employee skills (space separated):");
        int[] skills = Console.ReadLine().Split().Select(int.Parse).ToArray();

        Console.WriteLine("Enter team sizes (space separated):");
        int[] teamSizes = Console.ReadLine().Split().Select(int.Parse).ToArray();

        Array.Sort(skills);

        int index = skills.Length - 1;
        int totalStrength = 0;

        foreach (int size in teamSizes.OrderByDescending(x => x))
        {
            int max = skills[index];
            int min = skills[index - size + 1];

            totalStrength += max + min;
            index -= size;
        }

        Console.WriteLine("Maximum Total Strength: " + totalStrength);
    }
}
