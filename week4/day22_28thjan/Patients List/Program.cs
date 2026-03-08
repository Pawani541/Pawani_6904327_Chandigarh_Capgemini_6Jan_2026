using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Patient> patientList = new List<Patient>();

        Console.WriteLine("Enter the number of patients");
        int n = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= n; i++)
        {
            Console.WriteLine("Enter patient " + i + " details:");

            Console.WriteLine("Enter the name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the age");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the illness");
            string illness = Console.ReadLine();

            Console.WriteLine("Enter the city");
            string city = Console.ReadLine();

            patientList.Add(new Patient(name, age, illness, city));
        }

        PatientBO bo = new PatientBO();
        string choice;

        do
        {
            Console.WriteLine("Enter your choice:");
            Console.WriteLine("1)Display Patient Details");
            Console.WriteLine("2)Display Youngest Patient Details");
            Console.WriteLine("3)Display Patients from City");

            int ch = Convert.ToInt32(Console.ReadLine());

            switch (ch)
            {
                case 1:
                    Console.WriteLine("Enter patient name:");
                    string pname = Console.ReadLine();
                    bo.DisplayPatientDetails(patientList, pname);
                    break;

                case 2:
                    bo.DisplayYoungestPatientDetails(patientList);
                    break;

                case 3:
                    Console.WriteLine("Enter city");
                    string cityName = Console.ReadLine();
                    bo.DisplayPatientsFromCity(patientList, cityName);
                    break;
            }

            Console.WriteLine("Do you want to continue(Yes/No)?");
            choice = Console.ReadLine();

        } while (choice.Equals("Yes"));
    }
}
