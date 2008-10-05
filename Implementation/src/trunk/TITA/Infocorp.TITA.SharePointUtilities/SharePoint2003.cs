﻿using System;
using System.Collections.Generic;
using Infocorp.TITA.DataTypes;
using Microsoft.SharePoint;
using System.Collections.Specialized;

namespace Infocorp.TITA.SharePointUtilities
{
    public class SharePoint2003:ISharePoint
    {
        #region ISharePoint Members

        public List<DTItem> GetIssues(string urlSite)
        {
            try
            {
                List<DTItem> issues = new List<DTItem>();
                using (SPSite site = new SPSite(urlSite))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists["Issues"];
                        SPListItemCollection listItemCollection = list.Items;
                        List<DTAttachment> attachmentCollectionIssue;
                        foreach (SPListItem item in listItemCollection)
                        {
                            List<DTField> fieldCollectionIssue = new List<DTField>(GetFieldsListItem(web, list));
                            foreach (DTField field in fieldCollectionIssue)
                            {
                                    switch (field.GetCustomType())
                                    {
                                        case DTField.Types.Integer:
                                            ((DTFieldAtomicInteger)field).Value = int.Parse(item[field.Name].ToString());
                                            break;
                                        case DTField.Types.String:
                                            ((DTFieldAtomicString)field).Value = item[field.Name].ToString();
                                            break;
                                        case DTField.Types.Choice:
                                            ((DTFieldChoice)field).Value = item[field.Name].ToString();
                                            break;
                                        case DTField.Types.Boolean:
                                            ((DTFieldAtomicBoolean)field).Value = Boolean.Parse(item[field.Name].ToString());
                                            break;
                                        case DTField.Types.DateTime:
                                            ((DTFieldAtomicDateTime)field).Value = DateTime.Parse(item[field.Name].ToString());
                                            break;
                                        case DTField.Types.Note:
                                            ((DTFieldAtomicNote)field).Value = item[field.Name].ToString();
                                            break;
                                        case DTField.Types.User:
                                            SPUserCollection userCollection = web.AllUsers;
                                            string val = item[field.Name].ToString();
                                            int index = val.IndexOf(';');
                                            int id = int.Parse(val.Substring(0, index));
                                            foreach (SPUser user in userCollection)
                                            {
                                                if (user.ID == id)
                                                {
                                                    ((DTFieldChoiceUser)field).Value = user.Name;
                                                    break;
                                                }
                                            }
                                            break;
                                        case DTField.Types.Counter:
                                            ((DTFieldCounter)field).Value = int.Parse(item[field.Name].ToString());
                                            break;
                                        case DTField.Types.Lookup:
                                            DTFieldChoiceLookup fieldLookup = (DTFieldChoiceLookup)field;
                                            string val1 = item[field.Name].ToString();
                                            int index1 = val1.IndexOf(';');
                                            int id1 = int.Parse(val1.Substring(0, index1));

                                            SPListCollection listCollection = web.Lists;
                                            Guid listGuid = new Guid(fieldLookup.LookupList);
                                            SPList listL = listCollection.GetList(listGuid, false);
                                            SPListItemCollection itemCollection = listL.Items;
                                            foreach (SPListItem itemL in itemCollection)
                                            {
                                                if (itemL.ID == id1)
                                                {
                                                    fieldLookup.Value = itemL[fieldLookup.LookupField].ToString();
                                                    break;
                                                }
                                            }
                                            break;
                                        case DTField.Types.Default:
                                            break;
                                        default:
                                            break;
                                    }
                            }
                            attachmentCollectionIssue = new List<DTAttachment>();
                            SPAttachmentCollection attachmentCollection = item.Attachments;
                            foreach (var attachment in attachmentCollection)
                            {
                                attachmentCollectionIssue.Add(new DTAttachment(attachment.ToString(), attachmentCollection.UrlPrefix + attachment.ToString()));
                            }
                            DTItem issueItem = new DTItem(fieldCollectionIssue, attachmentCollectionIssue);
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
                    fieldsCollection = GetFieldsListItem(web, list);
                }
            }
            return fieldsCollection;
        }

        public bool AddIssue(string urlSite, DTItem issue)
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
                        list.Update();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateIssue(string urlSite, DTItem issue)
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
                        int IDIssue = ((DTFieldCounter)issue.GetField("ID")).Value;
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
            return true;
        }

        #endregion

        #region Auxiliar Methods

