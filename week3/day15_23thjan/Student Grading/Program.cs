using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<int, int> grades = new Dictionary<int, int>();

        Console.WriteLine("How many students?");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter roll number:");
            int roll = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter grade:");
            int grade = int.Parse(Console.ReadLine());

            grades.Add(roll, grade);
        }

        // Average using Func
        Func<double> average = () => grades.Values.Average();
        Console.WriteLine("Average Grade: " + average());

        // Risk students using Predicate
        Console.WriteLine("Enter risk threshold:");
        int threshold = int.Parse(Console.ReadLine());

        Predicate<int> isRisk = g => g < threshold;

        Console.WriteLine("Students at risk:");
        foreach (var s in grades)
        {
            if (isRisk(s.Value))
                Console.WriteLine("Roll No: " + s.Key);
        }

        // Update grade
        Console.WriteLine("Enter roll number to update:");
        int updateRoll = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter new grade:");
        int newGrade = int.Parse(Console.ReadLine());

        grades[updateRoll] = newGrade;

        Console.WriteLine("Updated Grades:");
        foreach (var s in grades)
            Console.WriteLine(s.Key + " : " + s.Value);
    }
}
