using System;
using System.Collections;
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
using SharepointUtilities;

public partial class ListaIncidentes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGV();
        }
    }

    private void BindGV()
    {
        gvIncidentes.DataSource = Utilities.GetIssues().RowData.ListItems;
        gvIncidentes.DataBind();
    }

    protected void gvIncidentes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            Utilities.DeleteIssue(e.CommandArgument.ToString());
        }
        else if (e.CommandName == "Select")
        {
            Response.Redirect("ModificarIncidente.aspx?id=" + e.CommandArgument.ToString());
        }
    }
    protected void gvIncidentes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        this.BindGV();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgregarIncidente.aspx");
    }
}
