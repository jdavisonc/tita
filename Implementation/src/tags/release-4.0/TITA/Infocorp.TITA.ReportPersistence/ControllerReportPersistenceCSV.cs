using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.ReportPersistence
{
    class ControllerReportPersistenceCSV
    {
        public IReportPersistenceCSV GetReportPersistenceInstanceCSV()
        {
            ReportPersistenceCSV oReportInstance = new ReportPersistenceCSV();
            return (IReportPersistenceCSV)oReportInstance;
        }
    }
}
