using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTFieldAtomicString : DTFieldAtomic
    {
        private string _value = string.Empty;

        public DTFieldAtomicString() : base()
        {}

        public DTFieldAtomicString(string name, bool required, bool hidden, bool isReadOnly)
            : base(name, required, hidden, isReadOnly)
        {}

        public DTFieldAtomicString(string name, bool required, bool hidden, bool isReadOnly, string value)
            : base(name, required, hidden, isReadOnly)
        {
            _value = value;
        }

        public DTFieldAtomicString(DTFieldAtomicString dtFieldAtomicString)
            : base((DTFieldAtomic)dtFieldAtomicString)
        {
            _value = dtFieldAtomicString.Value;
        }

        public override DTField.Types GetCustomType()
        {
            return Types.String;
        }

        [DataMember]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
