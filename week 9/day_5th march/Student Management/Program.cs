using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    class CollageManagement
    {
        Dictionary<string, Dictionary<string, int>> studentRecords =
            new Dictionary<string, Dictionary<string, int>>();

        Dictionary<string, Dictionary<string, int>> subjectsRecords =
            new Dictionary<string, Dictionary<string, int>>();

        List<string> studentInsertionOrder = new List<string>();

        public int AddStudent(string studentId, string subject, int marks)
        {
            if (!studentRecords.ContainsKey(studentId))
            {
                studentRecords[studentId] = new Dictionary<string, int>();
                studentInsertionOrder.Add(studentId);
            }

            if (!studentRecords[studentId].ContainsKey(subject))
            {
                studentRecords[studentId][subject] = marks;
            }
            else
            {
                if (marks > studentRecords[studentId][subject])
                    studentRecords[studentId][subject] = marks;
            }

            if (!subjectsRecords.ContainsKey(subject))
            {
                subjectsRecords[subject] = new Dictionary<string, int>();
            }

            if (!subjectsRecords[subject].ContainsKey(studentId))
            {
                subjectsRecords[subject][studentId] = marks;
            }
            else
            {
                if (marks > subjectsRecords[subject][studentId])
                    subjectsRecords[subject][studentId] = marks;
            }

            return 1;
        }

        public int RemoveStudent(string studentId)
        {
            if (!studentRecords.ContainsKey(studentId))
                return -1;

            foreach (var subject in studentRecords[studentId].Keys)
            {
                if (subjectsRecords.ContainsKey(subject))
                    subjectsRecords[subject].Remove(studentId);
            }

            studentRecords.Remove(studentId);
            studentInsertionOrder.Remove(studentId);

            return 1;
        }

        public string TopStudent(string subject)
        {
            if (!subjectsRecords.ContainsKey(subject))
                return "No records found.";

            int maxMarks = subjectsRecords[subject].Values.Max();

            List<string> toppers = new List<string>();

            foreach (var student in studentInsertionOrder)
            {
                if (subjectsRecords[subject].ContainsKey(student) &&
                    subjectsRecords[subject][student] == maxMarks)
                {
                    toppers.Add(student + " " + maxMarks);
                }
            }

            return string.Join("\n", toppers);
        }

        public string Result()
        {
            List<string> result = new List<string>();

            foreach (var student in studentInsertionOrder)
            {
                if (!studentRecords.ContainsKey(student))
                    continue;

                double avg = studentRecords[student].Values.Average();
                result.Add(student + " " + avg.ToString("F2"));
            }

            return string.Join("\n", result);
        }
    }

    public static void Main()
    {
        CollageManagement cm = new CollageManagement();

        Console.WriteLine("====== College Management System ======");
        Console.WriteLine("Available Commands:");
        Console.WriteLine("1. ADD <StudentID> <Subject> <Marks>");
        Console.WriteLine("   Example: ADD S1 Math 80");

        Console.WriteLine("2. REMOVE <StudentID>");
        Console.WriteLine("   Example: REMOVE S1");

        Console.WriteLine("3. TOP <Subject>");
        Console.WriteLine("   Example: TOP Math");

        Console.WriteLine("4. RESULT  -> Shows average marks of all students");

        Console.WriteLine("5. EXIT -> To stop the program");
        Console.WriteLine("---------------------------------------");

        while (true)
        {
            Console.Write("\nEnter Command: ");
            string input = Console.ReadLine();

            if (input.ToUpper() == "EXIT")
                break;

            string[] parts = input.Split(' ');

            if (parts[0] == "ADD")
            {
                cm.AddStudent(parts[1], parts[2], int.Parse(parts[3]));
            }
            else if (parts[0] == "REMOVE")
            {
                cm.RemoveStudent(parts[1]);
            }
            else if (parts[0] == "TOP")
            {
                Console.WriteLine(cm.TopStudent(parts[1]));
            }
            else if (parts[0] == "RESULT")
            {
                Console.WriteLine(cm.Result());
            }
            else
            {
                Console.WriteLine("Invalid Command!");
            }
        }
    }
}