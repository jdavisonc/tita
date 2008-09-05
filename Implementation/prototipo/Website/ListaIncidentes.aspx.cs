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
        gvIncidentes.DataSource = Utilities.GetIssues();
        gvIncidentes.DataBind();
    }
    protected void gvIncidentes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) 
        {
            Label l = e.Row.FindControl("lblTitle") as Label;
            l.Text = (e.Row.DataItem as IssueListItem).Title;


        }
    }
}
