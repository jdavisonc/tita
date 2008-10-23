using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTFieldChoiceLookup : DTFieldChoice
    {
        private string _lookupField;
        private string _lookupList;

        public DTFieldChoiceLookup() : base() {}

        public DTFieldChoiceLookup(string name, string internalName, bool required, bool hidden, bool isReadOnly, List<string> choices, string lookupField, string lookupList)
            : base(name, internalName, required, hidden, isReadOnly, choices)
        {
            _lookupField = lookupField;
            _lookupList = lookupList;
        }

        public DTFieldChoiceLookup(string name, string internalName, bool required, bool hidden, bool isReadOnly, List<string> choices, string lookupField, string lookupList, string value)
            : base(name, internalName, required, hidden, isReadOnly, choices, value)
        {
            _lookupField = lookupField;
            _lookupList = lookupList;
        }

        public DTFieldChoiceLookup(DTFieldChoiceLookup dtFieldChoiceLookup)
            : base((DTFieldChoice)dtFieldChoiceLookup)
        {
            _lookupField = dtFieldChoiceLookup.LookupField;
            _lookupList = dtFieldChoiceLookup.LookupList;
        }

        public override DTField.Types GetCustomType()
        {
            return Types.Lookup;
        }

        [DataMember]
        public string LookupField
        {
            get { return _lookupField; }
            set { _lookupField = value; }
        }

        [DataMember]
        public string LookupList
        {
            get { return _lookupList; }
            set { _lookupList = value; }
        }
    }
}
