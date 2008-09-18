using System.Collections.Generic;
using System.Collections.Specialized;
using System;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    public class DTField
    {
        private string _name = string.Empty;
        private Types _type = Types.Integer;
        private bool _required = false;
        private string _value = string.Empty;
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
        }

        public Types Type
        {
            get { return _type; }
        }
        
        public bool Required
        {
            get { return _required; }
        }

        public List<string> Choices
        {
            get { return _choices; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}