        private void UpdateListItem(SPWeb web,SPListItem listItem, DTItem issue)
        {
            List<DTField> fieldCollection = issue.Fields;
            foreach (DTField field in fieldCollection)
            {
                if (!(field.IsReadOnly || field.Hidden))
                {
                    switch (field.GetCustomType())
                    {
                        case DTField.Types.Integer:
                            listItem[field.Name] = ((DTFieldAtomicInteger)field).Value.ToString();
                            break;
                        case DTField.Types.String:
                            listItem[field.Name] = ((DTFieldAtomicString)field).Value;
                            break;
                        case DTField.Types.Choice:
                            listItem[field.Name] = ((DTFieldChoice)field).Value;
                            break;
                        case DTField.Types.Boolean:
                            listItem[field.Name] = ((DTFieldAtomicBoolean)field).Value.ToString();
                            break;
                        case DTField.Types.DateTime:
                            listItem[field.Name] = ((DTFieldAtomicDateTime)field).Value;
                            break;
                        case DTField.Types.Note:
                            listItem[field.Name] = ((DTFieldAtomicNote)field).Value;
                            break;
                        case DTField.Types.User:
                            SPUserCollection userCollection = web.AllUsers;
                            bool stop = false;
                            int i = 0;
                            while (!stop && i < userCollection.Count)
                            {
                                if (userCollection[i].Name.CompareTo(((DTFieldChoiceUser)field).Value) == 0)
                                {
                                    listItem[field.Name] = string.Format("{0};#{1}", userCollection[i].ID.ToString(), userCollection[i].Name);
                                    stop = true;
                                }
                                i++;
                            }
                            break;
                        case DTField.Types.Counter:
                            break;
                        case DTField.Types.Lookup:
                            DTFieldChoiceLookup lookup = (DTFieldChoiceLookup)field;
                            SPListCollection listCollection = web.Lists;
                            Guid listGuid = new Guid(lookup.LookupList);
                            SPList listLookup = listCollection.GetList(listGuid, false);
                            SPListItemCollection itemCollection = listLookup.Items;
                            foreach (SPListItem item in itemCollection)
                            {
                                if (lookup.Value.CompareTo(item[lookup.LookupField].ToString()) == 0)
                                {
                                    listItem[lookup.Name] = string.Format("{0}", item.ID.ToString());
                                    break;
                                }
                            }

                            break;
                        case DTField.Types.Default:
                            break;
                        default:
                            break;
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
                }
            }
            listItem.Update();
        }

        private List<DTField> GetFieldsListItem(SPWeb web, SPList list)
        {
            List<DTField> fieldsCollection = new List<DTField>();
            SPFieldCollection listFieldsCollection = list.Fields;
            foreach (SPField field in listFieldsCollection)
            {
                string name = field.Title;
                bool required = field.Required;
                bool hidden = field.Hidden;
                bool isReadOnly = field.ReadOnlyField;
                SPFieldType type = field.Type;
                List<string> choices;
                switch (type)
                {
                    case SPFieldType.Attachments:
                        break;
                    case SPFieldType.Boolean:
                        fieldsCollection.Add(new DTFieldAtomicBoolean(name, required, hidden, isReadOnly));
                        break;
                    case SPFieldType.Calculated:
                        break;
                    case SPFieldType.Choice:
                        choices = new List<string>();
                        StringCollection choicesCollection = ((SPFieldChoice)field).Choices;
                        foreach (var choice in choicesCollection)
                        {
                            choices.Add(choice);
                        }
                        fieldsCollection.Add(new DTFieldChoice(name, required, hidden, isReadOnly, choices));
                        break;
                    case SPFieldType.Computed:
                        break;
                    case SPFieldType.Counter:
                        fieldsCollection.Add(new DTFieldCounter(name, required, hidden, isReadOnly));
                        break;
                    case SPFieldType.CrossProjectLink:
                        break;
                    case SPFieldType.Currency:
                        break;
                    case SPFieldType.DateTime:
                        bool isDateOnly = false;
                        if (((SPFieldDateTime)field).DisplayFormat == SPDateTimeFieldFormatType.DateOnly)
                        {
                            isDateOnly = true;
                        }
                        fieldsCollection.Add(new DTFieldAtomicDateTime(name, required, hidden, isReadOnly, isDateOnly));
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
                        choices = new List<string>();
                        SPFieldLookup fieldLookup = (SPFieldLookup)field;
                        string lookupField = fieldLookup.LookupField;
                        string lookupList = fieldLookup.LookupList;
                        choices.AddRange(GetChoicesFromList(web, lookupList, lookupField));
                        fieldsCollection.Add(new DTFieldChoiceLookup(name, required, hidden, isReadOnly, choices, lookupField, lookupList));
                        break;
                    case SPFieldType.MaxItems:
                        break;
                    case SPFieldType.ModStat:
                        break;
                    case SPFieldType.MultiChoice:
                        break;
                    case SPFieldType.Note:
                        fieldsCollection.Add(new DTFieldAtomicNote(name, required, hidden, isReadOnly));
                        break;
                    case SPFieldType.Number:
                        fieldsCollection.Add(new DTFieldAtomicInteger(name, required, hidden, isReadOnly));
                        break;
                    case SPFieldType.Recurrence:
                        break;
                    case SPFieldType.Text:
                        fieldsCollection.Add(new DTFieldAtomicString(name, required, hidden, isReadOnly));
                        break;
                    case SPFieldType.Threading:
                        break;
                    case SPFieldType.URL:
                        break;
                    case SPFieldType.User:
                        choices = new List<string>();
                        SPUserCollection userCollection = web.AllUsers;
                        foreach (SPUser user in userCollection)
                        {
                            choices.Add(user.Name);
                        }
                        fieldsCollection.Add(new DTFieldChoiceUser(name, required, hidden, isReadOnly, choices));
                        break;
                    default:
                        break;
                }
            }
            return fieldsCollection;
        }

        #endregion

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
