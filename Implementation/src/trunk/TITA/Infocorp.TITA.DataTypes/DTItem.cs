using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTItem
    {
      
        private List<DTField> _fields = null;
        private List<DTAttachment> _attachments = null;

        public DTItem()
        { }

        public DTItem(List<DTField> Fields, List<DTAttachment> Attachments)
        {
            _fields = new List<DTField>(Fields);
            _attachments = new List<DTAttachment>(Attachments);
        }
        [DataMember]
        public List<DTAttachment> Attachments
        {
            get { return _attachments; }
            set { _attachments = value; }
        }
        [DataMember]
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

        public override bool Equals(object obj)
        {
            if (this.GetField("ID") != null && (obj as DTItem).GetField("ID") != null)
            {
                return (this.GetField("ID") as DTFieldCounter).Value == ((obj as DTItem).GetField("ID") as DTFieldCounter).Value;
            }
            else if (this.GetField("Id.") != null && (obj as DTItem).GetField("Id.") != null)
            {
                return (this.GetField("Id.") as DTFieldCounter).Value == ((obj as DTItem).GetField("Id.") as DTFieldCounter).Value;
            }

            return false;
        }
    }
}