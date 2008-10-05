using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes.Gui
{
    [Serializable]
    [DataContract]
    public class DTGuiIssue
    {
        private int _id = 0;
        private string _title = string.Empty;
        private string _reportedBy = string.Empty;
        private DateTime _reportedDate;
        private string _wp = string.Empty;
        private int _ord = 0;
        private string _priority = string.Empty;
        private string _status = string.Empty;
        private string _category = string.Empty;
        private string _resolution = string.Empty;
        private List<DTField> _fields = null;
        private List<DTAttachment> _attachments = null;

        [DataMember]
        public List<DTField> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        [DataMember]
        public List<DTAttachment> Attachments
        {
            get { return _attachments; }
            set { _attachments = value; }
        }

        [DataMember]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        [DataMember]
        public string ReportedBy
        {
            get { return _reportedBy; }
            set { _reportedBy = value; }
        }

        [DataMember]
        public DateTime ReportedDate
        {
            get { return _reportedDate; }
            set { _reportedDate = value; }
        }

        [DataMember]
        public string WP
        {
            get { return _wp; }
            set { _wp = value; }
        }

        [DataMember]
        public int Ord
        {
            get { return _ord; }
            set { _ord = value; }
        }

        [DataMember]
        public string Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        [DataMember]
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        [DataMember]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        public string Resolution
        {
            get { return _resolution; }
            set { _resolution = value; }
        }

        public DTGuiIssue(){}

        public DTGuiIssue(DTItem issue) 
        {
            _id = ((DTFieldCounter)issue.GetField("ID")).Value;
            _title = ((DTFieldAtomicString)issue.GetField("Title")).Value;
            _reportedBy = ((DTFieldChoiceUser)issue.GetField("Reported by")).Value;
            _reportedDate = ((DTFieldAtomicDateTime)issue.GetField("Reported date")).Value;
            _wp = ((DTFieldChoiceLookup)issue.GetField("WP")).Value;
            _ord = ((DTFieldAtomicInteger)issue.GetField("Priority Order")).Value;
            _priority = ((DTFieldAtomicString)issue.GetField("Priority")).Value;
            _category = ((DTFieldAtomicString)issue.GetField("Category")).Value;
            _status = ((DTFieldAtomicString)issue.GetField("Status")).Value;
            _resolution = ((DTFieldAtomicString)issue.GetField("Resolution")).Value;

            _fields = new List<DTField>();
            List<DTField> collectionFields = issue.Fields;
            foreach (DTField field in collectionFields)
            {
                if (field.Name.CompareTo("ID") !=0 && field.Name.CompareTo("Title") !=0 &&
                    field.Name.CompareTo("Reported by") != 0 && field.Name.CompareTo("Reported date") != 0 &&
                    field.Name.CompareTo("WP") != 0 && field.Name.CompareTo("Prority Order") != 0 &&
                    field.Name.CompareTo("Priority") != 0 && field.Name.CompareTo("Category") != 0 &&
                    field.Name.CompareTo("Resolution") != 0 && field.Name.CompareTo("Status") != 0
                    )
                {
                    _fields.Add(new DTField(field));
                }
            }
            _attachments = new List<DTAttachment>();
            List<DTAttachment> collectionAttachments = issue.Attachments;
            foreach (DTAttachment attach in collectionAttachments)
            {
                _attachments.Add(new DTAttachment(attach));
            }
        }

        public DTGuiIssue(int id, string title, string reportedBy, DateTime reportedDate, string wp, int ord,
            string priority, string category, string status, string resolution, List<DTField> fields,
            List<DTAttachment> attachments)
        {
            _id = id;
            _title = title;
            _reportedBy = reportedBy;
            _reportedDate = reportedDate;
            _wp = wp;
            _ord = ord;
            _priority = priority;
            _category = category;
            _status = status;
            _resolution = resolution;
            _fields = new List<DTField>(fields);
            _attachments = new List<DTAttachment>(attachments);
        }
    }
}
