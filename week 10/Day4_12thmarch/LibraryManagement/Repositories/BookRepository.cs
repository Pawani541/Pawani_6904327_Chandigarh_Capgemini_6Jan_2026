using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public class BookRepository : IBookRepository
    {
        private static List<Book> books = new List<Book>()
        {
            new Book { Id = 1, Title = "C# Basics", Author = "John" },
            new Book { Id = 2, Title = "ASP.NET Core", Author = "David" }
        };

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public Book GetBookById(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            var existing = books.FirstOrDefault(b => b.Id == book.Id);

            if (existing != null)
            {
                existing.Title = book.Title;
                existing.Author = book.Author;
            }
        }

        public void DeleteBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                books.Remove(book);
            }
        }
    }
}