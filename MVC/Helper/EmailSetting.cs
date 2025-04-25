using System.Net;
using System.Net.Mail;

namespace MVC.Helper
{
    //Helper Static Class
    //Here the procedure of sending the email
    public static class EmailSetting
    {
        public static bool SendEmail(Email email)
        {
            //Mail Server : Gmail
            //Protocol : Simple Mail Transfer Protocol  {SMTP}

            //Need two things 
            //1)Host  : smtp.gmail.com
            //2)Port  : 587
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                //Sender Data 
                client.Credentials = new NetworkCredential("a.yousef917.ya@gmail.com", "xjpsxddlmeauucba");
                client.Send(from: "a.yousef917.ya@gmail.com", 
                    recipients: email.To, 
                    subject: email.Subject, 
                    body: email.Body);

                return true;
            }
            catch(Exception ex)  
            {
            return false;

            }

        }
    }
}
