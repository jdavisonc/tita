using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.WITLogic
{
    class WITServices : IWITServices
    {

        #region IWITServices Members

        public DTIssue GetIssueTemplate(string urlSite)
        {
            DTIssue issue = new DTIssue();

            DTField field = new DTField();
            field.Name = "ID";
            field.Required = true;
            field.Type = DTField.Types.Integer;
            field.Value = "1";

            DTField field2 = new DTField();
            field2.Name = "Title";
            field2.Required = true;
            field2.Type = DTField.Types.String;
            field2.Value = "Título";

            DTField field3 = new DTField();
            field3.Name = "Urgent?";
            field3.Required = true;
            field3.Type = DTField.Types.Boolean;
            field3.Value = "true";

            DTField field4 = new DTField();
            field4.Name = "ReportedDate";
            field4.Required = true;
            field4.Type = DTField.Types.DateTime;
            field4.Value = DateTime.Now.AddDays(5).ToShortDateString();

            DTField field5 = new DTField();
            field5.Name = "Priority";
            field5.Required = true;
            field5.Type = DTField.Types.Choice;
            field5.Choices = new List<string>() { "Alta", "Media", "Baja" };
            field5.Value = "Media";

            issue.Fields = new List<DTField>() { field, field2, field3, field4, field5 };

            return issue;
        }

        public List<DTIssue> GetIssues(string urlSite)
        {

            List<DTIssue> result = new List<DTIssue>();
            List<DTCommandInfo> commands =  WITCommandState.Instance().Commands;

            commands.ForEach(delegate(DTCommandInfo command)
            {
                if (command.CommandType == CommandType.ADD)
                {
                    result.Add(command.Issue);
                };
            });

            return SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint().GetIssues(urlSite);
            //return result;
        }

        public void ApplyChanges()
        {
            throw new NotImplementedException();
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
            field.Type  = DTField.Types.Integer;
            field.Value = issueId.ToString();
            command.Issue.Fields = new List<DTField>() { field };

            WITCommandState.Instance().AddCommand(command);
        }



        #endregion

        #region IWITServices Members


        public void AddNewContract(DTContract contract)
        {
            throw new NotImplementedException();
        }

        public void DeleteContract(string contractId)
        {
            throw new NotImplementedException();
        }

        public void ChangeCurrentContract(int contractId)
        {
            throw new NotImplementedException();
        }

        public List<DTContract> GetContracts()
        {
            return new List<DTContract>();
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
