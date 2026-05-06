using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RestfulApiVisualCode.EmailClass
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            string emailsender;
            string passwordsender;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "ForEmailSending", "Email.txt");
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Email credentials file was not found.", path);
            }
            using (StreamReader stream = new StreamReader(path))
            {
                emailsender = stream.ReadLine() ?? string.Empty;
                passwordsender = stream.ReadLine() ?? string.Empty;
            }
            if (string.IsNullOrWhiteSpace(emailsender) || string.IsNullOrWhiteSpace(passwordsender))
            {
                throw new InvalidOperationException("Email credentials are not configured.");
            }
            using MailMessage mailMessage = new MailMessage
            {
                Subject = subject,
                Body = message,
                From = new MailAddress(emailsender, "Автоматическое оповещение")
            };
            mailMessage.To.Add(email);

            using SmtpClient smtpClient = new SmtpClient("smtp.yandex.ru", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(emailsender, passwordsender)
            };

            await smtpClient.SendMailAsync(mailMessage);

        }
    }
}
