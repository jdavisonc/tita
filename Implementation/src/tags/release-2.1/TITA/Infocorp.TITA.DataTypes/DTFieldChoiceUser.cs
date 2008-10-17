using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTFieldChoiceUser : DTFieldChoice
    {

        public DTFieldChoiceUser() : base() {}

        public DTFieldChoiceUser(string name, bool required, bool hidden, bool isReadOnly, List<string> choices)
            : base(name, required, hidden, isReadOnly, choices)
        {}

        public DTFieldChoiceUser(string name, bool required, bool hidden, bool isReadOnly, List<string> choices, string value)
            : base(name, required, hidden, isReadOnly, choices, value)
        {}

        public DTFieldChoiceUser(DTFieldChoiceUser dtFieldChoiceUser)
            : base((DTFieldChoice)dtFieldChoiceUser)
        {}

        public override Types GetCustomType()
        {
            return Types.User;
        }
    }
}
