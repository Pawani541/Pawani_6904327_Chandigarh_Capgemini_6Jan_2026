using BookStore.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace BookStore.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailSettings = _config.GetSection("EmailSettings");

            var senderEmail = emailSettings["SenderEmail"] ?? string.Empty;
            var senderName = emailSettings["SenderName"] ?? "BookStore Pro";
            var smtpHost = emailSettings["SmtpHost"] ?? "smtp.gmail.com";
            var smtpPort = int.Parse(emailSettings["SmtpPort"] ?? "587");
            var senderPassword = emailSettings["SenderPassword"] ?? string.Empty;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.To.Add(new MailboxAddress(string.Empty, toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(senderEmail, senderPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task SendOrderConfirmationAsync(
            string toEmail, string customerName, int orderId, decimal total)
        {
            var subject = $"Order Confirmation - Order #{orderId}";
            var body = $@"
                <h2>Dear {customerName},</h2>
                <p>Your order <strong>#{orderId}</strong> has been placed successfully.</p>
                <p>Total Amount: <strong>Rs. {total}</strong></p>
                <p>Thank you for shopping with BookStore Pro!</p>";

            await SendEmailAsync(toEmail, subject, body);
        }

        public async Task SendLowStockAlertAsync(
            string adminEmail, string bookTitle, int remainingStock)
        {
            var subject = $"Low Stock Alert: {bookTitle}";
            var body = $@"
                <h2>Low Stock Alert</h2>
                <p>Book <strong>{bookTitle}</strong> is running low on stock.</p>
                <p>Remaining Stock: <strong>{remainingStock}</strong></p>
                <p>Please restock immediately.</p>";

            await SendEmailAsync(adminEmail, subject, body);
        }

        public async Task SendWelcomeEmailAsync(string toEmail, string fullName)
        {
            var subject = "Welcome to BookStore Pro!";
            var body = $@"
                <h2>Welcome, {fullName}!</h2>
                <p>Your account has been created successfully.</p>
                <p>Start exploring thousands of books at BookStore Pro.</p>
                <p>Happy Reading!</p>";

            await SendEmailAsync(toEmail, subject, body);
        }
    }
}
