using System;
using System.Collections.Generic;
using System.IO;

namespace StudentRegistrationSystem
{
    public sealed class Exam
    {
        private const string filePath = "EligibleStudents.txt";

        public bool IsEligible(Student s)
        {
            return s.FeesPaid && s.DocumentsSubmitted && s.Attendance >= 75;
        }

        public void SaveEligibleStudents(List<Student> students)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Student s in students)
                {
                    if (IsEligible(s))
                    {
                        writer.WriteLine(
                            $"ID: {s.StudentId}, Name: {s.Name}, Attendance: {s.Attendance}%");
                    }
                }
            }
        }
    }
}
