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
        private bool _required = false;
        private bool _hidden = false;
        private bool _isReadOnly = false;

        public enum Types
        {
            Integer = 0,
            String = 1,
            Choice = 2,
            Boolean = 3,
            DateTime = 4,
            Note = 5,
            User = 6,
            Counter = 7,
            Lookup = 8,
            Default = 9
        }

        public DTField(string name, bool required, bool hidden, bool isReadOnly)
        {
            _name = name;
            _required = required;
            _hidden = hidden;
            _isReadOnly = isReadOnly;
        }

        public DTField() { }

        public DTField(DTField field)
        {
            _name = field.Name;
            _required = field.Required;
            _hidden = field.Hidden;
            _isReadOnly = field.IsReadOnly;
        }

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual Types GetCustomType()
        {
            return Types.Default;
        }
        
        [DataMember]
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }

        [DataMember]
        public bool Hidden
        {
            get { return _hidden; }
            set { _hidden = value; }
        }

        [DataMember]
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { _isReadOnly = value; }
        }

        public override bool Equals(object obj)
        {
            DTField field = obj as DTField;

            return field.Name.ToLower() == this.Name.ToLower();
        }
    }
}