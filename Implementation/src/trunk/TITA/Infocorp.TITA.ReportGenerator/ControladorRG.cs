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
            String str = initialDate.ToString(format);
            String str2 = finalDate.ToString(format); 
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
        public List<DTIssueReport> IssuesReport(string contractId, DateTime initialDate, DateTime finalDate)
        {
            ISharePoint sp = new SharePoint2003();
            String format = "yyyy-MM-ddThh:mm:ssZ";
            DataBaseAccess.DataBaseAccess db = new DataBaseAccess.DataBaseAccess();
            string site = db.ContractSite(contractId).Trim();
            String str = initialDate.ToString(format);
            String str2 = finalDate.ToString(format);
            List<DTItem> issueList = sp.GetIssues(contractId,"");
            List<DTIssueReport> issuesListReport = new List<DTIssueReport>();
            foreach (var issues in issueList)
            {
                List<DTField> fields = issues.Fields;
                bool isValid = true;
                int id = 0;
                string title = null;
                string workPackage = null;
                foreach (var fieldsIncident in fields)
                {
                    if (fieldsIncident.Name.Equals("Due Date"))
                        if ((((DTFieldAtomicDateTime)fieldsIncident).Value > finalDate) || ((DTFieldAtomicDateTime)fieldsIncident).Value < initialDate)
                        {
                            isValid = false;
                            break;
                        }
                }
                if (isValid)
                {
                    foreach (var fieldsOfIncident in fields)
                    {

                        if (fieldsOfIncident.Name.Equals("ID"))
                        {
                            id = ((DTFieldCounter)fieldsOfIncident).Value;
                        }
                        else
                            if (fieldsOfIncident.Name.Equals("Title"))
                            {
                                title = ((DTFieldAtomicString)fieldsOfIncident).Value;
                            }
                            else
                                if (fieldsOfIncident.Name.Equals("Work Package"))
                                {
                                    workPackage = ((DTFieldChoiceLookup)fieldsOfIncident).Value;
                                }
                    }
                    DTIssueReport dataIssueReport = new DTIssueReport(id.ToString(), title, site, workPackage);
                    issuesListReport.Add(dataIssueReport);
                }
            
            }
            return issuesListReport;                
        }
        public List<DTIssueReport> AllIssuesReport(DateTime initialDate, DateTime finalDate)
        {
            ISharePoint sp = new SharePoint2003();
            String format = "yyyy-MM-ddThh:mm:ssZ";
            DataBaseAccess.DataBaseAccess db = new DataBaseAccess.DataBaseAccess();
            List<DTContract> contracts = db.ContractList();
            List<DTIssueReport> issuesListReport = new List<DTIssueReport>();
            String str = initialDate.ToString(format);
            String str2 = finalDate.ToString(format);
            foreach(var contract in contracts)
            {
                    List<DTItem> issueList = sp.GetIssues(contract.ContractId,"");
                    foreach (var issues in issueList)
                    {
                        List<DTField> fields = issues.Fields;
                        bool isValid = true;
                        int id = 0;
                        string title = null;
                        string workPackage = null;
                        foreach (var fieldsIncident in fields)
                        {
                            if (fieldsIncident.Name.Equals("Due Date"))
                                if ((((DTFieldAtomicDateTime)fieldsIncident).Value > finalDate) || ((DTFieldAtomicDateTime)fieldsIncident).Value < initialDate)
                                {
                                    isValid = false;
                                    break;
                                }
                        }
                        if (isValid)
                        {
                            foreach (var fieldsOfIncident in fields)
                            {

                                if (fieldsOfIncident.Name.Equals("ID"))
                                {
                                    id = ((DTFieldCounter)fieldsOfIncident).Value;
                                }
                                else
                                    if (fieldsOfIncident.Name.Equals("Title"))
                                    {
                                        title = ((DTFieldAtomicString)fieldsOfIncident).Value;
                                    }
                                    else
                                        if (fieldsOfIncident.Name.Equals("Work Package"))
                                        {
                                            workPackage = ((DTFieldChoiceLookup)fieldsOfIncident).Value;
                                        }
                            }
                            DTIssueReport dataIssueReport = new DTIssueReport(id.ToString(), title, contract.Site, workPackage);
                            issuesListReport.Add(dataIssueReport);
                        }
                    
                    }
           } 
           return issuesListReport;                
        }
    }
}
