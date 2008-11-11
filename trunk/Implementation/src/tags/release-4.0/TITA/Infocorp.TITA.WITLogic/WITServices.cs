using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;
using Infocorp.TITA.SharePointUtilities;

namespace Infocorp.TITA.WITLogic
{
    class WITServices : IWITServices
    {

        #region ctor

        DataBaseAccess.DataBaseAccess _db = null;
        ISharePoint _sharepoint = null;

        public WITServices()
        {
            _sharepoint = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint();
            _db = new Infocorp.TITA.DataBaseAccess.DataBaseAccess();
        }

        public WITServices(DataBaseAccess.DataBaseAccess db, ISharePoint sharepoint)
        {
            _db = db;
            _sharepoint = sharepoint;
        }

        #endregion

        #region IssueMethods

        public DTItem GetIssueTemplate(string contractId)
        {
            return GetItemTemplate(contractId, ItemType.ISSUE);
        }

        public List<DTItem> GetIssues(string contractId)
        {            
            object  credential  = System.Net.CredentialCache.DefaultCredentials;
            return GetIssuesWithQuery(contractId, string.Empty);
        }

        private List<DTItem> GetIssuesWithQuery(string contractId, string query)
        {
            return GetItems(contractId, ItemType.ISSUE, query);
        }

        public List<DTItem> GetIssues(string contractId, string workpackageName)
        {
            //string camlQuery = @"<Query><Where><Eq><FieldRef Name=""_x0057_P2"" /><Value Type=""Lookup"">" + workpackageId + "</Value></Eq></Where></Query>";            

            string camlQuery = string.Empty;
            List<DTItem> result = this.GetIssuesWithQuery(contractId, camlQuery);

            result = result.FindAll(delegate(DTItem item)
            {
                DTFieldChoiceLookup wpField = (DTFieldChoiceLookup)item.Fields.Find(delegate(DTField field) { return (field.Name == "Work Package" || field.Name == "Paquete de Trabajo"); });
                return wpField.Value.Trim() == workpackageName.Trim();

            });


            return result;
        }

        public void AddIssue(DTItem issue, string contractId)
        {
            AddCommand(issue, ItemType.ISSUE, contractId);
        }

        public void ModifyIssue(DTItem issue, string contractId)
        {
            ModifyItem(issue,ItemType.ISSUE, contractId);
        }

        public void DeleteIssue(int issueId, string contractId)
        {
            DeleteItem(issueId, ItemType.ISSUE, contractId);
        }

        #endregion

        #region ContractMethods

        public void AddNewContract(DTContract contract)
        {
            _db.AddContract(contract);
        }

        public void DeleteContract(string contractId)
        {        
            _db.DeleteContract(contractId);
        }

        public void ChangeCurrentContract(int contractId)
        {
            //TODO 
            throw new NotImplementedException("Esta operación no se hizo!!");
        }

        public List<DTContract> GetContracts()
        {
            return _db.ContractList();         
        }

        public string GetContractSite(string contractId)
        {
            return _db.ContractSite(contractId);
        }

        public void ModifyContract(DTContract contract)
        {
            _db.ModifyContract(contract);
        }

        #endregion

        #region TaskMethods

        public DTItem GetTaskTemplate(string contractId)
        {
            return GetItemTemplate(contractId, ItemType.TASK);
        }
        
        public List<DTItem> GetTasks(string contractId)
        {
            return GetItems(contractId, ItemType.TASK, string.Empty);
        }

        public List<DTItem> GetTasks(string contractId, string issueId)
        {
            List<DTItem> tasks = this.GetTasks(contractId);

            List<DTItem> result = tasks.FindAll(delegate(DTItem item)
            {
                DTFieldChoiceLookup wpField = (DTFieldChoiceLookup)item.Fields.Find(delegate(DTField field) { return (field.Name.ToLower() == "issue" || field.Name.ToLower() == "incidente"); });
                return wpField.Value == issueId;

            });

            return result;

            //return null;
        }

        public void AddTask( DTItem task, string contractId)
        {
            AddCommand(task, ItemType.TASK, contractId);
        }

        public void DeleteTask(int taskId, string contractId)
        {
             DeleteItem(taskId, ItemType.TASK,contractId);
        }

        public void UpdateTask(DTItem task, string contractId)
        {
            ModifyItem(task, ItemType.TASK, contractId);
        }

        #endregion

        #region IWITServices Members

        public DTItem GetWorkPackageTemplate(string contractId)
        {
            return GetItemTemplate(contractId, ItemType.WORKPACKAGE);
        }

        public List<DTItem> GetWorkPackages(string contractId)
        {
            return GetItems(contractId, ItemType.WORKPACKAGE, string.Empty);
        }

        public void AddWorkPackage(DTItem workPackage, string contractId)
        {
            AddCommand(workPackage, ItemType.WORKPACKAGE, contractId);
        }

