using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;
using System.IO;

using System.Web.UI.HtmlControls;
using System.Web;
//using System.Web.Util;



namespace Infocorp.TITA.ReportPersistence
{
    class ReportPersistenceCSV:IReportPersistenceCSV
    {
        private string _EXTENSION = ".csv";
        private string _FIELD_SEPARATOR = ",";
        
        
        #region IReportPersistenceCSV Members

        public void ReportDesvWorkPackageToCSV(List<DTWorkPackageReport> reportData)
        {

            string reportName = "desvWorkPackage" + DateTime.Now.ToString("yyyyMMddHHmm") + this._EXTENSION;
            StringBuilder strb = new StringBuilder();
            foreach (DTWorkPackageReport item in reportData)
            {
                this.WriteUserInfo(item, ref strb);
            }
            this.CreateReportToCVS(reportName, StrToByteArray(strb.ToString()));
                
        }

        private void WriteUserInfo(DTWorkPackageReport item, ref StringBuilder strb)
        {
            AddComma(item.Site, strb);
            AddComma(item.IdWorkPackage, strb);
            AddComma(item.Title, strb);
            AddLastFieldElement(item.Desviation, strb);
        }

        public void IssuesReportToCSV(List<DTReportedItem> reportData)
        {
            string reportName = "reporteIncidentes" + DateTime.Now.ToString("yyyyMMddHHmm") + this._EXTENSION;
            StringBuilder strb = new StringBuilder();
            foreach (DTReportedItem item in reportData)
                {
                    
                    this.WriteUserInfo(item, ref strb);
                }
                this.CreateReportToCVS(reportName, StrToByteArray(strb.ToString()));
                
        }

        private void WriteUserInfo(DTReportedItem item, ref StringBuilder strb)
        {


            AddComma(item.Category, strb);
            AddComma(item.Status, strb);
            AddLastFieldElement(item.Count.ToString(), strb);
        }

        #endregion

        // C# to convert a string to a byte array.
        public static byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        void CreateReportToCVS(string filename, byte[] bytesData)
        {
            try
            {
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + filename);
                HttpContext.Current.Response.ContentType = "text/csv";
                HttpContext.Current.Response.BinaryWrite(bytesData);
                HttpContext.Current.Response.End();
            }
            catch (Exception)
            {
            }
            
        }

        
        
        private void AddComma(string item, StringBuilder strb)
        {
            strb.Append(item.Replace(_FIELD_SEPARATOR, " "));
            strb.Append(_FIELD_SEPARATOR);
        }

        private void AddLastFieldElement(string item, StringBuilder strb)
        {
            strb.Append(item.Replace(_FIELD_SEPARATOR, " "));
            strb.AppendLine();
        }


        #region WriteColumnName ejemplo
        private static void WriteColumnName()
        {
            string str = "Name, Family, Age, Salary";
            HttpContext.Current.Response.Write(str);
            HttpContext.Current.Response.Write(Environment.NewLine);
        }
        #endregion

    }
}
