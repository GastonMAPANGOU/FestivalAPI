using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;

namespace FestivalAPI.Data
{
    public class SendMail
    {
        private NetworkCredential login;
        private SmtpClient client;
        private MailMessage msg;
        private static readonly int Port = 587;
        private static readonly string Smtp = "smtp.gmail.com";
        private static readonly string Address_Mail = "Festival.ProjetS8.G3@gmail.com";
        private static readonly string Password = "FestivalProjetS8";
        //public string ReturnMessage { get; set; }

        public void ActionSendMail(string SendMailTo, string Subject, string Message)
        {
            login = new NetworkCredential(Address_Mail, Password);
            client = new SmtpClient(Smtp);
            client.Port = Convert.ToInt32(Port);
            client.Credentials = login;
            client.EnableSsl = true;
            msg = new MailMessage
            {
                From = new MailAddress( Address_Mail, "FestiNormandie", Encoding.UTF8)
            };

            msg.To.Add(new MailAddress(SendMailTo));

            msg.Subject = Subject;
            msg.Body = Message;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            //client.SendCompleted += new SendCompletedEventHandler(SendCompleteCallBack);
            string userstate = "Sending ...";
            client.SendAsync(msg, userstate);
        }

        //private void SendCompleteCallBack(object sender, AsyncCompletedEventArgs e)
        //{
        //    if (e.Cancelled)
        //    {
        //        ReturnMessage = string.Format("{0} send canceled.", e.UserState);
        //    }
        //    if (e.Error != null && !e.Cancelled)
        //    {
        //        ReturnMessage = string.Format("{0} {1}", e.UserState, e.Error);
        //    }
        //    else
        //    {
        //        ReturnMessage = "Votre message a été convenablement envoyé";
        //    }
        //}
    }
}
