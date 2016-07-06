using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace KarmicEnergy.Util.Notifications
{
    public class EmailService
    {
        public static void Send(String from, String subject, String body, String destinationEmail)
        {
            if (from == null || from == String.Empty)
                from = ConfigurationManager.AppSettings["EmailService:From"];

            String smtpServer = ConfigurationManager.AppSettings["EmailService:SMTPServer"];
            Int32 smtpPort = Int32.Parse(ConfigurationManager.AppSettings["EmailService:SMTPPort"].ToString());
            String smtpUsername = ConfigurationManager.AppSettings["EmailService:SMTPUsername"];
            String smtpPassword = ConfigurationManager.AppSettings["EmailService:SMTPPassword"];

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(from, "KE Siteminder");
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            MailAddress mailTo = new MailAddress(destinationEmail);
            mailMessage.To.Add(mailTo);

            // Create an SMTP client with the specified host name and port.
            using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
            {
                // Create a network credential with your SMTP user name and password.
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                // Use SSL when accessing Amazon SES. The SMTP session will begin on an unencrypted connection, and then 
                // the client will issue a STARTTLS command to upgrade to an encrypted connection using SSL.
                client.EnableSsl = true;

                // Send the email.                  
                client.Send(mailMessage);
            }
        }
    }
}
