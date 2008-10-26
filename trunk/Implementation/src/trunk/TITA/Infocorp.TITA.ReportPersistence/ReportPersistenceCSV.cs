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
            string reportName = "desvWorkPackage" + DateTime.Now.TimeOfDay.ToString()+ this._EXTENSION;
            StringBuilder strb = new StringBuilder();
            try
            {
                foreach (DTWorkPackageReport item in reportData)
                {
                    this.WriteUserInfo(item, ref strb);
                }
                this.CreateReportToCVS(reportName, StrToByteArray(strb.ToString()));
                HttpContext.Current.Response.Close();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void WriteUserInfo(DTWorkPackageReport item, ref StringBuilder strb)
        {


            AddComma(item.Site, strb);
            AddComma(item.IdWorkPackage, strb);
            AddComma(item.Title, strb);
            AddComma(item.Desviation, strb);
            strb.AppendLine();

            /*
            HttpContext.Current.Response.Write(strb.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
            */
        }

        public void IssuesReportToCSV(List<DTIssueReport> reportData)
        {
            string reportName = "reporteIncidentes" + DateTime.Now.TimeOfDay.ToString() + this._EXTENSION;
            StringBuilder strb = new StringBuilder();
            try
            {
                foreach (DTIssueReport item in reportData)
                {
                    this.WriteUserInfo(item, ref strb);
                }
                this.CreateReportToCVS(reportName, StrToByteArray(strb.ToString()));
                HttpContext.Current.Response.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void WriteUserInfo(DTIssueReport item, ref StringBuilder strb)
        {


            AddComma(item.IdIssue, strb);
            AddComma(item.Site, strb);
            AddComma(item.Title, strb);
            AddComma(item.WorkPackage, strb);
            strb.AppendLine();

            /*
            HttpContext.Current.Response.Write(strb.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
            */
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
            strb.Append(" "+_FIELD_SEPARATOR);
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
