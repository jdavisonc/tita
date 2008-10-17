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
        private DTItem _item;
        public DTItem Item
        {
            get { return _item; }
            set { _item = value; }
        }

        private ItemType _commandItemType;
        public ItemType CommandItemType
        {
            get { return _commandItemType; }
            set { _commandItemType = value; }
        }

        private string _contractId;
        public string ContractId
        {
            get { return _contractId; }
            set { _contractId = value; }
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            DTField f1 = this.Item.Fields.Find(delegate(DTField f) { return f.Name == "ID"; });
            DTField f2 = (obj as DTCommandInfo).Item.Fields.Find(delegate(DTField f) { return f.Name == "ID"; });
            if (f1 != null && f2 != null)
            {
                result =
                    this.CommandItemType == (obj as DTCommandInfo).CommandItemType &&
                    (f1 as DTFieldCounter).Value == (f2 as DTFieldCounter).Value &&
                    this.ContractId.ToLower().Trim() == (obj as DTCommandInfo).ContractId.ToLower().Trim();
            }

            return result;
        }


        public int CompareTo(object obj)
        {
            return this.CreationDate.CompareTo((obj as DTCommandInfo).CreationDate);
        }
    }
}
