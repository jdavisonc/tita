using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTAttachment
    {
      
        private string _name = string.Empty;
      
        private byte[] _data = null;
        
        private string _url = string.Empty;

        public DTAttachment() { }

        public DTAttachment(string name, byte[] data)
        {
            _name = name;
            _data = new byte[data.Length];
            data.CopyTo(_data, 0); 
        }

        public DTAttachment(string name, string url)
        {
            _name = name;
            _url = Url;
            _data = null;
        }

        public DTAttachment(string url)
        {
            _url = url;
            _data = null;
            int index = _url.Length - 1;
            bool found = false;
            while (index >= 0 && !found)
            {
                if (_url[index].CompareTo('/') == 0)
                {
                    found = true;
                }
                else
                {
                    index--;
                }
            }
            _name = _url.Substring(index + 1);
        }

        public DTAttachment(DTAttachment attachment)
        {
            _name = attachment.Name;
            _url = attachment.Url;
            if (attachment.Data != null)
                Array.Copy(attachment.Data, _data, attachment.Data.Length);
        }

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [DataMember]
        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }
        [DataMember]
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

    }
}