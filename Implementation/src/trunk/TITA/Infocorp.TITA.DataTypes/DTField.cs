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
        private string _name = string.Empty;
        private Types _type = Types.Integer;
        private bool _required = false;
        private string _value = string.Empty;
        private List<string> _choices = null;
        private bool _isDateOnly = false;

        public enum Types
        {
            Integer = 0,
            String = 1,
            Choice = 2,
            Boolean = 3,
            DateTime = 4,
            Note = 5,
            User = 6,
            Counter = 7
        }

        public DTField(string Name, Types Type, bool Required, List<string> Choices)
        {
            _name = Name;
            _type = Type;
            _required = Required;
            _choices = new List<string>(Choices);
        }

        public DTField() { }

        public DTField(DTField field)
        {
            _name = field.Name;
            _type = field.Type;
            _required = field.Required;
            _choices = new List<string>(field.Choices);
            _value = field.Value;
        }

        public DTField(string Name, Types Type, bool Required, List<string> Choices, string Value)
        {
            _name = Name;
            _type = Type;
            _required = Required;
            _value = Value;
            if (Choices != null)
            {
                _choices = new List<string>(Choices);
            }
            else
            {
                _choices = new List<string>();
            }
        }
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [DataMember]
        public Types Type
        {
            get { return _type; }
            set { _type = value; }
        }
        [DataMember]
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }
        [DataMember]
        public List<string> Choices
        {
            get { return _choices; }
            set { _choices = value; }
        }
        [DataMember]
        public string Value
        {
            get 
            {
                if (_type == Types.DateTime && _isDateOnly)
                {
                    return (DateTime.Parse(_value)).ToString("yyyy-MM-dd") + " 00:00:00 a.m."; 
                }
                else 
                {
                    return _value; 
                }
            }
            set 
            { 
                if (_type == Types.DateTime && value.Length > 0)
                {
                    _value = (DateTime.Parse(value)).ToString("yyyy-MM-dd hh:mm:ss tt");
                }
                else
                {
                    _value = value;
                }
            }
        }

        [DataMember]
        public bool IsDateOnly
        {
            get { return _isDateOnly; }
            set 
            {
                if (_type == Types.DateTime)
                {
                    _isDateOnly = value;
                }
            }
        }

        public override bool Equals(object obj)
        {
            DTField field = obj as DTField;

            return field.Name.ToLower() == this.Name.ToLower();
        }
    }
}