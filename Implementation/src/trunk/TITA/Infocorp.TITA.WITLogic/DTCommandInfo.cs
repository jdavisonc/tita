using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.WITLogic
{
    public class DTCommandInfo : IComparable
    {
        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }
        private CommandType _commandType;
        public CommandType CommandType
        {
            get { return _commandType; }
            set { _commandType = value; }
        }
        private DTIssue _issue;
        public DTIssue Issue
        {
            get { return _issue; }
            set { _issue = value; }
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            DTField f1 = this.Issue.Fields.Find(delegate(DTField f) { return f.Name == "ID"; });
            DTField f2 = (obj as DTCommandInfo).Issue.Fields.Find(delegate(DTField f) { return f.Name == "ID"; });
            if (f1 != null && f2 != null)
            {
                result = f1.Value == f2.Value;
            }

            return result;
        }


        public int CompareTo(object obj)
        {
            return this.CreationDate.CompareTo((obj as DTCommandInfo).CreationDate);
        }
    }
}
