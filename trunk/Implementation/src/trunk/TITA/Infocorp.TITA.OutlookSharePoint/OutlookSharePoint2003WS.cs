using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.OutlookSharePoint
{
    public class OutlookSharePoint2003WS : IOutlookSharePoint
    {
        #region IOutlookSharePoint Members

        public bool AddIssue(string urlSite, Infocorp.TITA.DataTypes.DTItem issue)
        {
            throw new NotImplementedException();
        }

        public List<Infocorp.TITA.DataTypes.DTField> GetFieldsIssue(string urlSite, string nameListIssues)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
