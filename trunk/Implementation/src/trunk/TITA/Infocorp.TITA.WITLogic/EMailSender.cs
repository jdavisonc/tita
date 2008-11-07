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

        public static void SendTaskNotification(DTCommandInfo command, string email)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(email));
            mail.Subject = GetSubject(command.CommandItemType, command.CommandType);
            mail.Body = GetBody(command);

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Send(mail);
        }

        private static string GetBody(DTCommandInfo command)
        {
            return "Aca va el body";
        }

        private static string GetSubject(ItemType itemType, CommandType commandType)
        {
            string subject = string.Empty;
            switch (commandType)
            {
                case CommandType.ADD:
                    subject = "Alta de ";
                    break;
                case CommandType.MODIFY:
                    subject = "Mofificación de ";
                    break;
                case CommandType.DELETE:
                    subject = "Eliminación de ";
                    break;
                default:
                    break;
            }

            switch (itemType)
            {
                case ItemType.ISSUE:
                    subject += "incidente";
                    break;
                case ItemType.TASK:
                    subject += "tarea";
                    break;
                case ItemType.WORKPACKAGE:
                    subject += "workpackage";
                    break;
                default:
                    break;
            }


            return subject;
        }

    }
}














