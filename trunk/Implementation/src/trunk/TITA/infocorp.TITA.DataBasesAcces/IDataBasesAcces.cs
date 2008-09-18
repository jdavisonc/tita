using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace infocorp.TITA.DataBasesAcces
{
    interface IDataBaseAcces
    {
        void AddContract(DataContract c);
        void DeleteContract(string idContract);
        List<Contract> ContractList();
        string ContractSite(string idContract);
        void ModifyContract(DataContract c);
        /*public void Update();*/

    }
}
