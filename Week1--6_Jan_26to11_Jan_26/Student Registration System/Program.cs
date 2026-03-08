using System;
using System.Collections.Generic;

namespace StudentRegistrationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== STUDENT REGISTRATION SYSTEM =====\n");

            // =========================
            // 1. REGISTRATION
            // =========================
            Console.Write("Enter Admission Number: ");
            int admissionNo = Convert.ToInt32(Console.ReadLine());

            Registration reg = new Registration(admissionNo);
            reg.InputDetails();
            reg.Display();

            // =========================
            // 2. COURSE DETAILS
            // =========================
            Course course = new Course();

            Console.Write("\nEnter Course Name: ");
            course.CourseName = Console.ReadLine() ?? "";

            Console.Write("Enter Course Duration (Semesters): ");
            course.Duration = Convert.ToInt32(Console.ReadLine());

            

            // =========================
            // 3. FEES DETAILS
            // =========================
            Fees fees = new Fees();

            Console.Write("\nEnter Fee per Semester: ");
            double feePerSem = Convert.ToDouble(Console.ReadLine());

            Console.Write("How many semesters fees PAID?: ");
            int paidSem = Convert.ToInt32(Console.ReadLine());

            fees.SetFees(feePerSem, 0, 0, 0, 0);

            Console.WriteLine("\n--- FEES STATUS ---");
            Console.WriteLine($"Fee per Semester : {feePerSem}");
            Console.WriteLine($"Paid Semesters   : {paidSem}");

            // =========================
            // 4. MARKS (PASS / FAIL)
            // =========================
            Console.WriteLine("\n--- MARKS DETAILS ---");

            Console.Write("Enter Subject 1 Marks: ");
            int m1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Subject 2 Marks: ");
            int m2 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Subject 3 Marks: ");
            int m3 = Convert.ToInt32(Console.ReadLine());

            Marks marks = new Marks(m1, m2, m3);

            double percentage = marks.Percentage();
            Console.WriteLine($"Percentage: {percentage}%");

            bool isPass = percentage >= 40;

            Console.WriteLine(isPass
                ? "Result: PASS "
                : "Result: FAIL ");

            // =========================
            // 5. GENERIC + ENUM
            // =========================
            AcademicRecord<string> record = new AcademicRecord<string>();

            record.SetRecord(
                course.CourseName,
                isPass ? AcademicStatus.Active : AcademicStatus.Discontinued
            );

            Console.WriteLine("\n--- ACADEMIC RECORD ---");
            record.ShowRecord();

            // =========================
            // 6. STUDENT VALIDATION
            // =========================
            StudentValidator validator = new StudentValidator();
            validator.ValidateStudent(reg.StudentId);

            // =========================
            // 7. EXAM ELIGIBILITY (SEALED CLASS)
            // =========================
            Student student = new Student(
                reg.StudentId,
                reg.Name,
                paidSem > 0,
                true,
                75   // assume attendance
            );

            Exam exam = new Exam();

            Console.WriteLine("\n--- EXAM ELIGIBILITY ---");

            for (int sem = 1; sem <= course.Duration; sem++)
            {
                if (sem <= paidSem)
                    Console.WriteLine($"Eligible for Semester {sem} Exam ");
                else
                    Console.WriteLine($"Not Eligible for Semester {sem} Exam  (Fees not paid)");
            }

            Console.WriteLine("\n===== ALL THE BEST =====");
            Console.ReadLine();
        }
    }
}
