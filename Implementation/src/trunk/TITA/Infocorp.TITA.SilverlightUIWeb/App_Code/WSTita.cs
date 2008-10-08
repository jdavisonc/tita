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
    public List<DTItem> GetWPS()
    {
        // retorna una lista de incidentes... igual creo q es lo mismo xq dtissue es generico
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetIssues("http://localhost/infocorp");
    }

    [WebMethod]
    public List<DTItem> GetIssues()
    {

        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetIssues("http://localhost/infocorp");
    }

    [WebMethod]
    public void AddIssue(DTItem issue, string url)
    {

        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.AddIssue(issue,url);
    }

    [WebMethod]
    public DTItem GetIssueTemplate()
    {

        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetIssueTemplate("http://localhost/infocorp");
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


    #region Apply

    [WebMethod]
    public void ApplyChanges(string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.ApplyChanges(url);
    }

    [WebMethod]
    public bool HasPendingChanges(string url)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.HasPendingChanges(url);
    }

    #endregion


}
