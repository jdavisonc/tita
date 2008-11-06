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
            try
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


                        DTWorkPackageReport dataWorkPackage = new DTWorkPackageReport(site, id.ToString(), title, desviation.ToString());
                        workPackageDesviation.Add(dataWorkPackage);
                    }

                }


                return workPackageDesviation;
            }
            catch (Exception e)
            {
                throw new Exception("La comunicación con sharepoint ha fallado");
            }
        }
        public List<DTReportedItem> IssuesReport(string contractId, DateTime initialDate, DateTime finalDate)
        {
            try
            {
                ISharePoint sp = new SharePoint2003();
                String format = "yyyy-MM-ddThh:mm:ssZ";
                DataBaseAccess.DataBaseAccess db = new DataBaseAccess.DataBaseAccess();
                String str = initialDate.ToString(format);
                String str2 = finalDate.ToString(format);
                if (contractId != null)
                {
                    List<DTItem> issueList = sp.GetIssues(contractId, "");
                    List<DTField> issueDataFileds = sp.GetFieldsIssue(contractId);
                    List<string> categories = null;
                    List<string> status = null;
                    foreach (var data in issueDataFileds)
                    {

                        if (data.Name.Equals("Category"))
                        {
                            categories = ((DTFieldChoice)data).Choices;
                        }
                        else
                            if (data.Name.Equals("Status"))
                            {
                                status = ((DTFieldChoice)data).Choices;
                            }
                    }
                    //Armo la combinacion categoria-estado            
                    List<DTReportItemFileds> stateCategory = new List<DTReportItemFileds>();
                    List<DTReportedItem> listReportedItems = new List<DTReportedItem>();
                    foreach (var category in categories)
                    {
                        foreach (var state in status)
                        {
                            DTReportItemFileds dataStateCategory = new DTReportItemFileds(category, state);
                            stateCategory.Add(dataStateCategory);
                        }
                    }

                    List<DTIssueReport> issuesListReport = new List<DTIssueReport>();
                    foreach (var dataPair in stateCategory)
                    {
                        foreach (var issues in issueList)
                        {
                            List<DTField> fields = issues.Fields;
                            bool isValid = true;
                            int id = 0;
                            string categoryIssue = null;
                            string stateIssue = null;
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
                                    if (fieldsOfIncident.Name.Equals("Category"))
                                    {
                                        categoryIssue = ((DTFieldChoice)fieldsOfIncident).Value;
                                    }
                                    else
                                        if (fieldsOfIncident.Name.Equals("Status"))
                                        {
                                            stateIssue = ((DTFieldChoice)fieldsOfIncident).Value;
                                        }

                                }
                                if (categoryIssue.Equals(dataPair.GetCategory()) && stateIssue.Equals(dataPair.GetStatus()))
                                {
                                    dataPair.AddReportFounded();
                                }

                            }

                        }
                        DTReportedItem reportedItem = new DTReportedItem(dataPair.GetCategory(), dataPair.GetStatus(), dataPair.GetCount());
                        listReportedItems.Add(reportedItem);
                    }
                    return listReportedItems;
                }
                else
                {
                    List<DTContract> contracts = db.ContractList();
                    List<DTReportedItem> listReportedItems = new List<DTReportedItem>();
                    List<DTReportItemFileds> stateCategory = new List<DTReportItemFileds>();
                    foreach (var contract in contracts)
                    {
                        List<DTItem> issueList = sp.GetIssues(contract.ContractId, "");
                        List<DTField> issueDataFileds = sp.GetFieldsIssue(contract.ContractId);
                        List<string> categories = null;
                        List<string> status = null;
                        foreach (var data in issueDataFileds)
                        {

                            if (data.Name.Equals("Category"))
                            {
                                categories = ((DTFieldChoice)data).Choices;
                            }
                            else
                                if (data.Name.Equals("Status"))
                                {
                                    status = ((DTFieldChoice)data).Choices;
                                }
                        }
                        //Armo la combinacion categoria-estado            

                        foreach (var category in categories)
                        {
                            foreach (var state in status)
                            {
                                DTReportItemFileds dataStateCategory = new DTReportItemFileds(category, state);
                                if (!stateCategory.Contains(dataStateCategory))
                                    stateCategory.Add(dataStateCategory);
                            }
                        }
                        //}//primer contract


                        foreach (var dataPair in stateCategory)
                        {
                            /*foreach (var contractAux in contracts)
                            {*/





                            List<DTIssueReport> issuesListReport = new List<DTIssueReport>();

                            foreach (var issues in issueList)
                            {
                                List<DTField> fields = issues.Fields;
                                bool isValid = true;
                                int id = 0;
                                string categoryIssue = null;
                                string stateIssue = null;
                                foreach (var fieldsIncident in fields)
                                {
                                    if (fieldsIncident.Name.Equals("Category"))
                                    {
                                        categoryIssue = ((DTFieldChoice)fieldsIncident).Value;
                                    }
                                    else
                                        if (fieldsIncident.Name.Equals("Status"))
                                        {
                                            stateIssue = ((DTFieldChoice)fieldsIncident).Value;
                                        }
                                        else
                                            if (fieldsIncident.Name.Equals("Due Date"))
                                                if ((((DTFieldAtomicDateTime)fieldsIncident).Value > finalDate) || ((DTFieldAtomicDateTime)fieldsIncident).Value < initialDate)
                                                {
                                                    isValid = false;
                                                    break;
                                                }
                                }
                                if (isValid)
                                {
                                    /*foreach (var fieldsOfIncident in fields)
                                    {*/


                                    //}
                                    if (categoryIssue.Equals(dataPair.GetCategory()) && stateIssue.Equals(dataPair.GetStatus()))
                                    {
                                        dataPair.AddReportFounded();
                                    }

                                }

                            }


                        }
                    }
                    foreach (var pair in stateCategory)
                    {
                        DTReportedItem reportedItem = new DTReportedItem(pair.GetCategory(), pair.GetStatus(), pair.GetCount());
                        listReportedItems.Add(reportedItem);
                    }

                    return listReportedItems;
                }

            }
            catch (Exception e)
            {
                throw new Exception("La comunicación con sharepoint ha fallado");
            }
        }
     }

}