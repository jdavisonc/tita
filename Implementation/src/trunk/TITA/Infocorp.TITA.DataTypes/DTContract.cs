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
        string ContractId { get; set; }

        [DataMember]
        string Site { get; set; }

        [DataMember]
        string UserName { get; set; }
    }
}
