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

public partial class AgregarIncidente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*Utilities util = new Utilities();
       ListItems<IssueListItem> incidentes =  Utilities.GetIssues();
       IssueListItem[] lista = incidentes.RowData.ListItems;
       for (int i = 0; i < incidentes.RowData.ItemCount; i++) 
           this.DropDownList7.Items.Add(lista[i].Title);*/

        RangeValidator1.MinimumValue = int.MinValue.ToString();
        RangeValidator1.MaximumValue = int.MaxValue.ToString();
        ListItems<IssueListItem> incidente = Utilities.GetIssues();
        IssueListItem[] lista = incidente.RowData.ListItems;
        this.ddlReported.Items.Add("Alejamdro Bouvier");
        this.ddlReported.Items.Add("Phillip Powe");
        this.ddlReported.Items.Add("Abigail Moncrieffe");
        this.ddlPriority.Items.Add("Normal");
        this.ddlPriority.Items.Add("High");
        this.ddlPriority.Items.Add("Low");

        this.ddlCategory.Items.Add("Change");
        this.ddlCategory.Items.Add("Error");
        this.ddlCategory.Items.Add("Query");

        this.ddlStatus.Items.Add("Active");
        this.ddlStatus.Items.Add("Reported");
        this.ddlStatus.Items.Add("Resolved");
        this.ddlStatus.Items.Add("Closed");
        this.ddlStatus.Items.Add("Canceled");
        
        //CARGA DE DATOS
        

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string title = this.txtTitle.Text;
        string description = this.txtDescription.Text;
        string reportedBy = this.ddlReported.SelectedValue;
        string reportedDate = this.calendarReportDate.SelectedDate.ToString("yyyy-MM-dd hh:mm:ss");
        string wp = this.ddlWp.SelectedValue;
        string asignedTo = this.ddlAsignedTo.SelectedValue;
        string priority = this.ddlPriority.SelectedValue;
        string priorityOrder = this.txtPriorityOrder.Text;
        string category = this.ddlCategory.SelectedValue;
        string status = this.ddlStatus.SelectedValue;
        string comments = this.txtComment.Text;
        string dueDate = this.CalendarDueDate.SelectedDate.ToString();
        string resolution = this.txtResolution.Text;
        string addRelatedIssues = this.txtAsignedRelatedIssue.Text;
        
        
        
        
        
        IssueListItem issue = new IssueListItem ();
        issue.Title = title;
        issue.Description = description;
        issue.ReportedBy = reportedBy;
        issue.ReportedDate = reportedDate;
        issue.Priority = priority;
        issue.Ord = int.Parse(priorityOrder);
        issue.Category = category;
        issue.Status = status;
        issue.Resolution = resolution;
        issue.LinkIssueIdNoMenu = int.Parse(addRelatedIssues);



        Utilities.AddIssue(issue);
        this.Page.Response.Redirect("ListaIncidentes.aspx");
        //ListItems<IssueListItem> incidentes = Utilities.GetIssues();    
    
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        this.Page.Response.Redirect("ListaIncidentes.aspx");
    }
    protected void DropDownList1_SelectedIndexChanged(object sender
        , EventArgs e)
    {

    }
    protected void txtAsignedRelatedIssue_TextChanged(object sender, EventArgs e)
    {

    }
}
