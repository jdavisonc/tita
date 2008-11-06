using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [DataContract, Serializable]
    public class DTContract
    {
        private string _contractId;
        [DataMember]
        public string ContractId
        {
            get { return _contractId; }
            set { _contractId = value == null ? value : value.Trim(); }
        }
        private string _site;
        [DataMember]
        public string Site
        {
            get { return _site; }
            set { _site = value == null ? value : value.Trim(); }
        }
        private string _issuesList;
        [DataMember]
        public string issuesList
        {
            get { return _issuesList; }
            set { _issuesList = value == null ? value : value.Trim(); }
        }
        private string _workPackageList;
        [DataMember]
        public string workPackageList
        {
            get { return _workPackageList; }
            set { _workPackageList = value == null ? value : value.Trim(); }
        }
        private string _taskList;
        [DataMember]
        public string taskList
        {
            get { return _taskList; }
            set { _taskList = value == null ? value : value.Trim(); }
        }

    }
}
