using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace StudentManagement
{
    internal class Program
    {
        static string connectionString =
            "Data Source=LAPTOP-MMKTQLIJ\\SQLEXPRESS;Initial Catalog=UniversityDB;Integrated Security=True;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n===== University Management System =====");
                Console.WriteLine("1. Insert Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. View Student By ID");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                switch (choice)
                {
                    case 1: InsertStudent(); break;
                    case 2: GetAllStudents(); break;
                    case 3: GetStudentById(); break;
                    case 4: UpdateStudent(); break;
                    case 5: DeleteStudent(); break;
                    case 6: return;
                    default: Console.WriteLine("Invalid Choice!"); break;
                }
            }
        }
        static void InsertStudent()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Department Id: ");

            if (!int.TryParse(Console.ReadLine(), out int deptId))
            {
                Console.WriteLine("Invalid DeptId!");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_AddStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@DeptId", deptId);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Student Inserted Successfully!");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Database Error:");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static void GetAllStudents()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_GetStudents", con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine("\n--- Student List ---");

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No students found.");
                        return;
                    }

                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $"ID: {reader["StudentId"]} | " +
                            $"Name: {reader["FirstName"]} {reader["LastName"]} | " +
                            $"Email: {reader["Email"]} | " +
                            $"DeptId: {reader["DeptId"]}");
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Database Error:");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static void GetStudentById()
        {
            Console.Write("Enter Student Id: ");

            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid StudentId!");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_GetStudentById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Console.WriteLine(
                            $"Name: {reader["FirstName"]} {reader["LastName"]} | " +
                            $"Email: {reader["Email"]} | " +
                            $"DeptId: {reader["DeptId"]}");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Database Error:");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static void UpdateStudent()
        {
            Console.Write("Enter Student Id to Update: ");

            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid StudentId!");
                return;
            }

            Console.Write("Enter New First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter New Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter New Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter New Department Id: ");

            if (!int.TryParse(Console.ReadLine(), out int deptId))
            {
                Console.WriteLine("Invalid DeptId!");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_UpdateStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@DeptId", deptId);

                try
                {
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                        Console.WriteLine("Student Updated Successfully!");
                    else
                        Console.WriteLine("Student Not Found!");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Database Error:");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static void DeleteStudent()
        {
            Console.Write("Enter Student Id to Delete: ");

            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid StudentId!");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_DeleteStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                try
                {
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                        Console.WriteLine("Student Deleted Successfully!");
                    else
                        Console.WriteLine("Student Not Found!");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Database Error:");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
