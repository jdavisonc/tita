using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTIssue
    {
        [DataMember]
        private List<DTField> _fields = null;
        [DataMember]
        private List<DTAttachment> _attachments = null;

        public DTIssue(List<DTField> Fields, List<DTAttachment> Attachments)
        {
            _fields = new List<DTField>(Fields);
            _attachments = new List<DTAttachment>(Attachments);
        }

        public List<DTAttachment> Attachments
        {
            get { return _attachments; }
            set { _attachments = value; }
        }

        public List<DTField> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public DTField GetField(string nameField)
        {
            foreach (DTField field in _fields)
            {
                if (nameField.ToUpper().CompareTo(field.Name.ToUpper()) == 0)
                    return field;
            }
            return null;
        }
    }
}