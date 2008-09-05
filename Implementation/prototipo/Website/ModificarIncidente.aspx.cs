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
using System.Collections.Generic;

public partial class ModificarIncidente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string issueId = Request.QueryString["id"];
            LoadFields(issueId);
           
        }
        RangeValidator1.MinimumValue = int.MinValue.ToString();
        RangeValidator1.MaximumValue = int.MaxValue.ToString();
    }

    protected void LoadFields(string issueId)
    {
        IssueListItem issue = Utilities.GetIssue(issueId);
        
        txtTitulo.Text = issue.Title;
        txtDescripcion.Text = issue.Description;
        dropDownReportado.DataSource = new List<string>() {"Alejandro Bouvier","Phillip Powe","Abigail Moncrieffe"};
        dropDownReportado.DataBind();
        dropDownReportado.SelectedValue = issue.ReportedBy;

        calendar2.SelectedDate = DateTime.Parse(issue.ReportedDate);


        dropDownPrioridad.DataSource = issue.Priority;

        txtOrden.Text = issue.Ord.ToString();

        dropDownCategoria.SelectedValue = issue.Category;
        dropDownEstado.SelectedValue = issue.Status;
        
        txtResolucion.Text = issue.Resolution;
        txtAsociado.Text = issue.LinkIssueIdNoMenu.ToString();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        IssueListItem issue = new IssueListItem();
        issue.ID =Convert.ToInt32(Request.QueryString["id"]);
        issue.Title = txtTitulo.Text;
        issue.Description = txtDescripcion.Text;
        issue.ReportedBy = dropDownReportado.SelectedValue;
        issue.ReportedDate = calendar2.SelectedDate.ToString("yyyy-MM-dd hh:mm:ss");
        issue.Priority = dropDownPrioridad.SelectedValue;
        issue.Ord = Convert.ToInt32(txtOrden.Text);
        issue.Status = dropDownEstado.SelectedValue;
        issue.Resolution = txtResolucion.Text;
        issue.LinkIssueIdNoMenu = Convert.ToInt32(txtAsociado.Text);
        Utilities.UpdateIssue(issue);
        Response.Redirect("ModificarIncidente.aspx?id=" + issue.ID.ToString());
    }
}
