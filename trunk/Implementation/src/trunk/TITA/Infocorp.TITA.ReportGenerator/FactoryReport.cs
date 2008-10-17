using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.ReportGenerator
{
    public class FactoryReport
    {
        private static  FactoryReport _instance = null;
        private static IReportGenerator _instanceIReportGenerator = null;
        public static FactoryReport GetInstance()
        {
            if (_instance == null)
                return _instance = new FactoryReport();
            return _instance;
        }
        public IReportGenerator GetIReportGenerator()
        {
            if (_instanceIReportGenerator == null)
                _instanceIReportGenerator = new ControladorRG();
            return _instanceIReportGenerator;
        }
    }
}
