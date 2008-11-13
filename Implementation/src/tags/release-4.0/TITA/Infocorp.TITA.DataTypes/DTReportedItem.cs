using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable,DataContract]
    public class DTReportedItem
    {
        private string _category;
        [DataMember]
        public string Category
        {
            set{_category = value;}
            get{return _category;}
        }
        private string _status;
        [DataMember]
        public string Status
        {
            set{_status = value;}
            get{return _status;}
        }
        private int _count;
        [DataMember]
        public int Count
        {
            set{_count = value;}
            get{return _count;}
        }

        public DTReportedItem() { }
        public DTReportedItem(string category, string status, int count)
        {
            _category = category;
            _status = status;
            _count = count;
        }
        
    
    }
}
