using System.Net;
using System.Net.Mail;

namespace authentication.domain.Services
{
    public static class MailService
    {
        public static void Email(string to, string subject, string htmlString, bool isBodyHtml = false)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("noreplay@email.com");
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.IsBodyHtml = isBodyHtml;
            message.Body = htmlString;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("FromMailAddress", "password");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.SendAsync(message, null);
        }
    }
}
