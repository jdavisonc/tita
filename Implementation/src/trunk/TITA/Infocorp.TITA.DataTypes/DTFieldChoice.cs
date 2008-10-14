using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTFieldChoice : DTField
    {
        private List<string> _choices = null;
        private string _value = string.Empty;

        public DTFieldChoice() : base() {}

        public DTFieldChoice(string name, bool required, bool hidden, bool isReadOnly, List<string> choices)
            : base(name, required, hidden, isReadOnly)
        {
            _choices = new List<string>(choices);
        }

        public DTFieldChoice(string name, bool required, bool hidden, bool isReadOnly, List<string> choices, string value)
            : base(name, required, hidden, isReadOnly)
        {
            _choices = new List<string>(choices);
            _value = value;
        }

        public DTFieldChoice(DTFieldChoice dtFieldChoice)
            : base((DTField)dtFieldChoice)
        {
            _value = dtFieldChoice.Value;
        }

        public override DTField.Types GetCustomType()
        {
            return Types.Choice;
        }

        [DataMember]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        [DataMember]
        public List<string> Choices
        {
            get { return _choices; }
            set { _choices = value; }
        }
    }
}
