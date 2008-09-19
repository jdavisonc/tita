using System;
using System.Collections.Generic;
using Infocorp.TITA.DataTypes;
using Microsoft.SharePoint;
using System.Collections.Specialized;

namespace Infocorp.TITA.SharePointUtilities
{
    public class SharePoint2003:ISharePoint
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

        public bool AddIssue(string urlSite, DTIssue issue)
        {
            try
            {
                using (SPSite site = new SPSite(urlSite))
                {
                    site.AllowUnsafeUpdates = true;
                    using (SPWeb web = site.OpenWeb())
                    {
                        web.AllowUnsafeUpdates = true;
                        SPList list = web.Lists["Issues"];
                        SPListItem listItem = list.Items.Add();
                        UpdateListItem(web, listItem, issue);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteIssue(string urlSite, int IDIssue)
        {
            try
            {
                using (SPSite site = new SPSite(urlSite))
                {
                    site.AllowUnsafeUpdates = true;
                    using (SPWeb web = site.OpenWeb())
                    {
                        web.AllowUnsafeUpdates = true;
                        SPList list = web.Lists["Issues"];
                        list.Items.DeleteItemById(IDIssue);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateIssue(string urlSite, DTIssue issue)
        {
            try
            {
                using (SPSite site = new SPSite(urlSite))
                {
                    site.AllowUnsafeUpdates = true;
                    using (SPWeb web = site.OpenWeb())
                    {
                        web.AllowUnsafeUpdates = true;
                        SPList list = web.Lists["Issues"];
                        int IDIssue = int.Parse(issue.GetField("ID").Value.ToString());
                        SPListItem listItem = list.Items.GetItemById(IDIssue);
                        UpdateListItem(web, listItem, issue);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region Auxiliar Methods

        private void UpdateListItem(SPWeb web,SPListItem listItem, DTIssue issue)
        {
            List<DTField> fieldCollection = issue.Fields;
            foreach (DTField field in fieldCollection)
            {
                if (field.Type == DTField.Types.DateTime)
                {
                    if (field.Value.CompareTo("") != 0)
                        listItem[field.Name] = DateTime.Parse(field.Value);
                }
                else if (field.Type == DTField.Types.User)
                {
                    SPUserCollection userCollection = web.AllUsers;
                    bool stop = false;
                    int i = 0;
                    while (!stop && i < userCollection.Count)
                    {
                        if (userCollection[i].Name.CompareTo(field.Value) == 0)
                        {
                            listItem[field.Name] = string.Format("{0};#{1}", userCollection[i].ID.ToString(), userCollection[i].Name);
                            stop = true;
                        }
                        i++;
                    }
                }
                else if (field.Type != DTField.Types.Counter)
                {
                    listItem[field.Name] = field.Value;
                }
            }
            SPAttachmentCollection listItemAttachmentCollection = listItem.Attachments;
            List<DTAttachment> attachmentCollection = issue.Attachments;
            bool condition;
            int j;
            foreach (var attachment in attachmentCollection)
            {
                condition = false;
                j = 0;
                while (!condition && j < listItemAttachmentCollection.Count)
                {
                    if (listItemAttachmentCollection[j].CompareTo(attachment.Name) == 0)
                        condition = true;
                    j++;
                }
                if (!condition && j == listItemAttachmentCollection.Count)
                {
                    listItemAttachmentCollection.Add(attachment.Name, attachment.Data);
                }
            }
            listItem.Update();
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
                        fieldsCollection.Add(new DTField(name, DTField.Types.Counter, required, choices));
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

        #endregion
    }
}
