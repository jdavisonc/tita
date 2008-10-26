using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;
using System.Xml;
using System.IO;

namespace Infocorp.TITA.OutlookSharePoint
{
    public class OutlookSharePoint2003WS : IOutlookSharePoint
    {
        private const string _wsListsSuf = "/_vti_bin/Lists.asmx";
        private const string _wsUserGroupSuf = "/_vti_bin/UserGroup.asmx";

        #region IOutlookSharePoint Members

        public bool AddIssue(string urlSite, string listName, DTItem issue)
        {
            string innerText = string.Empty;
            innerText = "<Batch><Method ID='1' Cmd='New'>";
            List<DTField> fieldCollection = issue.Fields;
            foreach (DTField field in fieldCollection)
            {
                if (!(field.IsReadOnly || field.Hidden))
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
                    foreach (DTAttachment attch in issue.Attachments)
                    {
                        listsWS.AddAttachment(listName, listItemID, attch.Name, attch.Data);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("UpdateListItem: " + e.Message);
            }
        }

        public List<DTField> GetFieldsIssue(string urlSite, string listName)
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
                                        fieldsCollection.Add(new DTFieldAtomicNumber(name, internalName, required, hidden, isReadOnly));
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
                                            lookupField, lookupList));
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
                throw new Exception("Error en GetFieldsIssue: " + e.Message);
            }
        }

        #endregion

        #region Auxiliar Methods

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
