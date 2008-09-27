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

    }
}