        public void DeleteWorkPackage(int workPackageId, string contractId)
        {
             DeleteItem(workPackageId, ItemType.WORKPACKAGE, contractId);
        }

        public void UpdateWorkPackage(DTItem workPackage, string contractId)
        {
             ModifyItem(workPackage, ItemType.WORKPACKAGE, contractId);
        }

        #endregion

        #region GenericMethods

        private DTItem GetItemTemplate(string contractId, ItemType itemType)
        {
            DTItem issue = new DTItem();
            switch (itemType)
            {
                case ItemType.ISSUE:
                    issue.Fields = _sharepoint.GetFieldsIssue(contractId);
                    break;
                case ItemType.TASK:
                    issue.Fields = _sharepoint.GetFieldsTask(contractId);
                    break;
                case ItemType.WORKPACKAGE:
                    issue.Fields = _sharepoint.GetFieldsWorkPackage(contractId);
                    break;
                default:
                    //No debería pasar...
                    break;
            }


            return issue;
        }

        private List<DTItem> GetItems(string contractId, ItemType itemType, string camlQuery)
        {
            List<DTItem> result = new List<DTItem>();
            List<DTCommandInfo> commands = WITCommandState.Instance().GetCommands(itemType, contractId);            
            switch (itemType)
            {
                case ItemType.ISSUE:
                    result = _sharepoint.GetIssues(contractId,camlQuery);
                    break;
                case ItemType.TASK:
                    result = _sharepoint.GetTasks(contractId, camlQuery);
                    break;
                case ItemType.WORKPACKAGE:
                    result = _sharepoint.GetWorkPackages(contractId, camlQuery);
                    break;
                default:
                    //No debería pasar...
                    break;
            }
            commands.ForEach(delegate(DTCommandInfo command)
            {
                if (command.CommandItemType == itemType && command.ContractId.ToLower().Trim() == contractId.ToLower().Trim())
                {
                    if (command.CommandType == CommandType.ADD || command.CommandType == CommandType.MODIFY)
                    {
                        DTFieldAtomicBoolean isLocal = new DTFieldAtomicBoolean("IsLocal", "IsLocal", false,false, true, true);
                        command.Item.Fields.Add(isLocal);
                        //Si traigo uno que estoy modificando, devuelvo sólo la modificación
                        if (command.CommandType == CommandType.MODIFY)
                        {
                            result.Remove(command.Item);
                        }
                        result.Add(command.Item);
                    }
                    else
                    {
                        result.Remove(command.Item);
                    };
                }
            });

            return result;
        }

