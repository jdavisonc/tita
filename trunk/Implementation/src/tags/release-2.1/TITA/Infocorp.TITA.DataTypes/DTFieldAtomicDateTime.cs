using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTFieldAtomicDateTime : DTFieldAtomic
    {
        private DateTime _value;
        private bool _isDateOnly = false;

        public DTFieldAtomicDateTime() : base()
        {}

        public DTFieldAtomicDateTime(string name, bool required, bool hidden, bool isReadOnly, bool isDateOnly)
            : base(name, required, hidden, isReadOnly)
        {
            _isDateOnly = isDateOnly;
        }

        public DTFieldAtomicDateTime(string name, bool required, bool hidden, bool isReadOnly, bool isDateOnly, DateTime value)
            : base(name, required, hidden, isReadOnly)
        {
            _value = value;
            _isDateOnly = isDateOnly;
        }

        public DTFieldAtomicDateTime(DTFieldAtomicDateTime dtFieldAtomicDateTime)
            : base((DTFieldAtomic)dtFieldAtomicDateTime)
        {
            _value = dtFieldAtomicDateTime.Value;
            _isDateOnly = dtFieldAtomicDateTime.IsDateOnly;
        }

        public override DTField.Types GetCustomType()
        {
            return Types.DateTime;
        }

        [DataMember]
        public DateTime Value
        {
            get { return _value; }
            set { _value = value; }
        }

        [DataMember]
        public bool IsDateOnly
        {
            get { return _isDateOnly; }
        }
    }
}
