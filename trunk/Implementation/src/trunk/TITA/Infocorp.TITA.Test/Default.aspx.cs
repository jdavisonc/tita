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
using Infocorp.TITA.SharePointUtilities;

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


                //IOutlookSharePoint iop = new OutlookSharePoint2003();
                //iop.AddIssue("http://localhost/infocorp");
                /*List<DTField> fieldsCollection = iop.GetFieldsIssue(SiteUrl);
                Webs.Text = "";
                foreach (DTField _value in fieldsCollection)
                {
                    Webs.Text += _value.Name + "(" + _value.Type.ToString() + ")<br>";
                }*/
                /*List<DTField> fields = new List<DTField>();
                fields.Add(new DTField("Title",DTField.Types.String,false,new List<string>(),"Soy buzzzz"));
                fields.Add(new DTField("Priority",DTField.Types.Integer,false,new List<string>(),"8"));
                fields.Add(new DTField("Category", DTField.Types.Choice, false, new List<string>(), "Error"));
                fields.Add(new DTField("Assigned To", DTField.Types.User, false, new List<string>(), "TITAVM\\administrator"));
                DTIssue issue = new DTIssue(fields, new List<DTAttachment>());
                if (iop.AddIssue(SiteUrl, issue))
                {
                    Webs.Text = "Bien";
                }
                else
                {
                    Webs.Text = "Mal";
                }*/

                /*using (SPSite site = new SPSite("http://localhost/infocorp"))
                {
                    site.AllowUnsafeUpdates = true;
                    using (SPWeb web = site.OpenWeb())
                    {
                        web.AllowUnsafeUpdates = true;
                        SPList list = web.Lists["Issues2"];


                    }
                }*/
                ISharePoint isp = SharePointUtilities.GetInstance().GetISharePoint();
                //List<DTItem> col = isp.GetIssues("http://localhost/infocorp");
                List<DTItem> items = isp.GetIssues(SiteUrl,"");
            }

        }
        private List<string> GetChoicesFromList(SPWeb web, string listId, string fieldName)
        {
            List<string> listLookupChoices = new List<string>();
            SPListCollection listCollection = web.Lists;
            Guid listGuid = new Guid(listId);
            SPList list = listCollection.GetList(listGuid, false);
            SPListItemCollection itemCollection = list.Items;
            foreach (SPListItem item in itemCollection)
            {
                listLookupChoices.Add(item[fieldName].ToString());
            }
            return listLookupChoices;
        }
    }
}
