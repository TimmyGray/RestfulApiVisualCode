using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;
using System.Threading.Tasks;
using System.IO;
namespace RestfulApiVisualCode.EmailClass
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            string emailsender;
            string passwordsender;
            string path = $"{Directory.GetCurrentDirectory()}\\ForEmailSending\\Email.txt";
            using (StreamReader stream = new StreamReader(path))
            {
                emailsender = stream.ReadLine();
                passwordsender = stream.ReadLine();
            }
            MimeMessage emailmessage = new MimeMessage();
            emailmessage.Subject = subject;
            emailmessage.From.Add(new MailboxAddress("Автоматическое оповещение", emailsender));
            emailmessage.To.Add(new MailboxAddress("",email));
            emailmessage.Body = new TextPart(TextFormat.Text)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 25, false);
                await client.AuthenticateAsync(emailsender, passwordsender);
                await client.SendAsync(emailmessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
