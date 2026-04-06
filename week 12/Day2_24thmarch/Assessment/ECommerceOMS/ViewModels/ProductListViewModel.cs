using ECommerceOMS.Models;

namespace ECommerceOMS.ViewModels
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string SearchTerm { get; set; }
    }
}