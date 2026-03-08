using System;

namespace StudentRegistrationSystem
{
    internal class SeniorStudent : Registration
    {
        public int PassingYear;
        public string Responsibility;

        public SeniorStudent(int admissionNo, int passingYear, string responsibility)
            : base(admissionNo) 
        {
            PassingYear = passingYear;
            Responsibility = responsibility;
        }

        public void DisplaySenior()
        {
            Display(); // parent class display
            Console.WriteLine($"Passing Year : {PassingYear}");
            Console.WriteLine($"Responsibility : {Responsibility}");
        }
    }
}
