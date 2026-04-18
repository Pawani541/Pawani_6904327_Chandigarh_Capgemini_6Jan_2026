namespace BookStore.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendOrderConfirmationAsync(string toEmail, string customerName, int orderId, decimal total);
        Task SendLowStockAlertAsync(string adminEmail, string bookTitle, int remainingStock);
        Task SendWelcomeEmailAsync(string toEmail, string fullName);
    }
}
