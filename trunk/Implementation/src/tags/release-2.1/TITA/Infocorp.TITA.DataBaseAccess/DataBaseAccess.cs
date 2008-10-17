using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.DataBaseAccess
{


    public class DataBaseAccess
    {
        public void AddContract(DTContract c)
        {
            LinqDataContext dc = new LinqDataContext();
            Contract contract = new Contract();
            contract.site = c.Site;
            contract.user = c.UserName;
            contract.issues_list = c.issuesList;
            contract.workpackage_list = c.workPackageList;
            contract.task_list = c.taskList;
            dc.Contracts.InsertOnSubmit(contract);
            dc.SubmitChanges();

        }

        public void DeleteContract(string idContract)
        {

            LinqDataContext dc = new LinqDataContext();
            var contract = from u in dc.Contracts
                           where u.id_contract == int.Parse(idContract)
                           select u;

            if (contract.Count() > 0)
            {

                dc.Contracts.DeleteOnSubmit(contract.First());
                dc.SubmitChanges();
            }

        }

        public List<DTContract> ContractList()
        {
            LinqDataContext dc = new LinqDataContext();
            List<Contract> contracts = dc.Contracts.ToList();

            List<DTContract> result = new List<DTContract>();
            foreach (Contract contract in contracts)
            {
                DTContract dtContract = new DTContract();
                dtContract.ContractId = contract.id_contract.ToString();
                dtContract.Site = contract.site;
                dtContract.UserName = contract.user;
                dtContract.issuesList = contract.issues_list;
                dtContract.workPackageList = contract.workpackage_list;
                dtContract.taskList = contract.task_list;
                result.Add(dtContract);
            }

            return result;
        }

        public string ContractSite(string idContract)
        {
            LinqDataContext dc = new LinqDataContext();
            var contract = from u in dc.Contracts
                           where u.id_contract == int.Parse(idContract)
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
                           where u.id_contract == int.Parse(c.ContractId)
                           select u;

            if (contract.Count() > 0)
            {
                contract.First().site = c.Site;
                contract.First().user = c.UserName;
                contract.First().issues_list = c.issuesList;
                contract.First().workpackage_list = c.workPackageList;
                contract.First().task_list = c.taskList;
                dc.SubmitChanges();
            }
        }
        public DTContract GetContract(string idContract)
        {
            LinqDataContext dc = new LinqDataContext();
            DTContract contractData = new DTContract();
            var contract = from u in dc.Contracts
                           where u.id_contract == int.Parse(idContract)
                           select u;

            if (contract.Count() > 0)
            {
                contractData.ContractId = idContract;
                contractData.Site = contract.First().site;
                contractData.UserName = contract.First().user;
                contractData.issuesList = contract.First().issues_list;
                contractData.workPackageList = contract.First().workpackage_list;
                contractData.taskList = contract.First().task_list;
                dc.SubmitChanges();
            }
            return contractData;
         }


    }
}
