using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTFieldAtomicInteger : DTFieldAtomic
    {
        private int _value = 0;

        public DTFieldAtomicInteger() : base()
        {}
        
        public DTFieldAtomicInteger(string name, bool required, bool hidden, bool isReadOnly)
            : base(name, required, hidden, isReadOnly)
        {}

        public DTFieldAtomicInteger(string name, bool required, bool hidden, bool isReadOnly, int value)
            : base(name, required, hidden, isReadOnly)
        {
            _value = value;
        }

        public DTFieldAtomicInteger(DTFieldAtomicInteger dtFieldAtomicInteger)
            : base((DTFieldAtomic)dtFieldAtomicInteger)
        {
            _value = dtFieldAtomicInteger.Value;
        }

        public override DTField.Types GetCustomType()
        {
            return Types.Integer;
        }

        [DataMember]
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
