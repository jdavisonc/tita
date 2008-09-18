using System.Collections.Generic;
using System;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    public class DTIssue
    {
        private List<DTField> _fields = null;
        private List<DTAttachment> _attachments = null;

        public DTIssue(List<DTField> Fields, List<DTAttachment> Attachments)
        {
            _fields = new List<DTField>(Fields);
            _attachments = new List<DTAttachment>(Attachments);
        }

        public List<DTAttachment> Attachments
        {
            get { return _attachments; }
        }

        public List<DTField> Fields
        {
            get { return _fields; }
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