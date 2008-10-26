using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.ReportPersistence
{
    public interface IReportPersistenceCSV
    {
        void ReportDesvWorkPackageToCSV(List<DTWorkPackageReport> reportData);
        void IssuesReportToCSV(List<DTIssueReport> reportData);
    }

}

