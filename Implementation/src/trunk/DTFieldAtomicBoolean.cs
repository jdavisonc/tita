using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTFieldAtomicBoolean : DTFieldAtomic
    {
        private bool _value = false;

        public DTFieldAtomicBoolean() : base()
        {}

        public DTFieldAtomicBoolean(string name, bool required, bool hidden, bool isReadOnly)
            : base(name, required, hidden, isReadOnly)
        {}

        public DTFieldAtomicBoolean(string name, bool required, bool hidden, bool isReadOnly, bool value)
            : base(name, required, hidden, isReadOnly)
        {
            _value = value;
        }

        public DTFieldAtomicBoolean(DTFieldAtomicBoolean dtFieldAtomicBoolean)
            : base((DTFieldAtomic)dtFieldAtomicBoolean)
        {
            _value = dtFieldAtomicBoolean.Value;
        }
        
        public override DTField.Types GetCustomType()
        {
            return Types.Boolean;
        }

        [DataMember]
        public bool Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
