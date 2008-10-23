using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTFieldAtomicNumber : DTFieldAtomic
    {
        private double _value = 0;

        public DTFieldAtomicNumber() : base()
        {}

        public DTFieldAtomicNumber(string name, string internalName, bool required, bool hidden, bool isReadOnly)
            : base(name, internalName, required, hidden, isReadOnly)
        {}

        public DTFieldAtomicNumber(string name, string internalName, bool required, bool hidden, bool isReadOnly, double value)
            : base(name, internalName, required, hidden, isReadOnly)
        {
            _value = value;
        }

        public DTFieldAtomicNumber(DTFieldAtomicNumber dtFieldAtomicInteger)
            : base((DTFieldAtomic)dtFieldAtomicInteger)
        {
            _value = dtFieldAtomicInteger.Value;
        }

        public override DTField.Types GetCustomType()
        {
            return Types.Number;
        }

        [DataMember]
        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
