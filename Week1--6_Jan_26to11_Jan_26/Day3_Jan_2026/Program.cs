using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter marks (0-100): ");
        int marks = int.Parse(Console.ReadLine());

        char grade;

        if (marks >= 90)
            grade = 'A';
        else if (marks >= 80)
            grade = 'B';
        else if (marks >= 70)
            grade = 'C';
        else if (marks >= 60)
            grade = 'D';
        else
            grade = 'F';

        Console.WriteLine("Your Grade is: " + grade);
    }
}
