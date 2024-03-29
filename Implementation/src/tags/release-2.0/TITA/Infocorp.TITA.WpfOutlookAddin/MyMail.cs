﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Office.Interop.Outlook;
using System.Runtime.Serialization;

namespace Infocorp.TITA.WpfOutlookAddin
{

    [Serializable]
    [DataContract]
    public class MyMail : ISerializable
    {

        MailItem _myMailObject;

        [DataMember]
        public MailItem MyMailSerilize
        {
            get { return _myMailObject; }
            set { _myMailObject = value; }
        }

        public MyMail() { }

        public MyMail(MailItem mail)
        {
            _myMailObject = mail;
        }

        public MailItem GetMail()
        {
            return _myMailObject;
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }

        public byte[] GetByteArrayWithObject()
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            //MyMail myMailObject = new MyMail(_myMail)
            string fileTmpName = System.IO.Path.GetTempFileName();
            _myMailObject.SaveAs(fileTmpName,OlSaveAsType.olMSG);
            FileStream oFile = new FileStream(fileTmpName,FileMode.Open, FileAccess.Read);
            byte[] imageData = new byte[oFile.Length];
            oFile.Read(imageData,0,(int)oFile.Length);
            oFile.Close();
            //binaryFormatter.Serialize(memoryStream
            
            //return memoryStream.ToArray();

            return imageData;
        }
   }
}
