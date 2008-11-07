using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.ReportPersistence
{
    public interface IReportPersistenceCSV
    {
        string ReportDesvWorkPackageToCSV(List<DTWorkPackageReport> reportData);
        string IssuesReportToCSV(List<DTReportedItem> reportData);
    }

}

