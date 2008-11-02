using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.DataBaseAccess
{
    public interface IDataBaseAccess
    {
        public void AddContract(DTContract c);
        
        public void DeleteContract(string idContract);
        
        public List<DTContract> ContractList();
        
        public string ContractSite(string idContract);
        
        public void ModifyContract(DTContract c);
        
        public DTContract GetContract(string idContract);
        
        public void AddCurrentUser(DTCurrentUser user);
       
        public void DeleteCurrent(string site);
        
        public List<DTCurrentUser> getCurrentsUsersList();
        
        public bool IsContractAquired(string contractId);
        
        public bool AquireContract(string contractId, string userName);
       
        public void ReleaseContract(string contractId);

        public bool IsContractAquiredByUser(string contractId, string userName);

        public void UpdateLastAccess(string contractId, string userName);

    }
}
