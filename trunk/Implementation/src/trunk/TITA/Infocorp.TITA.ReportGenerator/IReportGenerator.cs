using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.SharePointUtilities;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.ReportGenerator
{
    public interface IReportGenerator
    {
        List<DTItem> ReportDesvWorkPackage(string urlSite);
        List<DTItem> IssuesReport(string urlSite);
    }
}
