using System.Net.Mail;
using System.Net;

namespace FitVision.Helpers
{
    public class EmailSender
    {
        public static void Posalji(string to, string messageSubject, string messageBody, bool isBodyHtml = false)
        {
            if (to == "")
                return;
            String SendMailFrom = "fit.korisnik@gmail.com";

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage email = new MailMessage();
            // START
            email.From = new MailAddress(SendMailFrom);
            email.To.Add(to);
            email.CC.Add(SendMailFrom);
            email.Subject = messageSubject;
            email.Body = messageBody;
            email.IsBodyHtml = isBodyHtml;
            //END
            SmtpServer.Timeout = 5000;
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential(SendMailFrom, "ysvabvgbfxhsmldu");
            SmtpServer.Send(email);
        }
    }
}
