using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace LibraryDisconnected
{
    class Program
    {
        static string connectionString =
            "Data Source=LAPTOP-MMKTQLIJ\\SQLEXPRESS;Initial Catalog=LibraryDB;Integrated Security=True;TrustServerCertificate=True;";

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n===== Library Management (Disconnected) =====");
                Console.WriteLine("1. View Books");
                Console.WriteLine("2. Add Book");
                Console.WriteLine("3. Update Book");
                Console.WriteLine("4. Delete Book");
                Console.WriteLine("5. Exit");
                Console.Write("Enter choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                switch (choice)
                {
                    case 1: GetBooks(); break;
                    case 2: AddBook(); break;
                    case 3: UpdateBook(); break;
                    case 4: DeleteBook(); break;
                    case 5: return;
                    default: Console.WriteLine("Invalid choice!"); break;
                }
            }
        }

        static void GetBooks()
        {
            using SqlConnection con = new SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter("sp_GetBooks", con);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, "Books");

                DataTable table = ds.Tables["Books"];

                Console.WriteLine("\n--- Book List ---");

                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine($"ID: {row["BookId"]} | Title: {row["Title"]} | AuthorId: {row["AuthorId"]} | Year: {row["PublishedYear"]}");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database Error:");
                Console.WriteLine(ex.Message);
            }
        }

        static void AddBook()
        {
            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("AuthorId: ");
            int.TryParse(Console.ReadLine(), out int authorId);

            Console.Write("Published Year: ");
            int.TryParse(Console.ReadLine(), out int year);

            using SqlConnection con = new SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = new SqlCommand("sp_AddBook", con);
            adapter.InsertCommand.CommandType = CommandType.StoredProcedure;

            adapter.InsertCommand.Parameters.AddWithValue("@Title", title);
            adapter.InsertCommand.Parameters.AddWithValue("@AuthorId", authorId);
            adapter.InsertCommand.Parameters.AddWithValue("@PublishedYear", year);

            try
            {
                con.Open();
                adapter.InsertCommand.ExecuteNonQuery();
                Console.WriteLine("Book Added Successfully!");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void UpdateBook()
        {
            Console.Write("BookId: ");
            int.TryParse(Console.ReadLine(), out int bookId);

            Console.Write("New Title: ");
            string title = Console.ReadLine();

            Console.Write("New AuthorId: ");
            int.TryParse(Console.ReadLine(), out int authorId);

            Console.Write("New Year: ");
            int.TryParse(Console.ReadLine(), out int year);

            using SqlConnection con = new SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = new SqlCommand("sp_UpdateBook", con);
            adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;

            adapter.UpdateCommand.Parameters.AddWithValue("@BookId", bookId);
            adapter.UpdateCommand.Parameters.AddWithValue("@Title", title);
            adapter.UpdateCommand.Parameters.AddWithValue("@AuthorId", authorId);
            adapter.UpdateCommand.Parameters.AddWithValue("@PublishedYear", year);

            try
            {
                con.Open();
                int rows = adapter.UpdateCommand.ExecuteNonQuery();

                if (rows > 0)
                    Console.WriteLine("Book Updated Successfully!");
                else
                    Console.WriteLine("Book not found.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void DeleteBook()
        {
            Console.Write("BookId: ");
            int.TryParse(Console.ReadLine(), out int bookId);

            using SqlConnection con = new SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.DeleteCommand = new SqlCommand("sp_DeleteBook", con);
            adapter.DeleteCommand.CommandType = CommandType.StoredProcedure;

            adapter.DeleteCommand.Parameters.AddWithValue("@BookId", bookId);

            try
            {
                con.Open();
                int rows = adapter.DeleteCommand.ExecuteNonQuery();

                if (rows > 0)
                    Console.WriteLine("Book Deleted Successfully!");
                else
                    Console.WriteLine("Book not found.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

