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
        [DataMember]
        public string ContractId { get; set; }

        [DataMember]
        public string Site { get; set; }

        [DataMember]
        public string UserName { get; set; }
    }
}
