using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        this.Page.Response.Redirect("PruebaGrillaTestPage.aspx");
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        this.Page.Response.Redirect("formularioInc.aspx");
    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        this.Page.Response.Redirect("modificar.aspx");
    }

    protected void Button12_Click(object sender, EventArgs e)
    {
        this.Page.Response.Redirect("eliminarIncidente.aspx");
    }

}
