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

    #region Issue
    [WebMethod]
    public List<DTIssue> GetWPS()
    {
        // retorna una lista de incidentes... igual creo q es lo mismo xq dtissue es generico
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetIssues("http://localhost/infocorp");
    }

    [WebMethod]
    public List<DTIssue> GetIssues()
    {

        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetIssues("http://localhost/infocorp");
    }

    [WebMethod]
    public void AddIssue(DTIssue issue)
    {

        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.AddNewIssue(issue);
    }

    [WebMethod]
    public DTIssue GetIssueTemplate()
    {

        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        return witInstance.GetIssueTemplate("http://localhost/infocorp");
    }

    [WebMethod]
    public void ModifyIssue(DTIssue issue)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.ModifyIssue(issue);
    }

    [WebMethod]
    public void DeleteIssue(int id)
    {
        IWITServices witInstance = WITFactory.Instance().WITServicesInstance();
        witInstance.DeleteIssue(id);
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
        try
        {
            witInstance.GetContracts();
        }
        catch (Exception exc)
        {
            string s = exc.Message;
        }

        return new List<DTContract>();
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

}
