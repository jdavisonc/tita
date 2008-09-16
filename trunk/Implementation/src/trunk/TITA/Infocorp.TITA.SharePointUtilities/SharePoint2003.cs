using System;
using System.Collections.Generic;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.SharePointUtilities
{
    class SharePoint2003:ISharePoint
    {
        #region ISharePoint Members

        public List<DTIssue> GetIssues(string urlSite)
        {
            return new List<DTIssue>();
        }

        #endregion
    }
}
