using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreBookDemo.Models
{
    [Table("tblBooks")]
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }

        public string BookName { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
    }
}