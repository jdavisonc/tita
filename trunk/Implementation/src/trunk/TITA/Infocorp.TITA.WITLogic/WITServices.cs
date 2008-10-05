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

        #region IWITServices Members

        public DTItem GetIssueTemplate(string urlSite)
        {
            DTItem issue = new DTItem();

            issue.Fields =_sharepoint.GetFieldsIssue(urlSite);

            return issue;
        }

        public List<DTItem> GetIssues(string urlSite)
        {

            List<DTItem> result = new List<DTItem>();
            List<DTCommandInfo> commands = WITCommandState.Instance().Commands;
            result = _sharepoint.GetIssues(urlSite);
            commands.ForEach(delegate(DTCommandInfo command)
            {
                if (command.CommandType == CommandType.ADD || command.CommandType == CommandType.MODIFY)
                {
                    DTFieldAtomicBoolean isLocal = new DTFieldAtomicBoolean("IsLocal", false, true, true, true);
                    command.Issue.Fields.Add(isLocal);
                    //Si traigo uno que estoy modificando, devuelvo sólo la modificación
                    if (command.CommandType == CommandType.MODIFY)
                    {
                        result.Remove(command.Issue);
                    }
                    result.Add(command.Issue);
                }
                else
                {
                    result.Remove(command.Issue);
                };
            });


            return result;
        }

        public bool ApplyChanges(string siteUrl)
        {
            List<DTCommandInfo> commands = WITCommandState.Instance().Commands;
            ISharePoint spu = _sharepoint;
            commands.Sort();
            bool result = true;
            foreach (DTCommandInfo command in commands)
            {
                command.Issue.Fields.RemoveAll(delegate(DTField f) { return f.Name == "IsLocal"; });
                switch (command.CommandType)
                {
                    case CommandType.ADD:
                        result = spu.AddIssue(siteUrl, command.Issue);
                        break;
                    case CommandType.MODIFY:
                        result = spu.UpdateIssue(siteUrl, command.Issue);
                        break;
                    case CommandType.DELETE:
                        int issueId = Convert.ToInt32( (command.Issue.Fields.Find(delegate(DTField f) { return f.Name.ToLower() == "id"; }) as DTFieldAtomicNumber).Value);
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
            return WITCommandState.Instance().Commands.Count > 0;
        }

        public void AddNewIssue(DTItem issue)
        {
            DTCommandInfo command = new DTCommandInfo();
            command.CommandType = CommandType.ADD;
            command.CreationDate = DateTime.Now;
            DTField field = new DTField();
            field.Name = "ID";


            if (issue.Fields.Contains(field))
            {
                issue.Fields.Remove(field);
            }

            command.Issue = issue;

            WITCommandState.Instance().AddCommand(command);
        }

        public void ModifyIssue(DTItem issue)
        {
            DTCommandInfo command = new DTCommandInfo();
            command.CommandType = CommandType.MODIFY;
            command.CreationDate = DateTime.Now;
            command.Issue = issue;

            WITCommandState.Instance().AddCommand(command);
        }

        public void DeleteIssue(int issueId)
        {
            DTCommandInfo command = new DTCommandInfo();
            command.CommandType = CommandType.DELETE;
            command.CreationDate = DateTime.Now;
            command.Issue = new DTItem();
            DTFieldAtomicNumber field = new DTFieldAtomicNumber();
            field.Hidden = true;
            field.IsReadOnly = true;            
            field.Name = "ID";
            field.Required = true;            
            field.Value = issueId;
            command.Issue.Fields = new List<DTField>() { field };

            WITCommandState.Instance().AddCommand(command);
        }



        #endregion

        #region IWITServices Members


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

        #region IWITServices Members
        
        public List<DTItem> GetTasks(string urlSite)
        {
            return _sharepoint.GetTasks(urlSite);
        }

        public List<DTField> GetFieldsTask(string urlSite)
        {
            return _sharepoint.GetFieldsTask(urlSite);
        }

        public bool AddTask(string urlSite, DTItem task)
        {
            return _sharepoint.AddTask(urlSite, task);
        }

        public bool DeleteTask(string urlSite, int idTask)
        {
            return _sharepoint.DeleteTask(urlSite, idTask);
        }

        public bool UpdateTask(string urlSite, DTItem task)
        {
            return _sharepoint.UpdateTask(urlSite, task);
        }

        #endregion

        #region IWITServices Members


        public List<DTItem> GetWorkPackages(string urlSite)
        {
            return _sharepoint.GetWorkPackages(urlSite);
        }

        public List<DTField> GetFieldsWorkPackage(string urlSite)
        {
            return _sharepoint.GetFieldsWorkPackage(urlSite);
        }

        public bool AddWorkPackage(string urlSite, DTItem wp)
        {
            return _sharepoint.AddWorkPackage(urlSite, wp);
        }

        public bool DeleteWorkPackage(string urlSite, int idWp)
        {
            return _sharepoint.DeleteWorkPackage(urlSite, idWp);
        }

        public bool UpdateWorkPackage(string urlSite, DTItem wp)
        {
            return _sharepoint.UpdateWorkPackage(urlSite, wp);
        }

        #endregion
    }
}