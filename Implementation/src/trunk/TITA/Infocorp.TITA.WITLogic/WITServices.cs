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
            return new List<DTIssue>() { issue };

            //return SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint().GetIssues(urlSite);
        }

        public void ApplyChanges()
        {
            throw new NotImplementedException();
        }

        public void AddNewIssue(DTIssue issue)
        {
            throw new NotImplementedException();
        }

        public void ModifyIssue(DTIssue issue)
        {
            throw new NotImplementedException();
        }

        public void DeleteIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
