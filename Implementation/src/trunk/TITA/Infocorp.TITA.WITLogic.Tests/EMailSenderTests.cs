using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net.Mail;
using System.Net;

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
    }
}
