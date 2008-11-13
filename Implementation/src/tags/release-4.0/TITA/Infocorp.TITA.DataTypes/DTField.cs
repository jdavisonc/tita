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
        private string _internalName = string.Empty;
        private bool _required = false;
        private bool _hidden = false;
        private bool _isReadOnly = false;

        public enum Types
        {
            Number = 0,
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

        public DTField(string name, string internalName, bool required, bool hidden, bool isReadOnly)
        {
            _name = name;
            _internalName = internalName;
            _required = required;
            _hidden = hidden;
            _isReadOnly = isReadOnly;
        }

        public DTField() { }

        public DTField(DTField field)
        {
            _name = field.Name;
            _internalName = field.InternalName;
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

        [DataMember]
        public string InternalName
        {
            get { return _internalName; }
            set { _internalName = value; }
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
        
        private bool _showInGrid;
        [DataMember]
        public bool ShowInGrid
        {
            get { return _showInGrid; }
            set { _showInGrid = value; }        
        }

        private int _gridOrder;
        [DataMember]
        public int GridOrder
        {
            get { return _gridOrder; }
            set { _gridOrder = value; }
        }


        public override bool Equals(object obj)
        {
            DTField field = obj as DTField;

            return field.Name.ToLower().Replace(".", "") == this.Name.ToLower().Replace(".", "");
        }
    }
}