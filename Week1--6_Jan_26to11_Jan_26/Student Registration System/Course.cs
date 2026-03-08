using System;

namespace StudentRegistrationSystem
{
    // Structure to store course data
    struct Course
    {
        public int CourseId;
        public string CourseName;
        public int Duration;     // in months
        public double Fees;
    }

    // Class related to course management
    class CourseManagement
    {
        Course course;   // structure variable

        // Method to take course details
        public void AddCourse()
        {
            Console.Write("Enter Course Id: ");
            course.CourseId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Course Name: ");
            course.CourseName = Console.ReadLine();

            Console.Write("Enter Course Duration (months): ");
            course.Duration = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Course Fees: ");
            course.Fees = Convert.ToDouble(Console.ReadLine());
        }

        // Method to display course details
        public void DisplayCourse()
        {
            Console.WriteLine("\n--- Course Details ---");
            Console.WriteLine("Course Id   : " + course.CourseId);
            Console.WriteLine("Course Name : " + course.CourseName);
            Console.WriteLine("Duration    : " + course.Duration + " months");
            Console.WriteLine("Fees        : " + course.Fees);
        }
    }
}
