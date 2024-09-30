using SendGrid.Helpers.Mail;
using SendGrid;

namespace AutoChronos.API.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
    public class EmailService(IConfiguration configuration) : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var apiKey = configuration["SendGridApiKey"];
            var client = new SendGridClient(apiKey);
            var senderAddress = configuration["EmailSettings:SenderAddress"];
            var from = new EmailAddress(senderAddress, "AutoChronos");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            await client.SendEmailAsync(msg);
        }
    }
}
