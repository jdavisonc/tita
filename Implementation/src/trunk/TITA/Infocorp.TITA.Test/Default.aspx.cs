using System;
using System.Collections.Generic;
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
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Infocorp.TITA.DataTypes;
using System.Collections.Specialized;
using Infocorp.TITA.OutlookSharePoint;

namespace TestSharePoint
{
    public partial class _Default : System.Web.UI.Page
    {
        private string SiteUrl = "http://localhost/infocorp";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                #region 
                /*SharePointPermission permission = new SharePointPermission(System.Security.Permissions.PermissionState.Unrestricted);
                permission.ObjectModel = true;
                SPSite site = new SPSite("http://localhost/infocorp");
                SPWeb web = site.OpenWeb();
                SPList listIssues = web.Lists["Issues"];
                SPListItemCollection items = listIssues.Items;
                SPListItem item = items.Add();
                item["Title"] = "holaaa";
                item.Update();
                //Webs.Text = fields.Count.ToString();
                //listIssues.Add("HOLA", "Me llamo Jorge y vengo a flotar",SPListTemplate

                using (SPSite site = new SPSite("http://localhost/infocorp"))
                {
                    site.AllowUnsafeUpdates = true;
                    using (SPWeb web = site.OpenWeb())
                    {
                        web.AllowUnsafeUpdates = true;
                        SPList list = web.Lists["Issues"];
                        SPFieldCollection fields = list.Fields;
                        Webs.Text = "";
                        for (int i = 0; i < fields.Count; i++)
                        {
                            Webs.Text += fields[i].Title;
                            Webs.Text += " (" + fields[i].Type + ")";
                            Webs.Text += "<br>";
                        }
                        
                        SPUserCollection users = web.AllUsers;
                        SPRoleCollection roles = users[0].Roles;
                        Webs.Text = "";
                        for (int i = 0; i < roles.Count; i++)
                        {
                            Webs.Text += roles[i].Name;
                            //Webs.Text += " (" + fields[i].Type + ")";
                            Webs.Text += "<br>";
                        }

                        SPListItem listItem = list.Items.Add();
                        listItem["Title"] = "test2";
                        listItem["Comment"] = "HOLA QUESO";
                        listItem.Update();                    
                    }
                }*/
                #endregion

                IOutlookSharePoint iop = new OutlookSharePoint2003();
                //iop.AddIssue("http://localhost/infocorp");
                /*List<DTField> fieldsCollection = iop.GetFieldsIssue(SiteUrl);
                Webs.Text = "";
                foreach (DTField field in fieldsCollection)
                {
                    Webs.Text += field.Name + "(" + field.Type.ToString() + ")<br>";
                }*/
                List<DTField> fields = new List<DTField>();
                fields.Add(new DTField("Title",DTField.Types.String,false,new List<string>(),"KRONOS 444444"));
                fields.Add(new DTField("Priority",DTField.Types.Integer,false,new List<string>(),"2"));
                fields.Add(new DTField("Category", DTField.Types.Choice, false, new List<string>(), "Change"));
                //fields.Add(new DTField("Assigned To", DTField.Types.Choice, false, new List<string>(), "tita"));
                DTIssue issue = new DTIssue(fields, new List<DTAttachment>());
                if (iop.AddIssue(SiteUrl, issue))
                {
                    Webs.Text = "Bien";
                }
                else
                {
                    Webs.Text = "Mal";
                }
            }

        }
    }
}
