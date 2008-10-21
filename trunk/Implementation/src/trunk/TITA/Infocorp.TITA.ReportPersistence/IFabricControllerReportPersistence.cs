using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Infocorp.TITA.ReportPersistence
{
    interface IFabricControllerReportPersistence
    {
        IReportPersistenceCSV GetReportPersistenceCSV();
    }
}
