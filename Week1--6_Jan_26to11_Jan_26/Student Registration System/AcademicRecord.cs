using System;

namespace StudentRegistrationSystem
{
    // Enum to represent academic status
    public enum AcademicStatus
    {
        Active,
        Completed,
        Discontinued
    }

    // Generic Class: AcademicRecord
    public class AcademicRecord<T>
    {
        // Generic variable (nullable safe)
        public T? RecordData;

        // Enum type variable
        public AcademicStatus Status;

        // Method to set record data and academic status
        public void SetRecord(T data, AcademicStatus status)
        {
            RecordData = data;   // Assign generic data
            Status = status;     // Assign enum value
        }

        // Method to display record
        public void ShowRecord()
        {
            Console.WriteLine("Record Data      : " + RecordData);
            Console.WriteLine("Academic Status  : " + Status);
        }
    }
}
