using System.Collections.Generic;
using System.Collections.Specialized;

namespace Infocorp.TITA.DataTypes
{
    public class DTField
    {
        private string _name;
        private Types _type;
        private bool _required;
        private string _value;
        private List<string> _choices;

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

        public DTField()
        {
            _name = string.Empty;
            _type = Types.Integer;
            _required = false;
            _value = string.Empty;
            _choices = new List<string>();
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
            //set { _name = value; }
        }

        public Types Type
        {
            get { return _type; }
            //set { _type = value; }
        }
        
        public bool Required
        {
            get { return _required; }
            //set { _required = value; }
        }

        public List<string> Choices
        {
            get { return _choices; }
            //set { _choices = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}