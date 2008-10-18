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
            return GetItems(contractId, ItemType.ISSUE);
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
            try
            {
                DataBaseAccess.DataBaseAccess db = new DataBaseAccess.DataBaseAccess();
                db.AddContract(contract);
            }
            catch (Exception exc)
            {
                string s = exc.Message;
            }
        }

        public void DeleteContract(string contractId)
        {
            DataBaseAccess.DataBaseAccess db = new DataBaseAccess.DataBaseAccess();
            db.DeleteContract(contractId);
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
            return GetItems(contractId, ItemType.TASK);
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
            return GetItems(contractId, ItemType.WORKPACKAGE);
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

        private List<DTItem> GetItems(string contractId, ItemType itemType)
        {
            List<DTItem> result = new List<DTItem>();
            List<DTCommandInfo> commands = WITCommandState.Instance().GetCommands(itemType, contractId);
            string camlQuery = string.Empty;
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
                        DTFieldAtomicBoolean isLocal = new DTFieldAtomicBoolean("IsLocal", false,false, true, true);
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

        public bool ApplyChanges(string contractId, ItemType itemType)
        {            
            List<DTCommandInfo> commands = WITCommandState.Instance().GetCommands(contractId);
            ISharePoint spu = _sharepoint;
            commands = commands.FindAll(delegate(DTCommandInfo dt) { return dt.CommandItemType == itemType; });
            commands.Sort();
            bool result = true;
            foreach (DTCommandInfo command in commands)
            {
                command.Item.Fields.RemoveAll(delegate(DTField f) { return f.Name == "IsLocal"; });
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
                        int issueId = Convert.ToInt32((command.Item.Fields.Find(delegate(DTField f) { return f.Name.ToLower() == "id"; }) as DTFieldCounter).Value);
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
            }

            WITCommandState.Instance().ClearCommands(contractId);
            
            return result;
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
    }
}