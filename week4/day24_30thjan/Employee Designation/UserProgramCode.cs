using System;
using System.Collections.Generic;

public class UserProgramCode
{
    public static string[] getEmployee(string[] input1, string input2)
    {
        // Validate input
        foreach (string s in input1)
            if (!IsValid(s) || !IsValid(input2))
                return new string[] { "Invalid Input" };

        List<string> employees = new List<string>();
        bool allSame = true;

        // Check if all employees have same designation
        for (int i = 1; i < input1.Length; i += 2)
            if (!input1[i].Equals(input2, StringComparison.OrdinalIgnoreCase))
                allSame = false;

        if (allSame)
            return new string[] { "All employees belong to same " + input2 + " designation" };

        // Fetch employees of given designation
        for (int i = 0; i < input1.Length - 1; i += 2)
            if (input1[i + 1].Equals(input2, StringComparison.OrdinalIgnoreCase))
                employees.Add(input1[i]);

        if (employees.Count == 0)
            return new string[] { "No employee for " + input2 + " designation" };

        return employees.ToArray();
    }

    static bool IsValid(string s)
    {
        foreach (char c in s)
            if (!char.IsLetter(c) && c != ' ')
                return false;
        return true;
    }
}