        public List<DTItem> ApplyChanges(string contractId, ItemType itemType)
        {            
            List<DTCommandInfo> commands = WITCommandState.Instance().GetCommands(contractId);
            ISharePoint spu = _sharepoint;
            commands = commands.FindAll(delegate(DTCommandInfo dt) { return dt.CommandItemType == itemType; });
            commands.Sort();
            bool result = true;
            List<DTCommandInfo> commandsNotExecuted = new List<DTCommandInfo>();
            string eMail = string.Empty;
            try
            {
                eMail = _sharepoint.GetCurrentUserEmail(contractId);
                eMail = String.IsNullOrEmpty(eMail) ? "grupopis08@gmail.com" : eMail;
            }
            catch (Exception exc)
            {
                //No se pudo obtener el email por alguna razón
            }

            foreach (DTCommandInfo command in commands)
            {
                try
                {
                    
                    switch (command.CommandType)
                    {
                        case CommandType.ADD:
                            switch (command.CommandItemType)
                            {
                                case ItemType.ISSUE:
                                    result = spu.AddIssue(contractId, command.Item);
                                    break;
                                case ItemType.TASK:
                                    result = spu.AddTask(contractId, command.Item);
                                    break;
                                case ItemType.WORKPACKAGE:
                                    result = spu.AddWorkPackage(contractId, command.Item);
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case CommandType.MODIFY:
                            switch (command.CommandItemType)
                            {
                                case ItemType.ISSUE:
                                    result = spu.UpdateIssue(contractId, command.Item);
                                    break;
                                case ItemType.TASK:
                                    result = spu.UpdateTask(contractId, command.Item);
                                    break;
                                case ItemType.WORKPACKAGE:
                                    result = spu.UpdateWorkPackage(contractId, command.Item);
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case CommandType.DELETE:
                            int issueId = Convert.ToInt32((command.Item.Fields.Find(delegate(DTField f) { return f.Name.ToLower().Replace(".","") == "id"; }) as DTFieldCounter).Value);
                            switch (command.CommandItemType)
                            {
                                case ItemType.ISSUE:
                                    result = spu.DeleteIssue(contractId, issueId);
                                    break;
                                case ItemType.TASK:
                                    result = spu.DeleteTask(contractId, issueId);
                                    break;
                                case ItemType.WORKPACKAGE:
                                    result = spu.DeleteWorkPackage(contractId, issueId);
                                    break;
                                default:
                                    break;
                            }

                            break;
                        default:
                            break;
                    }
                    command.Item.Fields.RemoveAll(delegate(DTField f) { return f.Name == "IsLocal"; });
                }
                catch (Exception exc)
                {
                    result = false;
                }
                if (!result)
                {
                    commandsNotExecuted.Add(command);
                }
                else
                {
                    
                        if (command.CommandItemType == ItemType.TASK)
                        {
                            try
                            {
                                EMailSender.SendTaskNotification(command, eMail);
                            }
                            catch (Exception exc)
                            {
                                string msg = exc.Message;
                            }
                        }
                    
                }
            }

            try
            {
                if (commands.Count != commandsNotExecuted.Count)
                {
                    //Si apliqué alguno actualizo la base
                    UpdateLastAccess(contractId);
                }
            }
            catch (Exception exc)
            {
                //Problemas en el acceso a la base
            }

            WITCommandState.Instance().ClearCommands(contractId, itemType);


            List<DTItem> items = new List<DTItem>();
            foreach (DTCommandInfo info in commandsNotExecuted){
                items.Add(info.Item);
            }

            //return commandsNotExecuted;
            return items;
        }

        public bool HasPendingChanges(string contractId)
        {
            return WITCommandState.Instance().GetCommands(contractId).Count > 0;
        }

        private void AddCommand(DTItem issue, ItemType itemType, string contractId)
        {
            DTCommandInfo command = new DTCommandInfo();
            command.CommandType = CommandType.ADD;
            command.CreationDate = DateTime.Now;
            command.CommandItemType = itemType;
            command.ContractId = contractId;

            DTField field = new DTField();
            field.Name = "ID";

            if (issue.Fields.Contains(field))
            {
                issue.Fields.Remove(field);
            }

            command.Item = issue;

            WITCommandState.Instance().AddCommand(command);
        }

        private void ModifyItem(DTItem issue, ItemType itemType, string contractId)
        {
            DTCommandInfo command = new DTCommandInfo();
            command.CommandType = CommandType.MODIFY;
            command.CreationDate = DateTime.Now;
            command.Item = issue;
            command.ContractId = contractId;
            command.CommandItemType = itemType;

            WITCommandState.Instance().AddCommand(command);
        }

        private void DeleteItem(int issueId, ItemType itemType, string contractId)
        {
            DTCommandInfo command = new DTCommandInfo();
            command.CommandType = CommandType.DELETE;
            command.CreationDate = DateTime.Now;
            command.Item = new DTItem();
            command.CommandItemType = itemType;
            command.ContractId = contractId;

            DTFieldCounter field = new DTFieldCounter();
            field.Hidden = true;
            field.IsReadOnly = true;
            field.Name = "ID";
            field.Required = true;
            field.Value = issueId;
            command.Item.Fields = new List<DTField>() { field };

            WITCommandState.Instance().AddCommand(command);
        }

        #endregion

        #region IWITServices Members

        public bool IsContractLocked(string contractId)
        {
            return _db.IsContractAquired(contractId);
        }

        public bool AquireContractWritePermission(string contractId)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return _db.AquireContract(contractId, userName);
        }

        public void ReleaseContractWritePermission(string contractId)
        {
            _db.ReleaseContract(contractId);
        }

        private bool IsContractAquiredByMe(string contractId)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return _db.IsContractAquiredByUser(contractId, userName);
        }

        private void UpdateLastAccess(string contractId)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            _db.UpdateLastAccess(contractId, userName);
        }

        #endregion

        #region IWITServices Members


        public bool SiteMapPropertyValueWorkPackages(string idContract, string property, string initialValue, string endValue)
        {
            bool result = true;
            try
            {
            _sharepoint.SiteMapPropertyValueWorkPackages(idContract, property, initialValue, endValue);
            }
            catch (Exception exc)
            {
                result = false;
            }

            return result;
        }

        public bool SiteMapPropertyValueIssues(string idContract, string property, string initialValue, string endValue)
        {
            bool result = true;
            try
            {
            _sharepoint.SiteMapPropertyValueIssues(idContract, property, initialValue, endValue);
            }
            catch (Exception exc)
            {
                result = false;
            }

            return result;
        }

        public bool SiteMapPropertyValueTasks(string idContract, string property, string initialValue, string endValue)
        {
            bool result = true;
            try
            {
                _sharepoint.SiteMapPropertyValueTasks(idContract, property, initialValue, endValue);
            }
            catch (Exception exc)
            {
                result = false;
            }

            return result;
        }

        public bool IsContractAvailable(string contractId)
        {
            bool result = true;

            try
            {
                GetIssues(contractId);
            }
            catch (Exception exc)
            {
                result = false;
            }

            return result;
        }

        #endregion
    }
}