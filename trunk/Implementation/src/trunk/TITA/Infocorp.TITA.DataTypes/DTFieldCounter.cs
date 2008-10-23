using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTFieldCounter : DTField
    {
        private int _value = 0;

        public DTFieldCounter() : base() {}

        public DTFieldCounter(string name, string internalName, bool required, bool hidden, bool isReadOnly)
            : base(name, internalName, required, hidden, isReadOnly)
        {}

        public DTFieldCounter(string name, string internalName, bool required, bool hidden, bool isReadOnly, int value)
            : base(name, internalName, required, hidden, isReadOnly)
        {
            _value = value;
        }

        public DTFieldCounter(DTFieldCounter dtFieldCounter)
            : base((DTField)dtFieldCounter)
        {
            _value = dtFieldCounter.Value;
        }

        public override Types GetCustomType()
        {
            return Types.Counter;
        }

        [DataMember]
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
