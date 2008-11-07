using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;
using System.Net.Mail;
using System.Reflection;
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
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Send(mail);
        }

        private static string GetBody(DTCommandInfo command)
        {
            string body = "<h2>Detalle de la Tarea</h2><hr />";
            DTItem item = command.Item;
            foreach (DTField field in item.Fields)
            {
                string name = field.Name;
                string fieldValue = GetFieldValue(field);

                body += String.Format("<div style= \"width:10%;float:left\"><b>{0}:</b></div><div style=\"width:20%;float:left\">{1}</div><br/>", name, fieldValue);
            }

            return body;
        }

        private static string GetFieldValue(DTField field)
        {
            MethodInfo[] methods = field.GetType().GetMethods();

            MethodInfo method = null;
            foreach (MethodInfo info in methods)
            {
                if (info.Name == "get_Value")
                {
                    method = info;
                    break;
                }
            }

            return Convert.ToString(method.Invoke(field, null));
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
                    subject = "Modificación de ";
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














