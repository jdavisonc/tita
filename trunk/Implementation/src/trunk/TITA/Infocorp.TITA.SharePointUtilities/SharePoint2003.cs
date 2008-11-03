using System;
using System.Collections.Generic;
using Infocorp.TITA.DataTypes;
using Microsoft.SharePoint;
using System.Collections.Specialized;
using System.Globalization;
using Infocorp.TITA.DataBaseAccess;
using System.Collections.ObjectModel;

namespace Infocorp.TITA.SharePointUtilities
{
    public class SharePoint2003:ISharePoint
    {

        private DataBaseAccess.DataBaseAccess _dbAccess = null;

        public SharePoint2003()
        {
            _dbAccess = new DataBaseAccess.DataBaseAccess();
        }

        #region ISharePoint Members

        #region ABM Issues

        public List<DTItem> GetIssues(string idContract, string CAMLQuery)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return GetListItems(dtContract.Site, dtContract.issuesList, CAMLQuery);
        }

        public void SiteMapPropertyValueIssues(string idContract, string property, string initialValue, string endValue)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            SiteMapPropertyValue(dtContract.Site, dtContract.issuesList, property, initialValue, endValue);    
        }

        public List<DTField> GetFieldsIssue(string idContract)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return GetFieldsListItem(dtContract.Site, dtContract.issuesList);
        }

        public bool AddIssue(string idContract, DTItem issue)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return UpdateListItem(dtContract.Site, dtContract.issuesList, issue, false);
        }

        public bool DeleteIssue(string idContract, int idIssue)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return DeleteListItem(dtContract.Site, dtContract.issuesList, idIssue);
        }

        public bool UpdateIssue(string idContract, DTItem issue)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return UpdateListItem(dtContract.Site, dtContract.issuesList, issue, true);
        }

        #endregion

        #region ABM Work Packages

        public List<DTItem> GetWorkPackages(string idContract, string CAMLQuery)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return GetListItems(dtContract.Site, dtContract.workPackageList, CAMLQuery);
        }

        public void SiteMapPropertyValueWorkPackages(string idContract, string property, string initialValue, string endValue)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            SiteMapPropertyValue(dtContract.Site, dtContract.workPackageList, property, initialValue, endValue);
        }

        public List<DTField> GetFieldsWorkPackage(string idContract)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return GetFieldsListItem(dtContract.Site, dtContract.workPackageList);
        }

        public bool AddWorkPackage(string idContract, DTItem wp)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return UpdateListItem(dtContract.Site, dtContract.workPackageList, wp, false);
        }

        public bool DeleteWorkPackage(string idContract, int idWp)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return DeleteListItem(dtContract.Site, dtContract.workPackageList, idWp);
        }

        public bool UpdateWorkPackage(string idContract, DTItem wp)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return UpdateListItem(dtContract.Site, dtContract.workPackageList, wp, true);
        }

        #endregion

        #region ABM Tasks

        public List<DTItem> GetTasks(string idContract, string CAMLQuery)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return GetListItems(dtContract.Site, dtContract.taskList, CAMLQuery);
        }

        public void SiteMapPropertyValueTasks(string idContract, string property, string initialValue, string endValue)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            SiteMapPropertyValue(dtContract.Site, dtContract.taskList, property, initialValue, endValue);
        }

        public List<DTField> GetFieldsTask(string idContract)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return GetFieldsListItem(dtContract.Site, dtContract.taskList);
        }

        public bool AddTask(string idContract, DTItem task)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return UpdateListItem(dtContract.Site, dtContract.taskList, task, false);
        }

        public bool DeleteTask(string idContract, int idTask)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return DeleteListItem(dtContract.Site, dtContract.taskList, idTask);
        }

        public bool UpdateTask(string idContract, DTItem task)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            return UpdateListItem(dtContract.Site, dtContract.taskList, task, true);
        }

        #endregion

        public CultureInfo GetSiteLocale(string idContract)
        {
            try
            {
                DTContract dtContract = _dbAccess.GetContract(idContract);
                using (SPSite site = new SPSite(dtContract.Site))
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

        public List<String> GetLists(string idContract)
        {
            List<String> listCollection = new List<string>();
            try
            {
                DTContract dtContract = _dbAccess.GetContract(idContract);
                using (SPSite site = new SPSite(dtContract.Site))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPListCollection spListCollection = web.Lists;
                        foreach (SPList item in spListCollection)
                        {
                            listCollection.Add(item.Title);
                        }
                    }
                }
                return listCollection;
            }
            catch (Exception e)
            {
                throw new Exception("Error en GetLists: " + e.Message);
            }
        }

        public List<DTRol> GetPermissions(string idContract, string username)
        {
            try
            {
                List<DTRol> dtRolCollection = new List<DTRol>();
                DTContract dtContract = _dbAccess.GetContract(idContract);
                using (SPSite site = new SPSite(dtContract.Site))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPUserCollection spUserCollection = web.AllUsers;
                        foreach (SPUser user in spUserCollection)
                        {
                            if (user.Name.CompareTo(username) == 0)
                            {
                                SPRoleCollection roles = user.Roles;
                                foreach (SPRole rol in roles)
                                {
                                    switch (rol.Type)
	                                {
		                                case SPRoleType.Administrator:
                                            dtRolCollection.Add(new DTRol(rol.Name, rol.PermissionMask.ToString(), DTRol.RolType.Administrator));
                                            break;
                                        case SPRoleType.Contributor:
                                            dtRolCollection.Add(new DTRol(rol.Name, rol.PermissionMask.ToString(), DTRol.RolType.Contributor));
                                            break;
                                        case SPRoleType.Guest:
                                            dtRolCollection.Add(new DTRol(rol.Name, rol.PermissionMask.ToString(), DTRol.RolType.Guest));
                                            break;
                                        case SPRoleType.None:
                                            dtRolCollection.Add(new DTRol(rol.Name, rol.PermissionMask.ToString(), DTRol.RolType.None));
                                            break;
                                        case SPRoleType.Reader:
                                            dtRolCollection.Add(new DTRol(rol.Name, rol.PermissionMask.ToString(), DTRol.RolType.Reader));
                                            break;
                                        case SPRoleType.WebDesigner:
                                            dtRolCollection.Add(new DTRol(rol.Name,rol.PermissionMask.ToString(),DTRol.RolType.WebDesigner));
                                            break;
                                        default:
                                            break;
	                                }
                                }
                                break;
                            }
                        }
                    }
                }
                return dtRolCollection;
            }
            catch (Exception e)
            {
                throw new Exception("Error en GetPermissions: " + e.Message);
            }
        }

        public string GetCurrentUserEmail(string idContract)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Auxiliar Methods

        void SiteMapPropertyValue(string urlSite, string listName, string property, string initialValue, string endValue)
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
                        if (list.Fields.SchemaXml.Contains(property))
                        {
                            SPListItemCollection itemCollection = list.Items;
                            foreach (SPListItem item in itemCollection)
                            {
                                if (MustProcessItem(item) && item[property].ToString().CompareTo(initialValue) == 0)
                                {
                                    item[property] = endValue;
                                    item.Update();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error en SiteMapPropertyValue: " + e.Message);
            }
        }

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
                        #region Foreach DTField
                        foreach (DTField field in fieldCollection)
                        {
                            if (!(field.IsReadOnly || field.Hidden))
                            {
                                #region Switch
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
                                #endregion
                            }
                        }
                        #region Attachments
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
                        #endregion
                        #endregion
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
                            string internalName = field.InternalName;
                            SPFieldType type = field.Type;
                            List<string> choices;
                            switch (type)
                            {
                                case SPFieldType.Attachments:
                                    break;
                                case SPFieldType.Boolean:
                                    fieldsCollection.Add(new DTFieldAtomicBoolean(name, internalName, required, hidden, isReadOnly));
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
                                    fieldsCollection.Add(new DTFieldChoice(name, internalName, required, hidden, isReadOnly, choices));
                                    break;
                                case SPFieldType.Computed:
                                    break;
                                case SPFieldType.Counter:
                                    fieldsCollection.Add(new DTFieldCounter(name, internalName, required, hidden, isReadOnly));
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
                                    fieldsCollection.Add(new DTFieldAtomicDateTime(name, internalName, required, hidden, isReadOnly, isDateOnly));
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
                                    fieldsCollection.Add(new DTFieldChoiceLookup(name, internalName, required, hidden, isReadOnly, choices, lookupField, lookupList));
                                    break;
                                case SPFieldType.MaxItems:
                                    break;
                                case SPFieldType.ModStat:
                                    break;
                                case SPFieldType.MultiChoice:
                                    break;
                                case SPFieldType.Note:
                                    fieldsCollection.Add(new DTFieldAtomicNote(name, internalName, required, hidden, isReadOnly));
                                    break;
                                case SPFieldType.Number:
                                    fieldsCollection.Add(new DTFieldAtomicNumber(name, internalName, required, hidden, isReadOnly));
                                    break;
                                case SPFieldType.Recurrence:
                                    break;
                                case SPFieldType.Text:
                                    fieldsCollection.Add(new DTFieldAtomicString(name, internalName, required, hidden, isReadOnly));
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
                                    fieldsCollection.Add(new DTFieldChoiceUser(name, internalName, required, hidden, isReadOnly, choices));
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
