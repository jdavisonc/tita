using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.DataBaseAccess
{
   /* public class DataContract
    {
        public string idContract;
        public string site;
        public string user;
        public DataContract(string idContract, string site, string user)
        {
            this.idContract = idContract;
            this.site = site;
            this.user = user;
        }

    }*/







    public class DataBaseAcces
    {
        public void AddContract(DTContract c)
        {
            LinqDataContext dc = new LinqDataContext();
            Contract contract = new Contract();
            contract.id_contract = c.ContractId;
            contract.site = c.Site;
            contract.user = c.UserName;
            dc.Contracts.InsertOnSubmit(contract);
            dc.SubmitChanges();

        }

        public void DeleteContract(string idContract)
        {

            LinqDataContext dc = new LinqDataContext();
            var contract = from u in dc.Contracts
                           where u.id_contract == idContract
                           select u;

            if (contract.Count() > 0)
            {

                dc.Contracts.DeleteOnSubmit(contract.First());
                dc.SubmitChanges();
            }

        }

        public List<Contract> ContractList()
        {
            LinqDataContext dc = new LinqDataContext();
            return dc.Contracts.ToList();
        }

        public string ContractSite(string idContract)
        {
            LinqDataContext dc = new LinqDataContext();
            var contract = from u in dc.Contracts
                           where u.id_contract == idContract
                           select u;

            if (contract.Count() > 0)
                return contract.First().site;
            else
                return null;

        }

        public void ModifyContract(DTContract c)
        {
            LinqDataContext dc = new LinqDataContext();
            var contract = from u in dc.Contracts
                           where u.id_contract == c.ContractId
                           select u;

            if (contract.Count() > 0)
            {
                contract.First().site = c.Site;
                contract.First().user = c.UserName;
                dc.SubmitChanges();
            }
        }
        /* public void Update()
         { 
         }
         public Contract FindContract(string idContract) 
         { 
        
         }*/


    }
}
