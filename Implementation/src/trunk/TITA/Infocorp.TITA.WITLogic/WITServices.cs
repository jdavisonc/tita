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

        #region IWITServices Members

        public DTIssue GetIssueTemplate(string urlSite)
        {
            DTIssue issue = new DTIssue();

            issue.Fields = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint().GetFieldsIssue(urlSite);

            return issue;
        }

        public List<DTIssue> GetIssues(string urlSite)
        {

            List<DTIssue> result = new List<DTIssue>();
            List<DTCommandInfo> commands = WITCommandState.Instance().Commands;
            result = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint().GetIssues(urlSite);
            commands.ForEach(delegate(DTCommandInfo command)
            {
                if (command.CommandType == CommandType.ADD || command.CommandType == CommandType.MODIFY)
                {
                    DTField isLocal = new DTField("IsLocal", DTField.Types.Boolean, false, null, "true");
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
            ISharePoint spu = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint();
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
                        int issueId = Convert.ToInt32(command.Issue.Fields.Find(delegate(DTField f) { return f.Name.ToLower() == "id"; }).Value);
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

        public void AddNewIssue(DTIssue issue)
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

        public void ModifyIssue(DTIssue issue)
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
            command.Issue = new DTIssue();
            DTField field = new DTField();
            field.Name = "ID";
            field.Required = true;
            field.Type = DTField.Types.Counter;
            field.Value = issueId.ToString();
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
            throw new NotImplementedException();
        }

        public List<DTContract> GetContracts()
        {
            DataBaseAccess.DataBaseAccess db = new DataBaseAccess.DataBaseAccess();
            return db.ContractList();
            //return new List<DTContract>();
        }

        public string GetContractSite(string contractId)
        {
            throw new NotImplementedException();
        }

        public void ModifyContract(DTContract contract)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}