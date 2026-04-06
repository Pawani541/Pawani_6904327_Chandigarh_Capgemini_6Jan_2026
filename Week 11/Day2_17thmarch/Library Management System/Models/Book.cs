using System.ComponentModel.DataAnnotations;

namespace LibraryManagementMVC.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public int PublishedYear { get; set; }

        public string Genre { get; set; }
    }
}