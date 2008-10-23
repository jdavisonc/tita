using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTFieldAtomicNote : DTFieldAtomic
    {
        private string _value = string.Empty;

        public DTFieldAtomicNote() : base()
        {}

        public DTFieldAtomicNote(string name, string internalName, bool required, bool hidden, bool isReadOnly)
            : base(name, internalName, required, hidden, isReadOnly)
        {}

        public DTFieldAtomicNote(string name, string internalName, bool required, bool hidden, bool isReadOnly, string value)
            : base(name, internalName, required, hidden, isReadOnly)
        {
            _value = value;
        }

        public DTFieldAtomicNote(DTFieldAtomicNote dtFieldAtomicNote)
            : base((DTFieldAtomic)dtFieldAtomicNote)
        {
            _value = dtFieldAtomicNote.Value;
        }

        public override DTField.Types GetCustomType()
        {
            return Types.Note;
        }

        [DataMember]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
