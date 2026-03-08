using System;

public class StudentValidator
{
    public void ValidateStudent(int studentId)
    {
        try
        {
            // Rule: Student ID positive   
            if (studentId <= 0)
            {
                throw new Exception("Invalid Student ID");
            }

            Console.WriteLine("Student ID is VALID ");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Student ID is INVALID ");
            Console.WriteLine("Reason: " + ex.Message);
        }
    }
}
