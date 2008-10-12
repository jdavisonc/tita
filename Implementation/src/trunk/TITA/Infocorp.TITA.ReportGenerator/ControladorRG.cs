using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.SharePointUtilities;
using Infocorp.TITA.DataTypes;
using Infocorp.TITA.DataBaseAccess;


namespace Infocorp.TITA.ReportGenerator
{
    public class ControladorRG : IReportGenerator
    {


        public List<DTWorkPackageReport> ReportDesvWorkPackage(string contractId, DateTime initialDate, DateTime finalDate) 
        {
            ISharePoint sp = new SharePoint2003();
            String format = "yyyy-MM-ddThh:mm:ssZ";

            DataBaseAccess.DataBaseAccess db = new DataBaseAccess.DataBaseAccess();
            string site = db.ContractSite(contractId).Trim(); 
            // Converts the local DateTime to a string 
            // using the custom format string and display.
            String str = initialDate.ToString(format);
            String str2 = finalDate.ToString(format);
            
            /*string caml = "<Query>"+
   "<Where>"+
      "<And>"+
         "<Lt>"+
            "<FieldRef Name=\"End_x0020_Date\" />"+
            "<Value Type=\"DateTime\">"+str2+"</Value>"+
         "</Lt>"+
         "<Gt>"+
            "<FieldRef Name=\"End_x0020_Date\" />"+
            "<Value Type=\"DateTime\">"+str+"</Value>"+
         "</Gt>"+
      "</And>"+
   "</Where>"+
"</Query>";*/
        
   string caml = @"<Query>
   <Where><Lt><FieldRef Name='End_x0020_Date' /><Value Type='DateTime'>2009-12-25T12:00:00Z</Value></Lt></Where></Query>";
            List<DTItem> workPackageList = sp.GetWorkPackages(contractId, caml);
            List<DTWorkPackageReport> workPackageDesviation = new List<DTWorkPackageReport>();
            foreach (var workPackage in workPackageList)
            {
                
                List<DTField> listFields = workPackage.Fields;
                DateTime init = new DateTime();
                DateTime final = new DateTime();
                int id = 0;
                string title = "";
                bool isValid = true;
                foreach (var fields in listFields)
                {
                    if (fields.Name.Equals("End Date"))
                        if ((((DTFieldAtomicDateTime)fields).Value > finalDate) || ((DTFieldAtomicDateTime)fields).Value < initialDate)
                        {
                            isValid = false;
                            break;
                        }
                }
                if (isValid)    
                {    
                    foreach (var fields in listFields)
                    {
                           
                        if (fields.Name.Equals("ID"))
                        {
                            id = ((DTFieldCounter)fields).Value;
                        }
                        else
                            if (fields.Name.Equals("Title"))
                            {
                               title = ((DTFieldAtomicString)fields).Value;
                            }
                            else
                                if (fields.Name.Equals("End Date"))
                                {
                                    init = ((DTFieldAtomicDateTime)fields).Value;
                                }
                                else
                                    if (fields.Name.Equals("Proposed End Date"))
                                    {
                                        final = ((DTFieldAtomicDateTime)fields).Value;
                                        
                                    }
                    }
                    String formatDesviation = "ddd-hh:mm:ss";
                    var desviation = (init - final).Days.ToString(); 
                    
                    
                    DTWorkPackageReport dataWorkPackage = new DTWorkPackageReport(site,id.ToString(),title,desviation.ToString());
                    workPackageDesviation.Add(dataWorkPackage);
                }
            
                }


            return workPackageDesviation;
        }
        public List<DTItem> IssuesReport(string urlSite) 
        {
            ISharePoint sp = new SharePoint2003();
            List<DTItem> issuesReportList = sp.GetIssues(urlSite,"");
            return issuesReportList;
        }
        
    }
}
