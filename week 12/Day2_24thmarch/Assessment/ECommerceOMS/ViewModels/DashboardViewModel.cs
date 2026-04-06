using ECommerceOMS.Models;

namespace ECommerceOMS.ViewModels
{
    public class TopProduct
    {
        public string ProductName { get; set; }
        public int TotalSold { get; set; }
        public decimal Revenue { get; set; }
    }

    public class CustomerSummary
    {
        public Customer Customer { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalSpent { get; set; }
    }

    public class DashboardViewModel
    {
        public List<TopProduct> TopProducts { get; set; }
        public List<Order> PendingOrders { get; set; }
        public List<CustomerSummary> CustomerSummaries { get; set; }
    }
}