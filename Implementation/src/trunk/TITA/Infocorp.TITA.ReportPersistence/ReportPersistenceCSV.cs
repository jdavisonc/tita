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

            string reportName = "desvWorkPackage" + DateTime.Now.ToString("yyyyMMddHHmmss") + this._EXTENSION;
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

        public void IssuesReportToCSV(List<DTIssueReport> reportData)
        {
            string reportName = "reporteIncidentes" + DateTime.Now.ToString("yyyyMMddHHmmss") + this._EXTENSION;
            //DateTime
            StringBuilder strb = new StringBuilder();
                foreach (DTIssueReport item in reportData)
                {
                    
                    this.WriteUserInfo(item, ref strb);
                }
                this.CreateReportToCVS(reportName, StrToByteArray(strb.ToString()));
                
        }

        private void WriteUserInfo(DTIssueReport item, ref StringBuilder strb)
        {


            AddComma(item.IdIssue, strb);
            AddComma(item.Site, strb);
            AddComma(item.Title, strb);
            AddLastFieldElement(item.WorkPackage, strb);
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
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + filename);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.BinaryWrite(bytesData);
            HttpContext.Current.Response.End();
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

        #region WriteToCSV ejemplo
        /*
        public static void WriteToCSV(List<Person> personList)
        {
            
            string attachment = "attachment; filename=PerosnList.csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");

            WriteColumnName();

            foreach (Person item in personList)
            {
                WriteUserInfo(item);
            }

            HttpContext.Current.Response.End();
        }
        */
        #endregion




    }
}
