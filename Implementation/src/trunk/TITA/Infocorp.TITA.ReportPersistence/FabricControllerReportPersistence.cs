using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.ReportPersistence
{
    public class FabricControllerReportPersistence:IFabricControllerReportPersistence
    {
        #region IFabricControllerReportPersistence Members

        public IReportPersistenceCSV GetIReportPersistenceCSV()
        {
            ControllerReportPersistenceCSV oController = new ControllerReportPersistenceCSV();
            return oController.GetReportPersistenceInstanceCSV();
        }

        #endregion
    }
}
