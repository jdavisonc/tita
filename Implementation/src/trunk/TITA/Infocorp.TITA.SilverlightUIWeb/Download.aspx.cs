using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string data = Request.Form["DownloadData"];
        string fileName = Request.Form["FileName"];

        //Response.Clear();
        //Response.ContentType = "application/octet-stream";
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        //Response.Write(data);
        //Response.Flush();
        //Response.Close();

        Response.ClearHeaders();
        Response.Clear();
        Response.AddHeader("Content-Disposition", "inline; filename=" + fileName);
        Response.ContentType = "text/csv";
        Response.Write(data);
        Response.End();
    }
}