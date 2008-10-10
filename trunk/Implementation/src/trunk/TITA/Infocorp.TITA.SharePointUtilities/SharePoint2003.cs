﻿using System;
using System.Collections.Generic;
using Infocorp.TITA.DataTypes;
using Microsoft.SharePoint;
using System.Collections.Specialized;
using System.Globalization;

namespace Infocorp.TITA.SharePointUtilities
{
    public class SharePoint2003:ISharePoint
    {
        private const string _listIssues = "Issues";
        private const string _listWorkPackages = "Work Packages";
        private const string _listTasks = "Tasks";

        #region ISharePoint Members

        #region ABM Issues

        public List<DTItem> GetIssues(string urlSite, string CAMLQuery)
        {
            return GetListItems(urlSite, _listIssues, CAMLQuery);
        }

        public List<DTField> GetFieldsIssue(string urlSite)
        {
            return GetFieldsListItem(urlSite, _listIssues);
        }

        public bool AddIssue(string urlSite, DTItem issue)
        {
            return UpdateListItem(urlSite, _listIssues, issue, false);
        }

        public bool DeleteIssue(string urlSite, int idIssue)
        {
            return DeleteListItem(urlSite, _listIssues, idIssue);
        }

        public bool UpdateIssue(string urlSite, DTItem issue)
        {
            return UpdateListItem(urlSite, _listIssues, issue, true);
        }

        #endregion

        #region ABM Work Packages

        public List<DTItem> GetWorkPackages(string urlSite, string CAMLQuery)
        {
            return GetListItems(urlSite, _listWorkPackages, CAMLQuery);
        }

        public List<DTField> GetFieldsWorkPackage(string urlSite)
        {
            return GetFieldsListItem(urlSite, _listWorkPackages);
        }

        public bool AddWorkPackage(string urlSite, DTItem wp)
        {
            return UpdateListItem(urlSite, _listWorkPackages, wp, false);
        }

        public bool DeleteWorkPackage(string urlSite, int idWp)
        {
            return DeleteListItem(urlSite, _listWorkPackages, idWp);
        }

        public bool UpdateWorkPackage(string urlSite, DTItem wp)
        {
            return UpdateListItem(urlSite, _listWorkPackages, wp, true);
        }

        #endregion

        #region ABM Tasks

        public List<DTItem> GetTasks(string urlSite, string CAMLQuery)
        {
            return GetListItems(urlSite, _listTasks, CAMLQuery);
        }

        public List<DTField> GetFieldsTask(string urlSite)
        {
            return GetFieldsListItem(urlSite, _listTasks);
        }

        public bool AddTask(string urlSite, DTItem task)
        {
            return UpdateListItem(urlSite, _listTasks, task, false);
        }

        public bool DeleteTask(string urlSite, int idTask)
        {
            return DeleteListItem(urlSite, _listTasks, idTask);
        }

        public bool UpdateTask(string urlSite, DTItem task)
        {
            return UpdateListItem(urlSite, _listTasks, task, true);
        }

        #endregion

        public CultureInfo GetSiteLocale(string urlSite)
        {
            try
            {
                using (SPSite site = new SPSite(urlSite))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        return web.Locale;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error en GetSiteLocale: " + e.Message);
            }
        }

        #endregion

        #region Auxiliar Methods

        private bool UpdateListItem(string urlSite, string listName, DTItem item, bool isUpdate)
        {
            try
            {
                using (SPSite site = new SPSite(urlSite))
                {
                    site.AllowUnsafeUpdates = true;
                    using (SPWeb web = site.OpenWeb())
                    {
                        web.AllowUnsafeUpdates = true;
                        SPList list = web.Lists[listName];
                        SPListItem listItem;
                        if (isUpdate)
                        {
                            int IDItem = ((DTFieldCounter)item.GetField("ID")).Value;
                            listItem = list.Items.GetItemById(IDItem);
                        }
                        else
                        {
                            listItem = list.Items.Add();
                        }
                        List<DTField> fieldCollection = item.Fields;
                        foreach (DTField field in fieldCollection)
                        {
                            if (!(field.IsReadOnly || field.Hidden))
                            {
                                switch (field.GetCustomType())
                                {
                                    case DTField.Types.Number:
                                        listItem[field.Name] = ((DTFieldAtomicNumber)field).Value.ToString();
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
                                        foreach (SPListItem itemL in itemCollection)
                                        {
                                            if (lookup.Value.CompareTo(itemL[lookup.LookupField].ToString()) == 0)
                                            {
                                                listItem[lookup.Name] = string.Format("{0}", itemL.ID.ToString());
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
                                List<DTAttachment> attachmentCollection = item.Attachments;
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
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error en UpdateListItem: " + e.Message);
            }
        }

        private bool DeleteListItem(string urlSite, string listName, int idItem)
        {
            try
            {
                using (SPSite site = new SPSite(urlSite))
                {
                    site.AllowUnsafeUpdates = true;
                    using (SPWeb web = site.OpenWeb())
                    {
                        web.AllowUnsafeUpdates = true;
                        SPList list = web.Lists[listName];
                        list.Items.DeleteItemById(idItem);
                        list.Update();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error en DeleteListItem: " + e.Message);
            }
        }

        private List<DTItem> GetListItems(string urlSite, string listName, string CAMLQuery)
        {
            SPQuery query = new SPQuery();
            query.Query = CAMLQuery;
            try
            {
                List<DTItem> items = new List<DTItem>();
                using (SPSite site = new SPSite(urlSite))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists[listName];
                        SPListItemCollection listItemCollection = list.GetItems(query);
                        List<DTAttachment> attachmentCollectionIssue;
                        foreach (SPListItem item in listItemCollection)
                        {
                            if (MustProcessItem(item))
                            {
                                List<DTField> fieldCollectionIssue = new List<DTField>(GetFieldsListItem(urlSite, listName));
                                foreach (DTField field in fieldCollectionIssue)
                                {
                                    if (item[field.Name] != null)
                                    {
                                        switch (field.GetCustomType())
                                        {
                                            case DTField.Types.Number:
                                                ((DTFieldAtomicNumber)field).Value = double.Parse(item[field.Name].ToString());
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
                                }

                                attachmentCollectionIssue = new List<DTAttachment>();
                                SPAttachmentCollection attachmentCollection = item.Attachments;
                                foreach (var attachment in attachmentCollection)
                                {
                                    attachmentCollectionIssue.Add(new DTAttachment(attachment.ToString(), attachmentCollection.UrlPrefix + attachment.ToString()));
                                }
                                DTItem issueItem = new DTItem(fieldCollectionIssue, attachmentCollectionIssue);
                                items.Add(issueItem);
                            }
                        }
                    }
                }

                return items;
            }
            catch (Exception e)
            {
                throw new Exception("Error en GetListItems: " + e.Message);
            }
        }

        private bool MustProcessItem(SPListItem item)
        {
            bool result = true;
            if (item.Xml.Contains("IsCurrent"))
            {
                result = Convert.ToBoolean(item["IsCurrent"]);
            }

            return result;
        }

        private List<DTField> GetFieldsListItem(string urlSite, string listName)
        {
            try
            {
                List<DTField> fieldsCollection = new List<DTField>();
                using (SPSite site = new SPSite(urlSite))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists[listName];
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
                                    fieldsCollection.Add(new DTFieldAtomicNumber(name, required, hidden, isReadOnly));
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
                    }
                }
                return fieldsCollection;
            }
            catch (Exception e)
            {
                throw new Exception("Error en GetFieldsListItem: " + e.Message);
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

        #endregion
    }
}
