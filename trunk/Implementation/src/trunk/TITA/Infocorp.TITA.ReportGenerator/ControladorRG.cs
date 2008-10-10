using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.SharePointUtilities;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.ReportGenerator
{
    public class ControladorRG : IReportGenerator
    {


        public List<DTItem> ReportDesvWorkPackage(string urlSite) 
        {
            ISharePoint sp = new SharePoint2003();
            List<DTItem> workPackageList = sp.GetWorkPackages(urlSite,"");
            return workPackageList;
        }
        public List<DTItem> IssuesReport(string urlSite) 
        {
            ISharePoint sp = new SharePoint2003();
            List<DTItem> issuesReportList = sp.GetIssues(urlSite,"");
            return issuesReportList;
        }
        
    }
}
