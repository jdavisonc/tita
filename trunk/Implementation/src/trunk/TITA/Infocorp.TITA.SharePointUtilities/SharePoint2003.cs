using System;
using System.Collections.Generic;
using Infocorp.TITA.DataTypes;
using Microsoft.SharePoint;
using System.Collections.Specialized;

namespace Infocorp.TITA.SharePointUtilities
{
    class SharePoint2003:ISharePoint
    {
        #region ISharePoint Members

        public List<DTIssue> GetIssues(string urlSite)
        {
            try
            {
                List<DTIssue> issues = new List<DTIssue>();
                using (SPSite site = new SPSite(urlSite))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists["Issues"];
                        SPListItemCollection listItemCollection = list.Items;
                        List<DTAttachment> attachmentCollectionIssue;
                        foreach (SPListItem item in listItemCollection)
                        {
                            List<DTField> fieldCollectionIssue = new List<DTField>(GetFieldsListItem(list));
                            foreach (DTField field in fieldCollectionIssue)
                            {
                                if (item[field.Name] != null)
                                {
                                    field.Value = item[field.Name].ToString();
                                }
                                else
                                {
                                    field.Value = string.Empty;
                                }
                            }
                            attachmentCollectionIssue = new List<DTAttachment>();
                            SPAttachmentCollection attachmentCollection = item.Attachments;
                            foreach (var attachment in attachmentCollection)
                            {
                                attachmentCollectionIssue.Add(new DTAttachment(attachment.ToString(), attachmentCollection.UrlPrefix));
                            }
                            DTIssue issueItem = new DTIssue(fieldCollectionIssue, attachmentCollectionIssue);
                            issues.Add(issueItem);
                        }
                    }

                }
                return issues;
            }
            catch (Exception e)
            {
                throw new Exception("Error en GetIssues: " + e.Message);
            }
        }

        private List<DTField> GetFieldsListItem(SPList list)
        {
            List<DTField> fieldsCollection = new List<DTField>();
            SPFieldCollection listFieldsCollection = list.Fields;
            foreach (SPField field in listFieldsCollection)
            {
                string name = field.Title;
                bool required = field.Required;
                SPFieldType type = field.Type;
                List<string> choices = new List<string>();

                if (type == SPFieldType.Choice)
                {
                    StringCollection choicesCollection = ((SPFieldChoice)field).Choices;
                    foreach (var choice in choicesCollection)
                    {
                        choices.Add(choice);
                    }
                }
                else if (type == SPFieldType.User)
                {
                    SPUserCollection userCollection = list.ParentWeb.AllUsers;
                    foreach (SPUser user in userCollection)
                    {
                        choices.Add(user.Name);
                    }
                }
                switch (type)
                {
                    case SPFieldType.Attachments:
                        break;
                    case SPFieldType.Boolean:
                        fieldsCollection.Add(new DTField(name, DTField.Types.Boolean, required, choices));
                        break;
                    case SPFieldType.Calculated:
                        break;
                    case SPFieldType.Choice:
                        fieldsCollection.Add(new DTField(name, DTField.Types.Choice, required, choices));
                        break;
                    case SPFieldType.Computed:
                        break;
                    case SPFieldType.Counter:
                        fieldsCollection.Add(new DTField(name, DTField.Types.Integer, required, choices));
                        break;
                    case SPFieldType.CrossProjectLink:
                        break;
                    case SPFieldType.Currency:
                        break;
                    case SPFieldType.DateTime:
                        fieldsCollection.Add(new DTField(name, DTField.Types.DateTime, required, choices));
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
                        fieldsCollection.Add(new DTField(name, DTField.Types.Note, required, choices));
                        break;
                    case SPFieldType.Number:
                        fieldsCollection.Add(new DTField(name, DTField.Types.Integer, required, choices));
                        break;
                    case SPFieldType.Recurrence:
                        break;
                    case SPFieldType.Text:
                        fieldsCollection.Add(new DTField(name, DTField.Types.String, required, choices));
                        break;
                    case SPFieldType.Threading:
                        break;
                    case SPFieldType.URL:
                        break;
                    case SPFieldType.User:
                        fieldsCollection.Add(new DTField(name, DTField.Types.User, required, choices));
                        break;
                    default:
                        break;
                }
            }
            return fieldsCollection;
        }


        public List<DTField> GetFieldsIssue(string urlSite)
        {
            List<DTField> fieldsCollection = null;
            using (SPSite site = new SPSite(urlSite))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists["Issues"];
                    fieldsCollection = GetFieldsListItem(list);
                }
            }
            return fieldsCollection;
        }

        #endregion
    }
}
