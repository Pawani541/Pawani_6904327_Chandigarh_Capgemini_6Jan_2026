using System;
using System.Text.RegularExpressions;

class UserProgramCode
{
    public static int validateTime(string time)
    {
        // 12-hour format with case-insensitive am/pm
        string pattern = @"^(0[1-9]|1[0-2]):[0-5][0-9]\s(?i)(am|pm)$";

        if (!string.IsNullOrEmpty(time) && Regex.IsMatch(time, pattern))
            return 1;
        else
            return -1;
    }
}

class Program
{
    static void Main()
    {
        // Taking input from user
        string input = Console.ReadLine();

        int result = UserProgramCode.validateTime(input);

        if (result == 1)
            Console.WriteLine("Valid time format");
        else
            Console.WriteLine("Invalid time format");
    }
}
