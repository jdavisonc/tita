﻿using System;
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
            set { _contractId = value; }
        }
        private string _site;
        [DataMember]
        public string Site
        {
            get { return _site; }
            set { _site = value; }
        }
        private string _userName;
        [DataMember]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _issuesList;
        [DataMember]
        public string issuesList
        {
            get { return _issuesList; }
            set { _issuesList = value; }
        }
        private string _workPackageList;
        [DataMember]
        public string workPackageList
        {
            get { return _workPackageList; }
            set { _workPackageList = value; }
        }
        private string _taskList;
        [DataMember]
        public string taskList
        {
            get { return _taskList; }
            set { _taskList = value; }
        }

    }
}
