using System;

class HospitalPerson  
{
    public string Name { get; set; }
    public int Id { get; set; }
}

class Patient : HospitalPerson
{
    public string Disease { get; set; }
}

class Doctor : HospitalPerson
{
    public string Specialization { get; set; }
}

class Nurse : HospitalPerson
{
    public string Shift { get; set; }
}

class Appointment
{
    public Patient Patient;
    public Doctor Doctor;
    public DateTime Date;

    public void ShowAppointment()
    {
        Console.WriteLine("Patient: " + Patient.Name);
        Console.WriteLine("Doctor: " + Doctor.Name);
        Console.WriteLine("Date: " + Date.ToShortDateString());
    }
}

class Program
{
    static void Main()
    {
        Patient p = new Patient
        {
            Name = "Aarav",
            Id = 1,
            Disease = "Fever"
        };

        Doctor d = new Doctor
        {
            Name = "Dr. Sharma",
            Id = 101,
            Specialization = "Physician"
        };

        Appointment a = new Appointment
        {
            Patient = p,
            Doctor = d,
            Date = DateTime.Now
        };

        a.ShowAppointment();
    }
}
