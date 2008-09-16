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

        public List<DTField> GetFieldsCollection()
        {
            
            List<DTField> fieldsCollection = new List<DTField>();
            /*
            using (SPSite site = new SPSite(SiteUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists["Issues"];
                    SPFieldCollection fields = list.Fields;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string name = fields[i].Title;
                        bool required = fields[i].Required;
                        SPFieldType type = fields[i].Type;
                        StringCollection choices;
                        
                        if (type == SPFieldType.Choice)
                        {
                            choices = ((SPFieldChoice)fields[i]).Choices;
                        }
                        else
                        {
                             choices = new StringCollection();
                        }
                        switch (SPFieldType)
                        {
                            case SPFieldType.Attachments:
                                break;
                            case SPFieldType.Boolean:
                                break;
                            case SPFieldType.Calculated:
                                break;
                            case SPFieldType.Choice:
                                break;
                            case SPFieldType.Computed:
                                break;
                            case SPFieldType.Counter:
                                break;
                            case SPFieldType.CrossProjectLink:
                                break;
                            case SPFieldType.Currency:
                                break;
                            case SPFieldType.DateTime:
                                break;
                            case SPFieldType.Error:
                                break;
                            case SPFieldType.File:
                                break;
                            case SPFieldType.GridChoice:
                                break;
                            case SPFieldType.Guid:
                                break;
                            case SPFieldType.Integer:
                                break;
                            case SPFieldType.Invalid:
                                break;
                            case SPFieldType.Lookup:
                                break;
                            case SPFieldType.MaxItems:
                                break;
                            case SPFieldType.ModStat:
                                break;
                            case SPFieldType.MultiChoice:
                                break;
                            case SPFieldType.Note:
                                break;
                            case SPFieldType.Number:
                                break;
                            case SPFieldType.Recurrence:
                                break;
                            case SPFieldType.Text:
                                break;
                            case SPFieldType.Threading:
                                break;
                            case SPFieldType.URL:
                                break;
                            case SPFieldType.User:
                                break;
                            default:
                                break;
                        }
                        if (type != SPFieldType.Computed)
                        {
                            fieldsCollection.Add(new DTField(name, type,required, choices));
                        }
                    }
                }
            }*/
            return fieldsCollection;
        }

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
                /*List<DTField> list = GetFieldsCollection();
                Webs.Text = "";
                for (int i = 0; i < list.Count; i++)
                {
                    Webs.Text += list[i].Name + "(" + list[i].Type + ")" + "<br>";
                }*/
                IOutlookSharePoint iop = new OutlookSharePoint2003();
                iop.AddIssue("http://localhost/infocorp");
            }

        }
    }
}
