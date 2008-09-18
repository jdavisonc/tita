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
        [DataMember]
        private string _name = string.Empty;
        [DataMember]
        private byte[] _data = null;
        [DataMember]
        private string _url = string.Empty;
        
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

        public string Name
        {
            get { return _name; }
        }

        public byte[] Data
        {
            get { return _data; }
        }

        public string Url
        {
            get { return _url; }
        }

    }
}