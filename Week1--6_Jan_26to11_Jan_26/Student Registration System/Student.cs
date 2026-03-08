using System;

namespace StudentRegistrationSystem
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public bool FeesPaid { get; set; }
        public bool DocumentsSubmitted { get; set; }
        public double Attendance { get; set; }

        public Student(int id, string name, bool fees, bool docs, double attendance)
        {
            StudentId = id;
            Name = name;
            FeesPaid = fees;
            DocumentsSubmitted = docs;
            Attendance = attendance;
        }
    }
}
