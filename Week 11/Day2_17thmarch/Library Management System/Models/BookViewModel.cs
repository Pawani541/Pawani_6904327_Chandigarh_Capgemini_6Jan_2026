namespace LibraryManagementMVC.Models
{
    public class BookViewModel
    {
        public Book Book { get; set; }

        public bool IsAvailable { get; set; }

        // Practice Extension
        public string BorrowerName { get; set; }
    }
}