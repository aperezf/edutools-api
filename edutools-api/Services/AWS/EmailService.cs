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
        private readonly string SMTPHost;
        private readonly string SMTPUsername;
        private readonly string SMTPPassword;
        private readonly int SMTPPort;
        private readonly SmtpClient _SmtpClient;

        public EmailService(IConfiguration config)
        {
            _Configuration = config;
            var smtpSection = _Configuration.GetSection("SmtpCredentials");
            SMTPHost = smtpSection.GetValue<string>("Host");
            SMTPUsername = smtpSection.GetValue<string>("Username");
            SMTPPassword = smtpSection.GetValue<string>("Password");
            SMTPPort = smtpSection.GetValue<int>("Port");
            _SmtpClient = new SmtpClient(SMTPHost, SMTPPort);
            _SmtpClient.UseDefaultCredentials = false;
            _SmtpClient.Credentials = new NetworkCredential(SMTPUsername, SMTPPassword);
            _SmtpClient.EnableSsl = true;

        }

        public async Task<bool> SendEmail(string to, string body)
        {
            MailAddress from = new MailAddress("aperezf91@gmail.com", "Hola1", Encoding.UTF8);
            MailAddress toA = new MailAddress(to, to, Encoding.UTF8);
            MailMessage message = new MailMessage(from, toA);
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = "Hola2";
            message.SubjectEncoding = Encoding.UTF8;

            await _SmtpClient.SendMailAsync(message);
            return true;

        }
    }
}
