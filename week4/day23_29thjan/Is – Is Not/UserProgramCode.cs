using System.Text.RegularExpressions;

class UserProgramCode
{
    public static string negativeString(string input)
    {
        // \b ensures word boundary (no letter before or after)
        return Regex.Replace(input, @"\bis\b", "is not");
    }
}
