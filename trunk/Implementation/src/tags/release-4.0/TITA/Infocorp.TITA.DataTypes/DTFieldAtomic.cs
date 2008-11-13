using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTFieldAtomic : DTField
    {
        public DTFieldAtomic() : base()
        {}

        public DTFieldAtomic(string name, string internalName, bool required, bool hidden, bool isReadOnly)
            : base(name, internalName, required, hidden, isReadOnly)
        { }

        public DTFieldAtomic(DTFieldAtomic dtFieldAtomic)
            : base((DTField)dtFieldAtomic)
        {}

        public override Types GetCustomType()
        {
            return base.GetCustomType();
        }
    }
}
