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



        public List<DTIssue> GetIssues(string urlSite)
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

            issue.Fields = new List<DTField>() { field, field2 };

            List<DTCommandInfo> commands =  WITCommandState.Instance().Commands;
            

            List<DTIssue> result = new List<DTIssue>() { issue };

            commands.ForEach(delegate(DTCommandInfo command)
            {
                if (command.CommandType == CommandType.ADD)
                {
                    result.Add(command.Issue);
                };
            });

            //return SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint().GetIssues(urlSite);
            return result;
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
    }
}
