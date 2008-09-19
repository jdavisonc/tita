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
            return SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint().GetIssues(urlSite);
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
