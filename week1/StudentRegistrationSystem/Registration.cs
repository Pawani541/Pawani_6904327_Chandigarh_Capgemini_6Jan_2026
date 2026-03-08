using System;

namespace StudentRegistrationSystem
{
    internal class Registration
    {
        // Instance variables (per student)
        public int StudentId;
        public string Name = string.Empty;     // initialized to avoid null warning
        public int RollNo;
        public string Course = string.Empty;   // initialized to avoid null warning
        public int Age;

        // Static variable (shared across all students)
        public static int totalStudents = 0;

        // Constant variable
        public const int MaxMarks = 100;

        // Readonly variable (assigned once via constructor)
        public readonly int AdmissionNo;

        // Constructor
        public Registration(int admissionNo)
        {
            AdmissionNo = admissionNo;
            totalStudents++;
        }

        // Method to take input from user
        public void InputDetails()
        {
            Console.Write("Enter Student ID: ");
            StudentId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Name: ");
            Name = Console.ReadLine() ?? "";

            Console.Write("Enter Roll Number: ");
            RollNo = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Course: ");
            Course = Console.ReadLine() ?? "";

            Console.Write("Enter Age: ");
            Age = Convert.ToInt32(Console.ReadLine());
        }

        // Method to display student details
        public void Display()
        {
            Console.WriteLine("\n--- Student Registration Details ---");
            Console.WriteLine($"Student ID   : {StudentId}");
            Console.WriteLine($"Name         : {Name}");
            Console.WriteLine($"Roll No      : {RollNo}");
            Console.WriteLine($"Course       : {Course}");
            Console.WriteLine($"Age          : {Age}");
            Console.WriteLine($"Admission No : {AdmissionNo}");
            Console.WriteLine($"Max Marks    : {MaxMarks}");
        }
    }
}
