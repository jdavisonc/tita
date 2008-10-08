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
        }

        public WITServices(DataBaseAccess.DataBaseAccess db, ISharePoint sharepoint)
        {
            _db = db;
            _sharepoint = sharepoint;
        }

        #endregion

        #region IssueMethods

        public DTItem GetIssueTemplate(string siteUrl)
        {
            return GetItemTemplate(siteUrl, ItemType.ISSUE);
        }

        public List<DTItem> GetIssues(string siteUrl)
        {
            return GetItems(siteUrl, ItemType.ISSUE);
        }

        public void AddIssue(DTItem issue, string siteUrl)
        {
            AddCommand(issue, ItemType.ISSUE, siteUrl);
        }

        public void ModifyIssue(DTItem issue, string siteUrl)
        {
            ModifyItem(issue,ItemType.ISSUE, siteUrl);
        }

        public void DeleteIssue(int issueId, string siteUrl)
        {
            DeleteItem(issueId, ItemType.ISSUE, siteUrl);
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

        public DTItem GetTaskTemplate(string siteUrl)
        {
            return GetItemTemplate(siteUrl, ItemType.TASK);
        }
        
        public List<DTItem> GetTasks(string siteUrl)
        {
            return GetItems(siteUrl, ItemType.TASK);
        }

        public void AddTask( DTItem task, string siteUrl)
        {
            AddCommand(task, ItemType.TASK, siteUrl);
        }

        public void DeleteTask(int taskId, string siteUrl)
        {
             DeleteItem(taskId, ItemType.TASK,siteUrl);
        }

        public void UpdateTask(DTItem task, string siteUrl)
        {
            ModifyItem(task, ItemType.TASK, siteUrl);
        }

        #endregion

        #region IWITServices Members

        public DTItem GetWorkPackageTemplate(string siteUrl)
        {
            return GetItemTemplate(siteUrl, ItemType.WORKPACKAGE);
        }

        public List<DTItem> GetWorkPackages(string siteUrl)
        {
            return GetItems(siteUrl, ItemType.WORKPACKAGE);
        }

        public void AddWorkPackage(DTItem workPackage, string siteUrl)
        {
            AddCommand(workPackage, ItemType.WORKPACKAGE, siteUrl);
        }

        public void DeleteWorkPackage(int workPackageId, string siteUrl)
        {
             DeleteItem(workPackageId, ItemType.WORKPACKAGE, siteUrl);
        }

        public void UpdateWorkPackage(DTItem workPackage, string siteUrl)
        {
             ModifyItem(workPackage, ItemType.WORKPACKAGE, siteUrl);
        }

        #endregion

        #region GenericMethods

        private DTItem GetItemTemplate(string siteUrl, ItemType itemType)
        {
            DTItem issue = new DTItem();
            switch (itemType)
            {
                case ItemType.ISSUE:
                    issue.Fields = _sharepoint.GetFieldsIssue(siteUrl);
                    break;
                case ItemType.TASK:
                    issue.Fields = _sharepoint.GetFieldsTask(siteUrl);
                    break;
                case ItemType.WORKPACKAGE:
                    issue.Fields = _sharepoint.GetFieldsWorkPackage(siteUrl);
                    break;
                default:
                    //No debería pasar...
                    break;
            }


            return issue;
        }

        private List<DTItem> GetItems(string siteUrl, ItemType itemType)
        {
            List<DTItem> result = new List<DTItem>();
            List<DTCommandInfo> commands = WITCommandState.Instance().GetCommands(itemType, siteUrl);
                
            switch (itemType)
            {
                case ItemType.ISSUE:
                    result = _sharepoint.GetIssues(siteUrl);
                    break;
                case ItemType.TASK:
                    result = _sharepoint.GetTasks(siteUrl);
                    break;
                case ItemType.WORKPACKAGE:
                    result = _sharepoint.GetWorkPackages(siteUrl);
                    break;
                default:
                    //No debería pasar...
                    break;
            }
            commands.ForEach(delegate(DTCommandInfo command)
            {
                if (command.CommandItemType == itemType && command.SiteUrl.ToLower().Trim() == siteUrl.ToLower().Trim())
                {
                    if (command.CommandType == CommandType.ADD || command.CommandType == CommandType.MODIFY)
                    {
                        DTFieldAtomicBoolean isLocal = new DTFieldAtomicBoolean("IsLocal", false, true, true, true);
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

        public bool ApplyChanges(string siteUrl)
        {
            List<DTCommandInfo> commands = WITCommandState.Instance().GetCommands(siteUrl);
            ISharePoint spu = _sharepoint;
            commands.Sort();
            bool result = true;
            foreach (DTCommandInfo command in commands)
            {
                command.Item.Fields.RemoveAll(delegate(DTField f) { return f.Name == "IsLocal"; });
                switch (command.CommandType)
                {
                    case CommandType.ADD:
                        result = spu.AddIssue(siteUrl, command.Item);
                        break;
                    case CommandType.MODIFY:
                        result = spu.UpdateIssue(siteUrl, command.Item);
                        break;
                    case CommandType.DELETE:
                        int issueId = Convert.ToInt32((command.Item.Fields.Find(delegate(DTField f) { return f.Name.ToLower() == "id"; }) as DTFieldCounter).Value);
                        result = spu.DeleteIssue(siteUrl, issueId);
                        break;
                    default:
                        break;
                }
            }

            commands.Clear();
            return result;
        }

        public bool HasPendingChanges(string siteUrl)
        {
            return WITCommandState.Instance().GetCommands(siteUrl).Count > 0;
        }

        private void AddCommand(DTItem issue, ItemType itemType, string siteUrl)
        {
            DTCommandInfo command = new DTCommandInfo();
            command.CommandType = CommandType.ADD;
            command.CreationDate = DateTime.Now;
            command.CommandItemType = itemType;
            command.SiteUrl = siteUrl;

            DTField field = new DTField();
            field.Name = "ID";

            if (issue.Fields.Contains(field))
            {
                issue.Fields.Remove(field);
            }

            command.Item = issue;

            WITCommandState.Instance().AddCommand(command);
        }

        private void ModifyItem(DTItem issue, ItemType itemType, string siteUrl)
        {
            DTCommandInfo command = new DTCommandInfo();
            command.CommandType = CommandType.MODIFY;
            command.CreationDate = DateTime.Now;
            command.Item = issue;
            command.SiteUrl = siteUrl;
            command.CommandItemType = itemType;

            WITCommandState.Instance().AddCommand(command);
        }

        private void DeleteItem(int issueId, ItemType itemType, string siteUrl)
        {
            DTCommandInfo command = new DTCommandInfo();
            command.CommandType = CommandType.DELETE;
            command.CreationDate = DateTime.Now;
            command.Item = new DTItem();
            command.CommandItemType = itemType;
            command.SiteUrl = siteUrl;

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