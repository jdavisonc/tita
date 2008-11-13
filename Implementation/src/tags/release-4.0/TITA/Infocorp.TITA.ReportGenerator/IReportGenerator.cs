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
        List<DTWorkPackageReport> ReportDesvWorkPackage(string contractId, DateTime initialDate, DateTime finalDate);
        List<DTReportedItem> IssuesReport(string contractId, DateTime initialDate, DateTime finalDate);
    }
}
