using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using System.IO;

namespace Infocorp.TITA.OutlookSharePoint
{
    public class OutlookSharePoint2003: IOutlookSharePoint
    {
        public OutlookSharePoint2003()
        {
        }

        public static byte C2b(char c)
        {
            if ((int)c < 256) return (byte)c;
            throw new Exception("character overflows a byte");
        }

        public static byte[] CA2b(char[] value)
        {
            byte[] buffer = new byte[value.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = C2b(value[i]);
            }
            return buffer;
        }


        #region IOutlookSharePoint Members

        public bool AddIssue(string urlSite)
        {
            using (SPSite site = new SPSite(urlSite))
            {
                site.AllowUnsafeUpdates = true;
                using (SPWeb web = site.OpenWeb())
                {
                    web.AllowUnsafeUpdates = true;
                    SPList list = web.Lists["Issues"];
                    SPListItem listItem = list.Items.Add();
                    listItem["Title"] = "test2";
                    listItem["Comment"] = "HOLA QUESO";
                    SPAttachmentCollection attachmentCollection = listItem.Attachments;

                    // for more help please see .net sdk
                    FileStream fs = new FileStream("C:\\Test.txt", FileMode.Open, FileAccess.Read);
                    char[] buffer = new char[1024];
                    StreamReader sr = new StreamReader(fs);
                    int retval = sr.ReadBlock(buffer, 0, 1024); // no. of bytes read
                    Console.Write("Total Bytes Read = " + retval + "\n");
                    sr.Close();

                    attachmentCollection.Add("Test.txt", CA2b(buffer));
                    listItem.Update();
                }
            }
            return true;
        }

        #endregion

    }
}
