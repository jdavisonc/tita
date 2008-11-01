using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;
using System.Net.Mail;
namespace Infocorp.TITA.WITLogic
{
    class EMailSender
    {

        public static void SendNotification(DTItem task, ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.ISSUE:
                    break;
                case ItemType.TASK:
                    break;
                case ItemType.WORKPACKAGE:
                    break;
                default:
                    break;
            }
        }

        public static void SendTaskNotification(DTItem task)
        {
            string address = string.Empty;

            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(address));
            mail.Subject = "algo";
            mail.Body = "body";

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Send(mail);
        }

    }
}














