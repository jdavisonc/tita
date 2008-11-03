using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;
using System.Xml;
using System.IO;
using System.Globalization;

namespace Infocorp.TITA.SharePointUtilities
{
    public class SharePoint2003WS : ISharePoint
    {
        private DataBaseAccess.DataBaseAccess _dbAccess = null;
        private const string _wsListsSuf = "/_vti_bin/Lists.asmx";
        private const string _wsUserGroupSuf = "/_vti_bin/UserGroup.asmx";

        public SharePoint2003WS()
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

        public System.Globalization.CultureInfo GetSiteLocale(string idContract)
        {
            throw new NotImplementedException();
        }

        public List<string> GetLists(string idContract)
        {
            try
            {
                List<string> listCollection = new List<string>();
                DTContract dtContract = _dbAccess.GetContract(idContract);
                XmlNode xmlNode;
                using (ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists())
                {
                    listsWS.Url = CheckCorrectUrlFromat(dtContract.Site) + _wsListsSuf;
                    listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    xmlNode = listsWS.GetListCollection();
                }
                foreach (XmlNode childNode in xmlNode)
                {
                    if ((childNode.Attributes != null) && (childNode.Attributes.GetNamedItem("Title") != null))
                    {
                        listCollection.Add(childNode.Attributes.GetNamedItem("Title").Value);
                    }
                }
                return listCollection;
            }
            catch (Exception e)
            {
                throw new Exception("GetList: " + e.Message);
            }
        }

