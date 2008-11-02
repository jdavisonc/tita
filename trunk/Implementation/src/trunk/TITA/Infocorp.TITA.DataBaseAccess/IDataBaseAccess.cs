using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;
namespace Infocorp.TITA.DataBaseAccess
{
    public interface IDataBaseAccess
    {
        void AddContract(DTContract c);

        string GetContractId(string site);

        void DeleteContract(string idContract);
        
        List<DTContract> ContractList();
        
        string ContractSite(string idContract);
        
        void ModifyContract(DTContract c);
        
        DTContract GetContract(string idContract);
        
        void AddCurrentUser(DTCurrentUser user);
       
        void DeleteCurrent(string site);
        
        List<DTCurrentUser> getCurrentsUsersList();
        
        bool IsContractAquired(string contractId);
        
        bool AquireContract(string contractId, string userName);
       
        void ReleaseContract(string contractId);

        bool IsContractAquiredByUser(string contractId, string userName);

        void UpdateLastAccess(string contractId, string userName);

    }
}
