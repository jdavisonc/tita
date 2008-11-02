using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.DataBaseAccess
{
    
    public class DataBaseAccess : IDataBaseAccess
    {
        private static int maxTime = 10;
        #region ABM CONTRACTS
        public void AddContract(DTContract c)
        {
            LinqDataContext dc = new LinqDataContext();
            Contract contract = new Contract();
            var consultant = from u in dc.Contracts
                             where u.site.Trim() == c.Site.Trim()
                             select u;
            if (consultant.Count() > 0)
            {
                foreach (var aux in consultant)
                {
                    aux.issues_list = c.issuesList;
                    aux.task_list = c.taskList;
                    aux.task_list = c.taskList;
                    aux.user = c.UserName;
                }
                
                dc.SubmitChanges();
            }
            else
            {
                contract.site = c.Site;
                contract.user = c.UserName;
                contract.issues_list = c.issuesList;
                contract.workpackage_list = c.workPackageList;
                contract.task_list = c.taskList;
                dc.Contracts.InsertOnSubmit(contract);
                dc.SubmitChanges();
            }


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
                return contractData;
            }
            else
                return null;
            
         }
        #endregion

        #region 
        public void AddCurrentUser(DTCurrentUser user)
        {
            LinqDataContext dc = new LinqDataContext();
            Current cUser = new Current();
            var current = from u in dc.Currents
                          where u.site == user.Site.Trim()
                          select u;
            if (current.Count() > 0)
            {
                foreach (var aux in current)
                {
                    aux.current_user = user.CurrentUser;
                    aux.last_modification = user.LastModification;
                    aux.logged_date = user.LoggedDate;
                }
                dc.SubmitChanges();
            }
            else
            {
                cUser.site = user.Site;
                cUser.current_user = user.CurrentUser;
                cUser.logged_date = user.LoggedDate;
                cUser.last_modification = user.LastModification;
                dc.Currents.InsertOnSubmit(cUser);
                dc.SubmitChanges();
            }

        }


        public void DeleteCurrent(string site)
        {
            LinqDataContext dc = new LinqDataContext();
            var current = from u in dc.Currents
                           where u.site.Trim() == site.Trim()
                           select u;

            if (current.Count() > 0)
            {

                dc.Currents.DeleteOnSubmit(current.First());
                dc.SubmitChanges();
            }
        }
        public List<DTCurrentUser> getCurrentsUsersList()
        {
            LinqDataContext dc = new LinqDataContext();
            List<Current> currents = dc.Currents.ToList();
            List<DTCurrentUser> result = new List<DTCurrentUser>();
            foreach (Current current in currents)
            {
                DTCurrentUser dtCurrent = new DTCurrentUser();
                
                dtCurrent.Site = current.site;
                dtCurrent.CurrentUser = current.current_user;
                dtCurrent.LoggedDate = current.logged_date;
                dtCurrent.LastModification = current.last_modification;
                result.Add(dtCurrent);
            }

            return result;
        }

        /// <summary>
        /// Devuelve true sii el contracto está adquirido por algún usuario
        /// teniendo en cuenta un posible timeout (si expiró por timeout, lo libera)
        /// </summary>
        /// <param name="contractId">Id de contrato</param>
        /// <returns>true sii está adqurido</returns>
        public bool IsContractAquired(string contractId)
        {
            LinqDataContext dc = new LinqDataContext();
            DTContract contract = GetContract(contractId);
            if (contract != null)
            {
                if (contract.UserName == null)
                    return false;

                else
                {

                    //me fijo si ese usuario expiro
                    var current = from u in dc.Currents
                                  where (u.current_user == contract.UserName) && (u.site == contract.Site)
                                  select u;
                    DateTime d1 = Convert.ToDateTime(current.First().last_modification);
                    DateTime d2 = Convert.ToDateTime(current.First().logged_date);
                    var time = (d1 - d2);
                    
                    if (time.Seconds > maxTime)
                    {
                        //remover usuario
                        contract.UserName = null;
                        AddContract(contract);
                        DeleteCurrent(contract.Site);
                        return false;
                    }
                    else
                        return true;

                }

            }
            else
            {
                throw new ArgumentException("No existe el contrato que desea");
                return false;
            }    
        }

        
        /// <summary>
        /// Obtiene un contrato para escritura
        /// </summary>
        /// <param name="contractId">Id de contrato</param>
        /// <param name="userName">UserName</param>
        /// <returns>Si se pudo obtener el permiso</returns>
        public bool AquireContract(string contractId, string userName)
        {
            LinqDataContext dc = new LinqDataContext();
            DTContract contract = GetContract(contractId);
            if (contract != null)
            {
                if (contract.UserName == null)
                {
                    contract.UserName = userName;
                    AddContract(contract);
                    Current newCurrent = new Current();
                    newCurrent.current_user = userName;
                    newCurrent.site = contract.Site.Trim();
                    newCurrent.logged_date = DateTime.Now.ToString();
                    newCurrent.last_modification = DateTime.Now.ToString();
                    dc.Currents.InsertOnSubmit(newCurrent);
                    dc.SubmitChanges();

                    return true;
                }
                else
                {

                    //me fijo si ese usuario no expiro
                    var current = from u in dc.Currents
                                  where (u.current_user == contract.UserName) && (u.site == contract.Site)
                                  select u;
                    DateTime d1 = Convert.ToDateTime(current.First().last_modification);
                    DateTime d2 = Convert.ToDateTime(current.First().logged_date);
                    var time = (d1 - d2);

                    if (time.Seconds > maxTime) 
                    {
                        //remover usuario
                        contract.UserName = null;
                        AddContract(contract);
                        dc.Currents.DeleteOnSubmit(current.First());
                        dc.SubmitChanges();
                        DTCurrentUser newCurrent = new DTCurrentUser();
                        newCurrent.CurrentUser  = userName;
                        newCurrent.Site = contract.Site.Trim();
                        newCurrent.LoggedDate = DateTime.Now.ToString();
                        newCurrent.LastModification = DateTime.Now.ToString();
                        AddCurrentUser(newCurrent);
                        return true;
                    }
                    else 
                        return false;
                
               }
            }
            else
            {
                throw new ArgumentException("No existe el contrato que desea");
                return false;
            }
            
            
        }

        /// <summary>
        /// Libera un contrato obtenido
        /// </summary>
        /// <param name="contractId">Id del contrato</param>
        public void ReleaseContract(string contractId)
        {
            LinqDataContext dc = new LinqDataContext();
            DTContract contract = GetContract(contractId);
            if (contract != null)
            {
                contract.UserName = null;
                AddContract(contract);
                DeleteCurrent(contract.Site);
            }
            else
            {
                throw new ArgumentException("No existe el contrato que desea");
             
            }
            
        }

        /// <summary>
        /// Devuelve true sii el contrato está adquirido por el usuario
        /// </summary>
        /// <param name="contractId">Id del contrato</param>
        /// <param name="userName">UserName</param>
        /// <returns>true sii está adquirido por el usuario</returns>
        public bool IsContractAquiredByUser(string contractId, string userName)
        {
            LinqDataContext dc = new LinqDataContext();
            DTContract contract = GetContract(contractId);
            if (contract != null)
            {
                if (contract.UserName != userName)
                    return false;

                else
                {

                    //me fijo si ese usuario expiro
                    var current = from u in dc.Currents
                                  where (u.current_user == contract.UserName) && (u.site == contract.Site)
                                  select u;
                    if (current.Count() > 0)
                    {
                        DateTime d1 = Convert.ToDateTime(current.First().last_modification);
                        DateTime d2 = Convert.ToDateTime(current.First().logged_date);
                        var time = (d1 - d2);

                        if (time.Seconds > maxTime)
                        {
                            //remover usuario
                            dc.Currents.DeleteOnSubmit(current.First());
                            dc.SubmitChanges();
                            return false;
                        }
                        else
                            return true;
                    }
                    else
                        return false;
                }   

            }
            else
            {
                throw new ArgumentException("No existe el contrato que desea");
                return false;
            }    
        }

        /// <summary>
        /// Actualiza el último acceso al contrato con DateTime.Now
        /// </summary>
        /// <param name="contractId">Id del contrato</param>
        /// <param name="userName">UserName</param>
        public void UpdateLastAccess(string contractId, string userName)
        {
            LinqDataContext dc = new LinqDataContext();
            DTContract contract = GetContract(contractId);
            var current = (from u in dc.Currents
                          where (u.current_user == contract.UserName) && (u.site == contract.Site)
                          select u);
            DTCurrentUser newCurrent = new DTCurrentUser();
            newCurrent.CurrentUser = current.First().current_user;
            newCurrent.LoggedDate = current.First().logged_date;
            newCurrent.LastModification = DateTime.Now.ToString();
            newCurrent.Site = current.First().site;
            DeleteCurrent(current.First().site);
            AddCurrentUser(newCurrent);
           
        }
        #endregion

       
    }
}
