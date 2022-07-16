using FirebaseAdmin.Messaging;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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

        public class Notification
        {
            private static string serverKey = "AAAAix6f9Jk:APA91bHaTWQ9lm4m_y3QYvgxoJ_sjo9fI9CopP9UeRm_ua_iVm-NTQosdVvutN47uk_DOyiAmL2mU1Ly8TnMNzAdbvUSuXng7OdpPRv0YnR50pz9ZfNcB5zxdztAKArfQDXnBm8zhKgL";
            private static string senderId = "597514253465";
            private static string webAddr = "https://fcm.googleapis.com/fcm/send";
            public static void NotificationP(string deviceId)
            {
                try
                {
                    var applicationID = "AIza---------4GcVJj4dI";

                    var senderId = "57-------55";

                     WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");

                    tRequest.Method = "post";

                    tRequest.ContentType = "application/json";

                    var data = new

                    {

                        to = deviceId,

                        notification = new

                        {

                            body = "hola",

                            title = "adios",

                            icon = "myicon"

                        }
                    };

                    var serializer = new JavaScriptSerializer();

                    var json = serializer.Serialize(data);

                    Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                    tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));

                    tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));

                    tRequest.ContentLength = byteArray.Length;


                    using (Stream dataStream = tRequest.GetRequestStream())
                    {

                        dataStream.Write(byteArray, 0, byteArray.Length);


                        using (WebResponse tResponse = tRequest.GetResponse())
                        {

                            using (Stream dataStreamResponse = tResponse.GetResponseStream())
                            {

                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {

                                    String sResponseFromServer = tReader.ReadToEnd();

                                    string str = sResponseFromServer;

                                }
                            }
                        }
                    }
                }

                catch (Exception ex)
                {

                    string str = ex.Message;

                }
            }
                public static string SendNotification(string DeviceToken, string title, string msg)
            {
                var result = "-1";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                httpWebRequest.Method = "POST";

                var payload = new
                {
                    to = DeviceToken,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        
                        body = msg,
                        title = title
                    },
                };
                var serializer = new JavaScriptSerializer();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = serializer.Serialize(payload);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
        }


    }
}
