using System.Collections.Generic;

namespace Infocorp.TITA.DataTypes
{
    public class DTIssue
    {
        private List<DTField> _fields;
        private List<DTAttachment> _attachments;

        public DTIssue()
        {
            _fields = new List<DTField>();
        }

        public DTIssue(List<DTField> Fields, List<DTAttachment> Attachments)
        {
            _fields = new List<DTField>(Fields);
            _attachments = new List<DTAttachment>(Attachments);
        }

        public List<DTAttachment> Attachments
        {
            get { return _attachments; }
            //set { _attachments = value; }
        }

        public List<DTField> Fields
        {
            get { return _fields; }
            //set { _fields = value; }
        }
    }
}