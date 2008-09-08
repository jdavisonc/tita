using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using SharepointUtilities;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld(string texto)
    {
        IssueListItem issue = Utilities.GetIssue("6");
        return texto + issue.Title; 
    }

    [WebMethod]
    public IssueListItem[] GetIssues()
    {
        IssueListItem[] lst = Utilities.GetIssues().RowData.ListItems;
        return lst;
    }

    [WebMethod]
    public string UpdateIssue(IssueListItem issue)
    {
        Utilities.UpdateIssue(issue);
        return "";
    }

    [WebMethod]
    public string AddIssue(IssueListItem issue)
    {
        Utilities.AddIssue(issue);
        return "";
    }

    [WebMethod]
    public string DeleteIssue(string id)
    {
        Utilities.DeleteIssue(id);
        return "";
    }
}

