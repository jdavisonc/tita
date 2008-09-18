using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTField
    {
        [DataMember]
        private string _name = string.Empty;
        [DataMember]
        private Types _type = Types.Integer;
        [DataMember]
        private bool _required = false;
        [DataMember]
        private string _value = string.Empty;
        [DataMember]
        private List<string> _choices = null;

        public enum Types
        {
            Integer = 0,
            String = 1,
            Choice = 2,
            Boolean = 3,
            DateTime = 4,
            Note = 5,
            User = 6
        }

        public DTField(string Name, Types Type, bool Required, List<string> Choices)
        {
            _name = Name;
            _type = Type;
            _required = Required;
            _choices = new List<string>(Choices);
        }

        public DTField(string Name, Types Type, bool Required, List<string> Choices, string Value)
        {
            _name = Name;
            _type = Type;
            _required = Required;
            _value = Value;
            _choices = new List<string>(Choices);
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Types Type
        {
            get { return _type; }
            set { _type = value; }
        }
        
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }

        public List<string> Choices
        {
            get { return _choices; }
            set { _choices = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}