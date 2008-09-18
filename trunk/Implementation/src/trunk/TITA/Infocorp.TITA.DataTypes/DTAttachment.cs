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
            Data.CopyTo(_data, data.Length);
        }

        public DTAttachment(string name, string url)
        {
            _name = name;
            _url = Url;
            _data = null;
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