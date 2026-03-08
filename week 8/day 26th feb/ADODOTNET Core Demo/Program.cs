using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ADODOTNETCoreDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=LAPTOP-MMKTQLIJ\\SQLEXPRESS;Database=EmployeeDB;Integrated Security=True;TrustServerCertificate=True;";

            try
            {
                Console.WriteLine("DB Program Started");

                GetAllEmployees(connectionString);

                GetEmployeeByID(connectionString, 2);

                CreateEmployeeWithAddress(connectionString,
                    "Ramesh", "Sharma", "ramesh@gmail.com",
                    "MG Road", "Bangalore", "Karnataka", "560001");

                UpdateEmployeeWithAddress(connectionString,
                    2, "Jane", "Doe", "janeupdated@example.com",
                    "MG Road", "Bangalore", "Karnataka", "560001", 2);

                DeleteEmployee(connectionString, 5);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }

        static void GetAllEmployees(string connectionString)
        {
            Console.WriteLine("\nGetAllEmployees Called");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("GetAllEmployees", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    Console.WriteLine("No data found");
                }

                while (reader.Read())
                {
                    Console.WriteLine($"Employee: {reader["EmployeeID"]}, {reader["FirstName"]}, {reader["LastName"]}, {reader["Email"]}");
                    Console.WriteLine($"Address: {reader["Street"]}, {reader["City"]}, {reader["State"]}, {reader["PostalCode"]}\n");
                }

                reader.Close();
            }
        }

        static void GetEmployeeByID(string connectionString, int employeeID)
        {
            Console.WriteLine("\nGetEmployeeByID Called");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("GetEmployeeByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeID", employeeID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    Console.WriteLine("Employee not found");
                }

                while (reader.Read())
                {
                    Console.WriteLine($"Employee: {reader["FirstName"]} {reader["LastName"]}, Email: {reader["Email"]}");
                    Console.WriteLine($"Address: {reader["Street"]}, {reader["City"]}, {reader["State"]}, {reader["PostalCode"]}");
                }

                reader.Close();
            }
        }

        static void CreateEmployeeWithAddress(string connectionString,
            string firstName, string lastName, string email,
            string street, string city, string state, string postalCode)
        {
            Console.WriteLine("\nCreateEmployeeWithAddress Called");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("CreateEmployeeWithAddress", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Street", street);
                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@State", state);
                command.Parameters.AddWithValue("@PostalCode", postalCode);

                connection.Open();
                command.ExecuteNonQuery();

                Console.WriteLine("Employee created successfully");
            }
        }

        static void UpdateEmployeeWithAddress(string connectionString, int employeeID,
            string firstName, string lastName, string email,
            string street, string city, string state,
            string postalCode, int addressID)
        {
            Console.WriteLine("\nUpdateEmployeeWithAddress Called");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("UpdateEmployeeWithAddress", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmployeeID", employeeID);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Street", street);
                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@State", state);
                command.Parameters.AddWithValue("@PostalCode", postalCode);
                command.Parameters.AddWithValue("@AddressID", addressID);

                connection.Open();
                command.ExecuteNonQuery();

                Console.WriteLine("Employee updated successfully");
            }
        }

        static void DeleteEmployee(string connectionString, int employeeID)
        {
            Console.WriteLine("\nDeleteEmployee Called");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DeleteEmployee", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeID", employeeID);

                connection.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0)
                    Console.WriteLine("Employee deleted successfully");
                else
                    Console.WriteLine("Employee not found");
            }
        }
    }
}