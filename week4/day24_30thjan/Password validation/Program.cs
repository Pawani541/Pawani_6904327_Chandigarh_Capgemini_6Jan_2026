using System;

class Program
{
    static void Main()
    {
        string pwd = Console.ReadLine();

        // Check start condition
        if (char.IsDigit(pwd[0]) || !char.IsLetterOrDigit(pwd[0]))
        {
            Console.WriteLine("-1");
            return;
        }

        // Check end condition
        if (!char.IsLetterOrDigit(pwd[pwd.Length - 1]))
        {
            Console.WriteLine("-1");
            return;
        }

        // Special character check
        if (!(pwd.Contains("@") || pwd.Contains("#") || pwd.Contains("_")))
        {
            Console.WriteLine("-1");
            return;
        }

        bool hasLetter = false, hasDigit = false;

        foreach (char c in pwd)
        {
            if (char.IsLetter(c)) hasLetter = true;
            if (char.IsDigit(c)) hasDigit = true;
        }

        if (pwd.Length >= 8 && hasLetter && hasDigit)
            Console.WriteLine("1");
        else
            Console.WriteLine("-1");
    }
}

