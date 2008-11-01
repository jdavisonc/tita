using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using Infocorp.TITA.DataTypes;
using Infocorp.TITA.WITLogic;
using Infocorp.TITA.ReportGenerator;
using Infocorp.TITA.ReportPersistence;

/// <summary>
/// Summary description for WSTita
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WSTita : System.Web.Services.WebService
{

    public WSTita()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Extras

    [WebMethod]
    public void ExtendedDTS(DTFieldAtomicString str, DTFieldAtomicBoolean bl, DTFieldAtomicNumber num,
        DTFieldAtomicDateTime time, DTFieldAtomicNote note, DTFieldCounter counter, DTFieldChoiceUser ch, DTFieldChoiceLookup look)
    {
        str = new DTFieldAtomicString();
        bl = new DTFieldAtomicBoolean ();
        num = new DTFieldAtomicNumber();
        time = new DTFieldAtomicDateTime();
        note = new DTFieldAtomicNote();
        counter = new DTFieldCounter();
        ch = new DTFieldChoiceUser();
        look = new DTFieldChoiceLookup();
    }

    #endregion

    #region Issue
    [WebMethod]
    public List<DTItem> GetIssues(string idContract)
    {

        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetIssues(idContract);
    }

    [WebMethod]
    public void AddIssue(DTItem issue, string url)
    {

        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.AddIssue(issue,url);
    }

    [WebMethod]
    public DTItem GetIssueTemplate(string idContract)
    {

        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetIssueTemplate(idContract);
    }

    [WebMethod]
    public void ModifyIssue(DTItem issue, string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.ModifyIssue(issue,url);
    }

    [WebMethod]
    public void DeleteIssue(int id, string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.DeleteIssue(id,url);
    }
    #endregion

    #region Contract
    [WebMethod]
    public bool IsContractAvailable(string contractId)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.IsContractAvailable(contractId);
    }

    [WebMethod]
    public void AddNewContract(DTContract contract)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.AddNewContract(contract);
    }

    [WebMethod]
    public void DeleteContract(string contractId)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.DeleteContract(contractId);
    }

    [WebMethod]
    public void ChangeCurrentContract(int contractId)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.ChangeCurrentContract(contractId);
    }

    [WebMethod]
    public List<DTContract> GetContracts()
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetContracts();
    }

    [WebMethod]
    public string GetContractSite(string contractId)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetContractSite(contractId);
    }

    [WebMethod]
    public void ModifyContract(DTContract contract)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.ModifyContract(contract);
    }
    #endregion

    #region WP

    [WebMethod]
    public List<DTItem> GetWorkPackages(string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetWorkPackages(url);
    }

    [WebMethod]
    public DTItem GetWorkPackageTemplate(string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetWorkPackageTemplate(url);
    }

    [WebMethod]
    public void AddWorkPackage(DTItem wps,string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.AddWorkPackage(wps, url);
    }

    [WebMethod]
    public void DeleteWorkPackage(int wpId, string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.DeleteWorkPackage(wpId,url);
    }

    [WebMethod]
    public void ModifyWP(DTItem wp, string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.UpdateWorkPackage(wp, url);
    }


    #endregion

    #region Task

    [WebMethod]
    public List<DTItem> GetTasks(string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetTasks(url);
    }

    [WebMethod]
    public DTItem GetTaskTemplate(string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetTaskTemplate(url);
    }

    [WebMethod]
    public void AddTask(DTItem tsk, string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.AddTask(tsk, url);
    }

    [WebMethod]
    public void DeleteTask(int tskId, string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.DeleteTask(tskId, url);
    }

    [WebMethod]
    public void ModifyTask(DTItem tsk, string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.UpdateTask(tsk, url);
    }

    #endregion

    #region Reports

    [WebMethod]
    public List<DTWorkPackageReport> ReportDesvWorkPackage(String idContract, DateTime fch_inicial, DateTime fch_final)
    {
        IReportGenerator reportInstance = FactoryReport.GetInstance().GetIReportGenerator();
        return reportInstance.ReportDesvWorkPackage(idContract, fch_inicial, fch_final);
    }

    [WebMethod]
    public List<DTReportedItem> IssuesReport(String idContract, DateTime fch_inicial, DateTime fch_final)
    {
        IReportGenerator reportInstance = FactoryReport.GetInstance().GetIReportGenerator();
        return reportInstance.IssuesReport(idContract, fch_inicial, fch_final);
    }

    [WebMethod]
    public void ExportDesWP(List<DTWorkPackageReport> lst)
    {
        IFabricControllerReportPersistence frp = new FabricControllerReportPersistence();
        IReportPersistenceCSV irp= frp.GetReportPersistenceCSV();
        irp.ReportDesvWorkPackageToCSV(lst);
    }

    [WebMethod]
    public void ExportISSUES(List<DTReportedItem> lst)
    {
        IFabricControllerReportPersistence frp = new FabricControllerReportPersistence();
        IReportPersistenceCSV irp = frp.GetReportPersistenceCSV();
        //irp.IssuesReportToCSV(lst);
    }

    #endregion

    #region Apply

    [WebMethod]
    public void ApplyChanges(string url, ItemType itemType)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.ApplyChanges(url, itemType);
    }

    [WebMethod]
    public bool HasPendingChanges(string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.HasPendingChanges(url);
    }

    #endregion

}
