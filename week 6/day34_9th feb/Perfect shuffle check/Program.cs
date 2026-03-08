using System;

class Program
{
    static void Main()
    {
        Console.WriteLine(IsPerfectShuffle("abc", "def", "adbcef"));
    }

    static bool IsPerfectShuffle(string x, string y, string z)
    {
        if (x.Length + y.Length != z.Length) return false;

        int i = 0, j = 0;

        for (int k = 0; k < z.Length; k++)
        {
            if (i < x.Length && z[k] == x[i])
                i++;
            else if (j < y.Length && z[k] == y[j])
                j++;
            else
                return false;
        }
        return true;
    }
}
