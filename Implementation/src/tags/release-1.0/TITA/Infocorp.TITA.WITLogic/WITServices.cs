﻿using System;
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
            /*
            DTField field = new DTField();
            field.Name = "ID";
            field.Required = true;
            field.Type = DTField.Types.Counter;
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
            */

            issue.Fields = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint().GetFieldsIssue(urlSite);

            return issue;
        }

        public List<DTIssue> GetIssues(string urlSite)
        {

            List<DTIssue> result = new List<DTIssue>();
            List<DTCommandInfo> commands =  WITCommandState.Instance().Commands;
            result = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint().GetIssues(urlSite);

            commands.ForEach(delegate(DTCommandInfo command)
            {
                if (command.CommandType == CommandType.ADD || command.CommandType == CommandType.MODIFY)
                {
                    result.Add(command.Issue);
                };
            });

            
            return result;
        }

        public void ApplyChanges(string siteUrl)
        {
            List<DTCommandInfo> commands = WITCommandState.Instance().Commands;
            ISharePoint spu = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint();
            commands.Sort();
            foreach (DTCommandInfo command in commands)
            {
                switch (command.CommandType)
                {
                    case CommandType.ADD:
                        spu.AddIssue(siteUrl, command.Issue);
                        break;
                    case CommandType.MODIFY:
                        spu.UpdateIssue(siteUrl, command.Issue);
                        break;
                    case CommandType.DELETE:
                        int issueId   = Convert.ToInt32(command.Issue.Fields.Find(delegate(DTField f) { return f.Name.ToLower() == "id"; }).Value);
                        spu.DeleteIssue(siteUrl, issueId);
                        break;
                    default:
                        break;
                }                
            }

            commands.Clear();
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
            field.Type  = DTField.Types.Counter;
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
            return  db.ContractList();
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
