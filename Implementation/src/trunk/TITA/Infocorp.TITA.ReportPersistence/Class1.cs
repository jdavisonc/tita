using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.ReportPersistence
{
    class Class1
    {
        void toto() 
        {
            IFabricControllerReportPersistence oIfce = new FabricControllerReportPersistence();
            IReportPersistenceCSV oIRP= oIfce.GetIReportPersistenceCSV();
            //oIRP.ReportDesvWorkPackageToCSV(
        }
    }
}
