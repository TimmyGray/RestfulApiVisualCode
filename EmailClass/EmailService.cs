using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Net.Proxy;
using MimeKit.Text;
using System.Threading.Tasks;
using System.IO;
using System;

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
            emailmessage.To.Add(new MailboxAddress("", email));
            emailmessage.Body = new TextPart(TextFormat.Text)
            {
                Text = message
            };

            string proxyip = null;
            int proxyport = 0;
            string pathproxy = $"{Directory.GetCurrentDirectory()}\\ForEmailSending\\Proxy.txt";

            using (StreamReader stream = new StreamReader(pathproxy))
            {
                int enableproxy;
                int.TryParse(stream.ReadLine(), out enableproxy);
                if (enableproxy == 1)
                {
                    proxyip = stream.ReadLine();
                    proxyport = Convert.ToInt32(stream.ReadLine());

                }
            }

            using (var client = new SmtpClient())
            {
                
                if (proxyip != null)
                {
                    try
                    {
                        client.ProxyClient = new HttpsProxyClient(proxyip, proxyport);

                    }
                    catch (Exception)
                    {

                        Console.WriteLine("Invalid proxy");
                        return;
                    }

                }
                await client.ConnectAsync("smtp.yandex.ru", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(emailsender, passwordsender);
                await client.SendAsync(emailmessage);
                await client.DisconnectAsync(true);
            }

        }
    }
}
