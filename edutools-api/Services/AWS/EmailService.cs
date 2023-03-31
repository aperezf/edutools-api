using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace edutools_api.Services.AWS
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _Configuration;

        public EmailService(IConfiguration config)
        {
            _Configuration = config;
        }

        public async Task<bool> SendEmail(string to, string body)
        {
            string host = "email-smtp.us-east-1.amazonaws.com";
            int port = 587;
            string smtpUser = "";
            string smtpPassword = "";
            SmtpClient client = new SmtpClient(host, port);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(smtpUser, smtpPassword);
            client.EnableSsl = true;
            MailAddress from = new MailAddress("aperezf91@gmail.com", "Hola1", Encoding.UTF8);
            MailAddress toA = new MailAddress(to, to, Encoding.UTF8);
            MailMessage message = new MailMessage(from, toA);
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = "Hola2";
            message.SubjectEncoding = Encoding.UTF8;

            await client.SendMailAsync(message);
            return true;

        }
    }
}
