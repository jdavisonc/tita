using System.Collections.Generic;
using System.Collections.Specialized;

namespace Infocorp.TITA.DataTypes
{
    public class DTAttachment
    {
        private string _name;
        private byte[] _data;

        public DTAttachment()
        {
            _name = string.Empty;
            _data = new byte[0];
        }
        
        public DTAttachment(string Name, byte[] Data)
        {
            _name = string.Empty;
            _data = new byte[Data.Length];
            Data.CopyTo(_data, Data.Length);
        }

        public string Name
        {
            get { return _name; }
            //set { _name = value; }
        }

        public byte[] Data
        {
            get { return _data; }
            //set { _data = value; }
        }

    }
}