        public List<DTRol> GetPermissions(string idContract, string username)
        {
            try
            {
                List<DTRol> rolCollection = new List<DTRol>();
                DTContract dtContract = _dbAccess.GetContract(idContract);
                XmlNode node;
                using (UserGroupWebServiceReference.UserGroup userGroup = new UserGroupWebServiceReference.UserGroup())
                {
                    userGroup.Url = CheckCorrectUrlFromat(dtContract.Site) + _wsUserGroupSuf;
                    userGroup.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    node = userGroup.GetAllUserCollectionFromWeb();
                }
                string loginName = username;
                foreach (XmlNode xmlNodeChild in node.ChildNodes)
                {
                    foreach (XmlNode userNode in xmlNodeChild)
                    {
                        if ((userNode.Attributes != null) && 
                            (userNode.Attributes.GetNamedItem("Name") != null) &&
                            (userNode.Attributes.GetNamedItem("Name").Value.CompareTo(username) == 0))
                        {
                            loginName = userNode.Attributes.GetNamedItem("LoginName").Value;
                            break;
                        }
                    }
                }
                XmlNode xmlNode;
                using (UserGroupWebServiceReference.UserGroup userGroup = new UserGroupWebServiceReference.UserGroup())
                {
                    userGroup.Url = CheckCorrectUrlFromat(dtContract.Site) + _wsUserGroupSuf;
                    userGroup.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    xmlNode = userGroup.GetRoleCollectionFromUser(loginName);
                }
                foreach (XmlNode xmlNodeChild in xmlNode.ChildNodes)
                {
                    foreach (XmlNode userNode in xmlNodeChild)
                    {
                        if (userNode.Attributes != null)
                        {
                            string name = userNode.Attributes.GetNamedItem("Name").Value;
                            string type = userNode.Attributes.GetNamedItem("Type").Value;
                            
                            switch (type)
                            {
                                case "0":
                                    rolCollection.Add(new DTRol(name, string.Empty, DTRol.RolType.None));
                                    break;
                                case "1":
                                    rolCollection.Add(new DTRol(name, string.Empty, DTRol.RolType.Guest));
                                    break;
                                case "2":
                                    rolCollection.Add(new DTRol(name, string.Empty, DTRol.RolType.Reader));
                                    break;
                                case "3":
                                    rolCollection.Add(new DTRol(name, string.Empty, DTRol.RolType.Contributor));
                                    break;
                                case "4":
                                    rolCollection.Add(new DTRol(name, string.Empty, DTRol.RolType.WebDesigner));
                                    break;
                                case "5":
                                    rolCollection.Add(new DTRol(name, string.Empty, DTRol.RolType.Administrator));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                }
                return rolCollection;
            }
            catch (Exception e)
            {
                throw new Exception("GetPermissions: " + e.Message);
            }
        }

        public string GetCurrentUserEmail(string idContract)
        {
            DTContract dtContract = _dbAccess.GetContract(idContract);
            XmlNode node;
            using (UserGroupWebServiceReference.UserGroup userGroup = new UserGroupWebServiceReference.UserGroup())
            {
                userGroup.Url = CheckCorrectUrlFromat(dtContract.Site) + _wsUserGroupSuf;
                userGroup.Credentials = System.Net.CredentialCache.DefaultCredentials;
                node = userGroup.GetAllUserCollectionFromWeb();
            }
            string username = System.Net.CredentialCache.DefaultNetworkCredentials.UserName;
            string email = string.Empty;
            foreach (XmlNode xmlNodeChild in node.ChildNodes)
            {
                foreach (XmlNode userNode in xmlNodeChild)
                {
                    if ((userNode.Attributes != null) &&
                        (userNode.Attributes.GetNamedItem("Name") != null) &&
                        (userNode.Attributes.GetNamedItem("Name").Value.CompareTo(username) == 0))
                    {
                        email = userNode.Attributes.GetNamedItem("EMail").Value;
                        break;
                    }
                }
            }
            return email;
        }

        #endregion

        #region Auxiliar Method

        void SiteMapPropertyValue(string urlSite, string listName, string property, string initialValue, string endValue)
        {
            try
            {
                List<DTField> fieldsList = GetFieldsListItem(urlSite, listName);
                DTField dTFieldProp = null;
                foreach (DTField dTField in fieldsList)
                {
                    if (dTField.Name.CompareTo(property) == 0)
                    {
                        dTFieldProp = dTField;
                        break;
                    }
                }
                if (dTFieldProp != null)
                {
                    string updateXml = "<Batch>";
                    int idCmd = 1;
                    string innerXml = "<ViewFields>";
                    innerXml += "<FieldRef Name='" + dTFieldProp.InternalName + "' />";
                    innerXml += "<FieldRef Name='ID' />";
                    innerXml += "</ViewFields>";
                    XmlNode xmlListItems;
                    using (ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists())
                    {
                        listsWS.Url = CheckCorrectUrlFromat(urlSite) + _wsListsSuf;
                        listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        xmlListItems = listsWS.GetListItems(listName, string.Empty, null, StringToXmlNode(innerXml), string.Empty, null);
                    }
                    foreach (XmlNode nodeWP in xmlListItems.ChildNodes)
                    {
                        foreach (XmlNode item in nodeWP.ChildNodes)
                        {
                            if (item.Attributes != null)
                            {
                                if ((item.Attributes.GetNamedItem("ows_" + dTFieldProp.InternalName) != null) &&
                                    (item.Attributes.GetNamedItem("ows_" + dTFieldProp.InternalName).Value.CompareTo(initialValue) == 0))
                                { 
                                    updateXml += "<Method ID='" + (idCmd++).ToString() + "' Cmd='Update'>";
                                    updateXml += "<Field Name='" + dTFieldProp.InternalName + "'>" + endValue + "</Field>";
                                    updateXml += "<Field Name='ID'>" + item.Attributes.GetNamedItem("ows_ID").Value + "</Field>";
                                    updateXml += "</Method>";
                                }

                            }
                        }
                    }
                    updateXml += "</Batch>";
                    using (ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists())
                    {
                        listsWS.Url = CheckCorrectUrlFromat(urlSite) + _wsListsSuf;
                        listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        listsWS.UpdateListItems(listName, StringToXmlNode(updateXml));
                    }
                }
            }
            catch (Exception e)
            {
                
                throw new Exception("SiteMapPropertyValue: " + e.Message);
            }
        }

        private bool UpdateListItem(string urlSite, string listName, DTItem item, bool isUpdate)
        {
            string operation = "Update";
            string innerText = string.Empty;
            if (!isUpdate)
            {
                operation = "New";
            }

            innerText = "<Batch><Method ID='1' Cmd='" + operation + "'>";
            List<DTField> fieldCollection = item.Fields;
            foreach (DTField field in fieldCollection)
            {
                if ((field.InternalName.CompareTo("ID") == 0) && (isUpdate))
                {
                    innerText += "<Field Name='" + field.InternalName + "'>";
                    innerText += ((DTFieldCounter)field).Value;
                    innerText += "</Field>";
                }
                else if (!(field.IsReadOnly || field.Hidden))
                {
                    #region switch
                    switch (field.GetCustomType())
                    {
                        case DTField.Types.Number:
                            innerText += "<Field Name='" + field.InternalName + "'>";
                            innerText += ((DTFieldAtomicNumber)field).Value.ToString();
                            innerText += "</Field>";
                            break;
                        case DTField.Types.String:
                            innerText += "<Field Name='" + field.InternalName + "'>";
                            innerText += ((DTFieldAtomicString)field).Value;
                            innerText += "</Field>";
                            break;
                        case DTField.Types.Choice:
                            innerText += "<Field Name='" + field.InternalName + "'>";
                            innerText += ((DTFieldChoice)field).Value;
                            innerText += "</Field>";
                            break;
                        case DTField.Types.Boolean:
                            innerText += "<Field Name='" + field.InternalName + "'>";
                            innerText += ((DTFieldAtomicBoolean)field).Value.ToString();
                            innerText += "</Field>";
                            break;
                        case DTField.Types.DateTime:
                            innerText += "<Field Name='" + field.InternalName + "' Type='DateTime'>";
                            innerText += ((DTFieldAtomicDateTime)field).Value.ToString("u");
                            innerText += "</Field>";
                            break;
                        case DTField.Types.Note:
                            if (((DTFieldAtomicNote)field).Value.CompareTo("") != 0)
                            {
                                innerText += "<Field Name='" + field.InternalName + "'>";
                                innerText += ((DTFieldAtomicNote)field).Value;
                                innerText += "</Field>";
                            }
                            break;
                        case DTField.Types.User:
                            innerText += "<Field Name='" + field.InternalName + "'>";
                            innerText += GetUserID(urlSite, ((DTFieldChoiceUser)field).Value);
                            innerText += "</Field>";
                            break;
                        case DTField.Types.Counter:
                            break;
                        case DTField.Types.Lookup:
                            innerText += "<Field Name='" + field.InternalName + "'>";
                            innerText += GetLookupFieldID(urlSite, (DTFieldChoiceLookup)field);
                            innerText += "</Field>";
                            break;
                        case DTField.Types.Default:
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
            }

            innerText += "</Method></Batch>";
            XmlNode xmlNode = StringToXmlNode(innerText);
            try
            {
                using (ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists())
                {
                    listsWS.Url = CheckCorrectUrlFromat(urlSite) + _wsListsSuf;
                    listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    XmlNode result = listsWS.UpdateListItems(listName, xmlNode);
                    string listItemID = string.Empty;
                    foreach (XmlNode resultChild in result.ChildNodes)
                    {
                        foreach (XmlNode resultaChildChild in resultChild.ChildNodes)
                        {
                            if (resultaChildChild.Attributes != null &&
                                resultaChildChild.Attributes.GetNamedItem("ows_ID") != null)
                            {
                                listItemID = resultaChildChild.Attributes.GetNamedItem("ows_ID").Value;
                                break;
                            }
                        }
                    }
                    if (!isUpdate)
                    {
                        foreach (DTAttachment attch in item.Attachments)
                        {
                            listsWS.AddAttachment(listName, listItemID, attch.Name, attch.Data);
                        }
                    }
                    else 
                    {
                        XmlNode xmlNodeAttch = listsWS.GetAttachmentCollection(listName, listItemID);
                        // Proceso nuevos attachments o modificados
                        foreach (DTAttachment attch in item.Attachments)
                        {
                            bool found = false;
                            foreach (XmlNode nodeAttch in xmlNodeAttch)
                            {
                                if (nodeAttch.InnerText.CompareTo(attch.Url) == 0)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found || (found && attch.Data != null && attch.Data.Length > 0))
                            {
                                if (found)
                                {
                                    listsWS.DeleteAttachment(listName, listItemID, attch.Url);
                                }
                                listsWS.AddAttachment(listName, listItemID, attch.Name, attch.Data);
                            }
                        }
                        // Proceso attachments borrados
                        foreach (XmlNode nodeAttch in xmlNodeAttch)
                        {
                            bool found = false;
                            foreach (DTAttachment attch in item.Attachments)
                            {
                                if (nodeAttch.InnerText.CompareTo(attch.Url) == 0)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                listsWS.DeleteAttachment(listName, listItemID, nodeAttch.InnerText);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("UpdateListItem: " + e.Message);
            }
        }

        private bool DeleteListItem(string urlSite, string listName, int idItem)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement batchElement = doc.CreateElement("Batch");
            batchElement.SetAttribute("OnError", "Continue");
            batchElement.InnerXml = "<Method ID='1' Cmd='Delete'>" +
                                        "<Field Name='ID'>" + idItem.ToString() + "</Field>" +
                                    "</Method>";
            try
            {
                using (ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists())
                {
                    listsWS.Url = CheckCorrectUrlFromat(urlSite) + _wsListsSuf;
                    listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    listsWS.UpdateListItems(listName, batchElement);
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("DeleteListItem: " + e.Message);
            }
        }

        private List<DTItem> GetListItems(string urlSite, string listName, string CAMLQuery)
        {
            try
            {
                List<DTField> fieldsCollection = GetFieldsListItem(urlSite, listName);

                List<DTItem> itemsCollection = new List<DTItem>();
                XmlDocument xmlDoc = new System.Xml.XmlDocument();
                XmlNode ndViewFields = xmlDoc.CreateNode(XmlNodeType.Element,"ViewFields","");
                ndViewFields.InnerXml = "";
                foreach (DTField dtField in fieldsCollection)
                {
                    ndViewFields.InnerXml += "<FieldRef Name='" + dtField.InternalName + "' />";
                }
                XmlNode xmlListItems;
                
                using (ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists())
                {
                    listsWS.Url = CheckCorrectUrlFromat(urlSite) + _wsListsSuf;
                    listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    XmlNode camlNode = String.IsNullOrEmpty(CAMLQuery) ? null : StringToXmlNode(CAMLQuery);

                    xmlListItems = listsWS.GetListItems(listName, string.Empty, camlNode, ndViewFields, string.Empty, null);
                }
                foreach (XmlNode nodeWP in xmlListItems.ChildNodes)
                {
                    foreach (XmlNode item in nodeWP.ChildNodes)
                    {
                        if (item.Attributes != null)
                        {
                            int id = 0;
                            List<DTField> newItemFieldCollection = CloneDTFieldCollection(fieldsCollection);
                            foreach (XmlAttribute attr in item.Attributes)
                            {
                                foreach (DTField field in newItemFieldCollection)
	                            {
                            		 if (attr.Name.CompareTo("ows_" + field.InternalName) == 0)
                                     {
                                         switch (field.GetCustomType())
                                         {
                                             case DTField.Types.Number:
                                                 ((DTFieldAtomicNumber)field).Value = double.Parse(attr.Value,CultureInfo.InvariantCulture.NumberFormat);
                                                 break;
                                             case DTField.Types.String:
                                                 ((DTFieldAtomicString)field).Value = attr.Value;
                                                 break;
                                             case DTField.Types.Choice:
                                                 ((DTFieldChoice)field).Value = attr.Value;
                                                 break;
                                             case DTField.Types.Boolean:
                                                 if (Math.Abs(Convert.ToInt32(attr.Value)) == 1)
                                                 {
                                                     ((DTFieldAtomicBoolean)field).Value = true;
                                                 }
                                                 else
                                                 {
                                                     ((DTFieldAtomicBoolean)field).Value = false;
                                                 }
                                                 break;
                                             case DTField.Types.DateTime:
                                                 ((DTFieldAtomicDateTime)field).Value = DateTime.Parse(attr.Value);
                                                 break;
                                             case DTField.Types.Note:
                                                 ((DTFieldAtomicNote)field).Value = attr.Value;
                                                 break;
                                             case DTField.Types.User:
                                                 int index = attr.Value.IndexOf('#');
                                                 ((DTFieldChoiceUser)field).Value = attr.Value.Substring(index+1);
                                                 break;
                                             case DTField.Types.Counter:
                                                 ((DTFieldCounter)field).Value = int.Parse(attr.Value);
                                                 if (attr.Name.CompareTo("ows_ID") == 0)
                                                 {
                                                     id = int.Parse(attr.Value);
                                                 }
                                                 break;
                                             case DTField.Types.Lookup:
                                                 int indexNum = attr.Value.IndexOf('#');
                                                 ((DTFieldChoiceLookup)field).Value = attr.Value.Substring(indexNum + 1);
                                                 break;
                                             case DTField.Types.Default:
                                                 break;
                                             default:
                                                 break;
                                        }
                                        break;
                                    }
	                            }
                            }
                            XmlNode attachmentsCollectionWS;
                            using (ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists())
                            {
                                listsWS.Url = CheckCorrectUrlFromat(urlSite) + _wsListsSuf;
                                listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                                attachmentsCollectionWS = listsWS.GetAttachmentCollection(listName, id.ToString());    
                            }
                            List<DTAttachment> newAttachmentsCollection = new List<DTAttachment>();
                            foreach (XmlNode nodeRootAttch in attachmentsCollectionWS)
	                        {
                        		 foreach (XmlNode nodeAttch in nodeRootAttch)
	                             {
                            	     newAttachmentsCollection.Add(new DTAttachment(nodeAttch.InnerText));
	                             }
	                        }
                            itemsCollection.Add(new DTItem(newItemFieldCollection, newAttachmentsCollection));
                        }
                    }
                }
                return itemsCollection;
            }
            catch (Exception e)
            {
                throw new Exception("GetListItems: " + e.Message);
            }
        }

        private List<DTField> GetFieldsListItem(string urlSite, string listName)
        {
            try
            {
                List<DTField> fieldsCollection = new List<DTField>();
                XmlNode list;
                using (ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists())
                {
                    listsWS.Url = CheckCorrectUrlFromat(urlSite) + _wsListsSuf;
                    listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    list = listsWS.GetList(listName);
                }
                XmlNodeList childNodes = list.ChildNodes;
                foreach (XmlNode fieldsNode in childNodes)
                {
                    if (fieldsNode.Name.CompareTo("Fields") == 0)
                    {
                        foreach (XmlNode fieldNode in fieldsNode.ChildNodes)
                        {
                            string name = string.Empty;
                            string internalName = string.Empty;
                            bool required = false;
                            bool hidden = false;
                            bool isReadOnly = false;
                            bool isDateOnly = false;
                            string lookupList = string.Empty;
                            string lookupField = string.Empty;
                            DTField.Types type = DTField.Types.Default;

                            foreach (XmlAttribute attr in fieldNode.Attributes)
                            {
                                #region if
                                if (attr.Name.CompareTo("DisplayName") == 0)
                                {
                                    name = attr.Value;
                                }
                                else if (attr.Name.CompareTo("Name") == 0)
                                {
                                    internalName = attr.Value;
                                }
                                else if (attr.Name.CompareTo("Required") == 0)
                                {
                                    required = bool.Parse(attr.Value);
                                }
                                else if (attr.Name.CompareTo("Hidden") == 0)
                                {
                                    hidden = bool.Parse(attr.Value);
                                }
                                else if (attr.Name.CompareTo("ReadOnly") == 0)
                                {
                                    isReadOnly = bool.Parse(attr.Value);
                                }
                                else if (attr.Name.CompareTo("List") == 0)
                                {
                                    lookupList = attr.Value;
                                }
                                else if (attr.Name.CompareTo("ShowField") == 0)
                                {
                                    lookupField = attr.Value;
                                }
                                else if (attr.Name.CompareTo("Format") == 0)
                                {
                                    if (attr.Value.CompareTo("DateOnly") == 0)
                                    {
                                        isDateOnly = true;
                                    }
                                }
                                else if (attr.Name.CompareTo("Type") == 0)
                                {
                                    if (attr.Value.CompareTo("Choice") == 0)
                                    {
                                        type = DTField.Types.Choice;
                                    }
                                    else if (attr.Value.CompareTo("Number") == 0)
                                    {
                                        type = DTField.Types.Number;
                                    }
                                    else if (attr.Value.CompareTo("Text") == 0)
                                    {
                                        type = DTField.Types.String;
                                    }
                                    else if (attr.Value.CompareTo("Note") == 0)
                                    {
                                        type = DTField.Types.Note;
                                    }
                                    else if (attr.Value.CompareTo("Lookup") == 0)
                                    {
                                        type = DTField.Types.Lookup;
                                    }
                                    else if (attr.Value.CompareTo("DateTime") == 0)
                                    {
                                        type = DTField.Types.DateTime;
                                    }
                                    else if (attr.Value.CompareTo("Counter") == 0)
                                    {
                                        type = DTField.Types.Counter;
                                    }
                                    else if (attr.Value.CompareTo("Boolean") == 0)
                                    {
                                        type = DTField.Types.Boolean;
                                    }
                                    else if (attr.Value.CompareTo("User") == 0)
                                    {
                                        type = DTField.Types.User;
                                    }
                                }
                                #endregion
                            }

                            if (name.CompareTo(string.Empty) != 0)
                            {
                                #region switch
                                switch (type)
                                {
                                    case DTField.Types.Number:
                                        fieldsCollection.Add(new DTFieldAtomicNumber(name,internalName,required,hidden,isReadOnly));
                                        break;
                                    case DTField.Types.String:
                                        fieldsCollection.Add(new DTFieldAtomicString(name, internalName, required, hidden, isReadOnly));
                                        break;
                                    case DTField.Types.Choice:
                                        List<string> choiceCollection = new List<string>();
                                        foreach (XmlNode xmlNodeChoices in fieldNode.ChildNodes)
                                        {
                                            if (xmlNodeChoices.Name.CompareTo("CHOICES") == 0)
                                            {
                                                foreach (XmlNode xmlNodeChoiceCustom in xmlNodeChoices.ChildNodes)
                                                {
                                                    choiceCollection.Add(xmlNodeChoiceCustom.InnerText);
                                                }
                                                break;
                                            }
                                        }
                                        fieldsCollection.Add(new DTFieldChoice(name, internalName, required, hidden, isDateOnly, choiceCollection));
                                        break;
                                    case DTField.Types.Boolean:
                                        fieldsCollection.Add(new DTFieldAtomicBoolean(name, internalName, required, hidden, isReadOnly));
                                        break;
                                    case DTField.Types.DateTime:
                                        fieldsCollection.Add(new DTFieldAtomicDateTime(name, internalName, required, hidden, isReadOnly, isDateOnly));
                                        break;
                                    case DTField.Types.Note:
                                        fieldsCollection.Add(new DTFieldAtomicNote(name, internalName, required, hidden, isReadOnly));
                                        break;
                                    case DTField.Types.User:
                                        List<string> users = GetUsers(urlSite);
                                        fieldsCollection.Add(new DTFieldChoiceUser(name, internalName, required, hidden, isReadOnly, users));
                                        break;
                                    case DTField.Types.Counter:
                                        fieldsCollection.Add(new DTFieldCounter(name, internalName, required, hidden, isReadOnly));
                                        break;
                                    case DTField.Types.Lookup:
                                        List<string> choicesFromLookupList = GetChoicesFromList(urlSite, lookupList, lookupField);
                                        fieldsCollection.Add(new DTFieldChoiceLookup(name, internalName, required, hidden, isReadOnly, choicesFromLookupList,
                                            lookupField,lookupList));
                                        break;
                                    case DTField.Types.Default:
                                        break;
                                    default:
                                        break;
                                }
                                #endregion
                            }
                        }
                        break;
                    }
                }
                return fieldsCollection;
            }
            catch (Exception e)
            {
                throw new Exception("Error en GetFieldsListItem: " + e.Message);
            }
        }

        private List<string> GetChoicesFromList(string urlSite, string listId, string fieldName)
        {
            List<string> lookupChoicesCollection = new List<string>();
            XmlNode listItems;
            using (ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists())
            {
                listsWS.Url = CheckCorrectUrlFromat(urlSite) + _wsListsSuf;
                listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                listItems = listsWS.GetListItems(listId, string.Empty, null, null, string.Empty, null);
            }
            foreach (XmlNode nodeWP in listItems.ChildNodes)
            {
                foreach (XmlNode item in nodeWP.ChildNodes)
                {
                    if (item.Attributes != null)
                    {
                        foreach (XmlAttribute attr in item.Attributes)
                        {
                            if (attr.Name.CompareTo("ows_" + fieldName) == 0)
                            {
                                lookupChoicesCollection.Add(attr.Value);
                                break;
                            }
                        }
                    }
                }
            }
            return lookupChoicesCollection;
        }

        private List<string> GetUsers(string urlSite)
        {
            try
            {
                List<string> usersCollection = new List<string>();
                XmlNode xmlNode;
                using (UserGroupWebServiceReference.UserGroup userGroup = new UserGroupWebServiceReference.UserGroup())
                {
                    userGroup.Url = CheckCorrectUrlFromat(urlSite) + _wsUserGroupSuf;
                    userGroup.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    xmlNode = userGroup.GetAllUserCollectionFromWeb();
                }
                foreach (XmlNode xmlNodeChild in xmlNode.ChildNodes)
                {
                    foreach (XmlNode userNode in xmlNodeChild)
                    {
                        if (userNode.Attributes != null)
                        {
                            usersCollection.Add(userNode.Attributes.GetNamedItem("Name").Value);
                        }
                    }

                }
                return usersCollection;
            }
            catch (Exception e)
            {
                throw new Exception("GetUsers: " + e.Message);
            }
        }

        private List<DTField> CloneDTFieldCollection(List<DTField> dtFieldCollection)
        {
            List<DTField> cloneCollection = new List<DTField>();
            foreach (DTField dtField in dtFieldCollection)
            {
                switch (dtField.GetCustomType())
	            {
		            case DTField.Types.Number:
                        cloneCollection.Add(new DTFieldAtomicNumber((DTFieldAtomicNumber)dtField));
                        break;
                    case DTField.Types.String:
                        cloneCollection.Add(new DTFieldAtomicString((DTFieldAtomicString)dtField));
                        break;
                    case DTField.Types.Choice:
                         cloneCollection.Add(new DTFieldChoice((DTFieldChoice)dtField));
                         break;
                    case DTField.Types.Boolean:
                         cloneCollection.Add(new DTFieldAtomicBoolean((DTFieldAtomicBoolean)dtField));
                         break;
                    case DTField.Types.DateTime:
                         cloneCollection.Add(new DTFieldAtomicDateTime((DTFieldAtomicDateTime)dtField));
                         break;
                    case DTField.Types.Note:
                         cloneCollection.Add(new DTFieldAtomicNote((DTFieldAtomicNote)dtField));
                         break;
                    case DTField.Types.User:
                         cloneCollection.Add(new DTFieldChoiceUser((DTFieldChoiceUser)dtField));
                         break;
                    case DTField.Types.Counter:
                         cloneCollection.Add(new DTFieldCounter((DTFieldCounter)dtField));
                         break;
                    case DTField.Types.Lookup:
                         cloneCollection.Add(new DTFieldChoiceLookup((DTFieldChoiceLookup)dtField));
                         break;
                    case DTField.Types.Default:
                        break;
                    default:
                        break;
	            }
            }
            return cloneCollection;
        }

        private string GetLookupFieldID(string urlSite, DTFieldChoiceLookup dTFieldChoiceLookup)
        {
            try
            {
                XmlNode listItems;
                using (ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists())
                {
                    listsWS.Url = CheckCorrectUrlFromat(urlSite) + _wsListsSuf;
                    listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    listItems = listsWS.GetListItems(dTFieldChoiceLookup.LookupList, string.Empty, null, null, string.Empty, null);
                }
                string id = string.Empty;
                foreach (XmlNode nodeWP in listItems.ChildNodes)
                {
                    foreach (XmlNode item in nodeWP.ChildNodes)
                    {
                        if (item.Attributes != null)
                        {
                            if (item.Attributes.GetNamedItem("ows_" + dTFieldChoiceLookup.LookupField).Value.CompareTo(dTFieldChoiceLookup.Value) == 0)
                            {
                                id = item.Attributes.GetNamedItem("ows_ID").Value;
                                break;
                            }
                        }
                    }
                }
                return id;
            }
            catch (Exception e)
            {
                throw new Exception("GetLookupFieldID: " + e.Message);
            }
        }

        private string GetUserID(string urlSite, string username)
        {
            try
            {
                string value = string.Empty;
                XmlNode xmlNode;
                using (UserGroupWebServiceReference.UserGroup userGroup = new UserGroupWebServiceReference.UserGroup())
                {
                    userGroup.Url = CheckCorrectUrlFromat(urlSite) + _wsUserGroupSuf;
                    userGroup.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    xmlNode = userGroup.GetAllUserCollectionFromWeb();
                }
                foreach (XmlNode xmlNodeChild in xmlNode.ChildNodes)
                {
                    foreach (XmlNode userNode in xmlNodeChild)
                    {
                        if (userNode.Attributes != null)
                        {
                            if (userNode.Attributes.GetNamedItem("Name").Value.CompareTo(username) == 0)
                            {
                                value = userNode.Attributes.GetNamedItem("ID").Value;
                                break;
                            }
                        }
                    }

                }
                return value;
            }
            catch (Exception e)
            {
                throw new Exception("GetUserID: " + e.Message);
            }
        }

        private static XmlNode StringToXmlNode(string str)
        {
            XmlTextReader xmlReader = new XmlTextReader(new StringReader(str));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);
            return (XmlNode)xmlDocument;
        }

        private string CheckCorrectUrlFromat(string urlSite)
        {
            if (urlSite[urlSite.Length - 1].CompareTo('/') == 0)
            {
                return urlSite.Substring(0, urlSite.Length - 1);
            }
            return urlSite;
        }

        #endregion
    }
}
