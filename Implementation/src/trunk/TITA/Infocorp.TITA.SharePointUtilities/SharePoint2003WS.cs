﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;
using System.Xml;
using System.IO;

namespace Infocorp.TITA.SharePointUtilities
{
    public class SharePoint2003WS : ISharePoint
    {
        private DataBaseAccess.DataBaseAccess _dbAccess = null;

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

        public List<string> GetLists(string urlSite)
        {
            throw new NotImplementedException();
        }

        public List<DTRol> GetPermissions(string idContract, string username)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Auxiliar Method

        void SiteMapPropertyValue(string urlSite, string listName, string property, string initialValue, string endValue)
        { 
        
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
                if ((field.Name.CompareTo("ID") == 0) && (isUpdate))
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
                            innerText += ((DTFieldAtomicDateTime)field).Value.ToString("yyyy-MM-dd HH:mm:ss");
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
                            innerText += GetUserID(((DTFieldChoiceUser)field).Value);
                            innerText += "</Field>";
                            break;
                        case DTField.Types.Counter:
                            break;
                        case DTField.Types.Lookup:
                            innerText += "<Field Name='" + field.InternalName + "'>";
                            innerText += GetLookupFieldID((DTFieldChoiceLookup)field);
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
                ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists();
                listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                listsWS.UpdateListItems(listName, xmlNode);
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
                ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists();
                listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                listsWS.UpdateListItems(listName, batchElement);
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
                ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists();
                listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                XmlDocument xmlDoc = new System.Xml.XmlDocument();

                XmlNode ndViewFields = xmlDoc.CreateNode(XmlNodeType.Element,"ViewFields","");
                ndViewFields.InnerXml = "";
                foreach (DTField dtField in fieldsCollection)
                {
                    ndViewFields.InnerXml += "<FieldRef Name='" + dtField.InternalName + "' />";
                }
                
                XmlNode xmlListItems = listsWS.GetListItems(listName, string.Empty, null, ndViewFields, string.Empty, null);
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
                                                 ((DTFieldAtomicNumber)field).Value = double.Parse(attr.Value);
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
                            XmlNode attachmentsCollectionWS = listsWS.GetAttachmentCollection(listName, id.ToString());
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
                ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists();
                listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                XmlNode list = listsWS.GetList(listName);
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
                            }

                            if (name.CompareTo(string.Empty) != 0)
                            {
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
                                        List<string> choicesFromLookupList = GetChoicesFromList(listsWS, lookupList, lookupField);
                                        fieldsCollection.Add(new DTFieldChoiceLookup(name, internalName, required, hidden, isReadOnly, choicesFromLookupList,
                                            lookupField,lookupList));
                                        break;
                                    case DTField.Types.Default:
                                        break;
                                    default:
                                        break;
                                }
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

        private List<string> GetChoicesFromList(ListsWebServiceReference.Lists listsWS,string listId, string fieldName)
        {
            List<string> lookupChoicesCollection = new List<string>();
            XmlNode listItems = listsWS.GetListItems(listId, string.Empty, null, null, string.Empty, null);
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
                UserGroupWebServiceReference.UserGroup userGroup = new UserGroupWebServiceReference.UserGroup();
                userGroup.Credentials = System.Net.CredentialCache.DefaultCredentials;
                XmlNode xmlNode = userGroup.GetAllUserCollectionFromWeb();
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

        private string GetLookupFieldID(DTFieldChoiceLookup dTFieldChoiceLookup)
        {
            try
            {
                ListsWebServiceReference.Lists listsWS = new ListsWebServiceReference.Lists();
                listsWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                string id = string.Empty;
                XmlNode listItems = listsWS.GetListItems(dTFieldChoiceLookup.LookupList, string.Empty, null, null, string.Empty, null);
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

        private string GetUserID(string username)
        {
            try
            {
                string value = string.Empty;
                UserGroupWebServiceReference.UserGroup userGroup = new UserGroupWebServiceReference.UserGroup();
                userGroup.Credentials = System.Net.CredentialCache.DefaultCredentials;
                XmlNode xmlNode = userGroup.GetAllUserCollectionFromWeb();
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

        #endregion
    }
}