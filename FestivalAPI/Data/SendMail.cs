using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;

namespace FestivalAPI.Data
{
    public class SendMail
    {
        NetworkCredential login;
        SmtpClient client;
        MailMessage msg;
        int Port = 587;
        public void ActionSendMail(string Smtp, string Username, string Password, string To, string CC, string Subject, string Message, string Address_Mail)
        {
            login = new NetworkCredential(Username, Password);
            client = new SmtpClient(Smtp);
            client.Port = Convert.ToInt32(Port);
            client.Credentials = login;
            msg = new MailMessage
            {
                From = new MailAddress( Address_Mail, "Lucy", Encoding.UTF8)
            };

            msg.To.Add(new MailAddress(To));

            if (!string.IsNullOrEmpty(CC))
            {
                msg.To.Add(new MailAddress(CC));
            }

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

        /*public void SendCompleteCallBack(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show(string.Format("{0} send canceled.", e.UserState), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (e.Error != null)
                MessageBox.Show(string.Format("{0} {1}", e.UserState, e.Error), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(string.Format("Your message has been successfully sent"), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }*/
    }
}
