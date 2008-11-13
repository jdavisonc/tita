using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net.Mail;
using System.Net;
using Infocorp.TITA.DataTypes;
using System.Reflection;

namespace Infocorp.TITA.WITLogic.Tests
{
    [TestFixture]
    public class EMailSenderTests
    {
        [Test]
        public void SendMail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress("gschnyder@gmail.com"));
                mail.Subject = "algo";
                mail.Body = "body";

                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential("grupopis08@gmail.com", "grupopis2008");
                client.EnableSsl = true;
                client.Send(mail);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void GetValue()
        {
            DTItem item = new DTItem();
            item.Fields = new List<DTField>();
            item.Fields.Add(new DTFieldAtomicString("name", "internal", true, false, false, "valor"));

            MethodInfo[] methods = item.Fields[0].GetType().GetMethods();

            MethodInfo method = null;
            foreach (MethodInfo info in methods)
            {
                if (info.Name == "get_Value")
                {
                    method = info;
                    break;
                }
            }

            object result = method.Invoke(item.Fields[0], null);

            Assert.AreEqual(result.GetType(), typeof(string));
            Assert.AreEqual((result as string), "valor");
        }
    }
}



