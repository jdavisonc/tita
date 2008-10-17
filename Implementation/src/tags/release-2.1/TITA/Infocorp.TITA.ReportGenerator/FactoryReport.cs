using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.ReportGenerator
{
    public class FactoryReport
    {
        private static IReportGenerator _instance = null;
        public static IReportGenerator GetInstance()
        {
            if (_instance == null)
                return _instance = new ControladorRG();
            return _instance;
        }
    }
}
