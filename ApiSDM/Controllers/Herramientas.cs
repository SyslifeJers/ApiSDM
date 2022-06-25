using System.Net;
using System.Net.Mail;

namespace ApiSDM.Controllers
{
    public class Herramientas
    {
            public static void Correo(string EmailDestino, string asunto, string Body)
            {

                string EmailOrigen = "noresponder@app-sabordemexico.com";
                string pass = "Rtx2080_";
                MailMessage mailMessage = new MailMessage(EmailOrigen, EmailDestino, asunto, Body);
                mailMessage.IsBodyHtml = false;


                SmtpClient smtp = new SmtpClient("mail.app-sabordemexico.com");

                NetworkCredential Credentials = new NetworkCredential(EmailOrigen, pass);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Port = 8889;    //alternative port number is 8889
                smtp.EnableSsl = false;
                smtp.Send(mailMessage);

                //SmtpClient smtpClient = new SmtpClient("mail5005.site4now.net");
                //smtpClient.EnableSsl = false;
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.Port = 465;
                //smtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, pass);
                //smtpClient.Send(mailMessage);
                //smtpClient.Dispose();

            }

      
    }
}
