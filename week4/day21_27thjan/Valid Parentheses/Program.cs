using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        Stack<char> stack = new Stack<char>();
        bool valid = true;

        foreach (char ch in s)
        {
            if (ch == '(' || ch == '{' || ch == '[')
            {
                stack.Push(ch);
            }
            else
            {
                if (stack.Count == 0)
                {
                    valid = false;
                    break;
                }

                char top = stack.Pop();

                if ((ch == ')' && top != '(') ||
                    (ch == '}' && top != '{') ||
                    (ch == ']' && top != '['))
                {
                    valid = false;
                    break;
                }
            }
        }

        if (stack.Count != 0) valid = false;

        Console.WriteLine(valid ? "YES" : "NO");
    }
}
