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
        private bool _percentage = false;

        public DTFieldAtomicNumber() : base()
        {}

        public DTFieldAtomicNumber(string name, string internalName, bool required, bool hidden, bool isReadOnly, bool percentage)
            : base(name, internalName, required, hidden, isReadOnly)
        {
            _percentage = percentage;
        }

        public DTFieldAtomicNumber(string name, string internalName, bool required, bool hidden, bool isReadOnly, bool percentage, double value)
            : base(name, internalName, required, hidden, isReadOnly)
        {
            _value = value;
            _percentage = percentage;
        }

        public DTFieldAtomicNumber(DTFieldAtomicNumber dtFieldAtomicInteger)
            : base((DTFieldAtomic)dtFieldAtomicInteger)
        {
            _value = dtFieldAtomicInteger.Value;
            _percentage = dtFieldAtomicInteger.Percentage;
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
        
        [DataMember]
        public bool Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
        }
    }
}
