using System.Net.Mail;
using System.Net;

namespace CRM.Email
{
    public class EmailService
    {
        public static void SendEmail(string email,string subject, string body)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("sugra.alhuseynova@gmail.com");
            message.To.Add(new MailAddress(email));
            message.Subject = subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = $"click the link: <a href={body}>Click the link below</a>";
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("sugra.alhuseynova@gmail.com", "amckoneoyjzelwsr");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                smtp.Send(message);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
