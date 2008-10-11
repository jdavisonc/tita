using System;
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


        //I serialize the object which is created by it with the below function

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

        /*
    //Then again I create a MailItem object with this code

    MailItem mailItem =(Outlook.MailItem)this.CreateItem(Outlook.OlItemType.olMailItem);

    myMail myDeserializedMail = new myMail(mailItem);

    //And then i want to deserialize the object which i get from db (in binary form) with the below code

    public object getObjectWithByteArray(byte[] theByteArray)
    {
    MemoryStream ms = new MemoryStream(theByteArray);
    BinaryFormatter bf1 = new BinaryFormatter();
    ms.Position = 0;

    return bf1.Deserialize(ms);
    }

    --> getObjectWithByteArray(blob);

    But at this moment i raises exception that

    "ex = {"The constructor to deserialize an object of type 'GetMail.myMail'
    was not found."}"

    What can i do???
                      
                     
    *************************************************************
                     
    Usually a class that has manual serialisation (where you have a routine that
    does the serialising for you - your GetObjectData method) needs a ctor that
    looks something like this :

    protected MyClass(SerializationInfo info, StreamingContext context)

    {

    myint64 = info.GetInt64("f1");

    mybool = info.GetBoolean("b1");

    }

    You might try sticking a default ctor in, as  well

    i.e.

    public MyClass()
    */


    }
}
