using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using Infocorp.TITA.DataTypes;

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


    [WebMethod]
    public List<DTIssue> GetIssues()
    {
        //List<DTIssue> lst = GetIssues();
        //return lst;
        DTField DTF = new DTField("EMILIA",0,true,new List<string>());
        List<DTField> fields = new List<DTField>();
        fields.Add(DTF);
        DTIssue dt = new DTIssue(fields, new List<DTAttachment>());
            
        List<DTIssue> lst = new List<DTIssue>();
        lst.Add(dt);
        return lst;
    }

}

