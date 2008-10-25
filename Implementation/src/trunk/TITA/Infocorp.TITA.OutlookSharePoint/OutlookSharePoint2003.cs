//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.SharePoint;
//using System.IO;
//using Infocorp.TITA.DataTypes;
//using System.Collections.Specialized;
//using System.Windows.Forms;

//namespace Infocorp.TITA.OutlookSharePoint
//{
//    public class OutlookSharePoint2003: IOutlookSharePoint
//    {
//        private string _listIssues = null;

//        public OutlookSharePoint2003() { }

//        #region IOutlookSharePoint Members

//        public bool AddIssue(string urlSite, DTItem issue)
//        {
//            try
//            {
//                using (SPSite site = new SPSite(urlSite))
//                {
//                    site.AllowUnsafeUpdates = true;
//                    using (SPWeb web = site.OpenWeb())
//                    {
//                        web.AllowUnsafeUpdates = true;
//                        SPList list = web.Lists[_listIssues];
//                        SPListItem listItem = list.Items.Add();
//                        List<DTField> fieldCollection = issue.Fields;

//                        foreach (DTField field in fieldCollection)
//                        {
//                            if (!(field.IsReadOnly || field.Hidden))
//                            {
//                                switch (field.GetCustomType())
//                                {
//                                    case DTField.Types.Number:
//                                        listItem[field.Name] = ((DTFieldAtomicNumber)field).Value.ToString();
//                                        break;
//                                    case DTField.Types.String:
//                                        listItem[field.Name] = ((DTFieldAtomicString)field).Value;
//                                        break;
//                                    case DTField.Types.Choice:
//                                        listItem[field.Name] = ((DTFieldChoice)field).Value;
//                                        break;
//                                    case DTField.Types.Boolean:
//                                        listItem[field.Name] = ((DTFieldAtomicBoolean)field).Value.ToString();
//                                        break;
//                                    case DTField.Types.DateTime:
//                                        listItem[field.Name] = ((DTFieldAtomicDateTime)field).Value;
//                                        break;
//                                    case DTField.Types.Note:
//                                        listItem[field.Name] = ((DTFieldAtomicNote)field).Value;
//                                        break;
//                                    case DTField.Types.User:
//                                        SPUserCollection userCollection = web.AllUsers;
//                                        bool stop = false;
//                                        int i = 0;
//                                        while (!stop && i < userCollection.Count)
//                                        {
//                                            if (userCollection[i].Name.CompareTo(((DTFieldChoiceUser)field).Value) == 0)
//                                            {
//                                                listItem[field.Name] = string.Format("{0};#{1}", userCollection[i].ID.ToString(), userCollection[i].Name);
//                                                stop = true;
//                                            }
//                                            i++;
//                                        }
//                                        break;
//                                    case DTField.Types.Counter:
//                                        break;
//                                    case DTField.Types.Lookup:
//                                        DTFieldChoiceLookup lookup = (DTFieldChoiceLookup)field;
//                                        SPListCollection listCollection = web.Lists;
//                                        Guid listGuid = new Guid(lookup.LookupList);
//                                        SPList listLookup = listCollection.GetList(listGuid, false);
//                                        SPListItemCollection itemCollection = listLookup.Items;
//                                        foreach (SPListItem item in itemCollection)
//                                        {
//                                            if (lookup.Value.CompareTo(item[lookup.LookupField].ToString()) == 0)
//                                            {
//                                                listItem[lookup.Name] = string.Format("{0}", item.ID.ToString());
//                                                break;
//                                            }
//                                        }

//                                        break;
//                                    case DTField.Types.Default:
//                                        break;
//                                    default:
//                                        break;
//                                }
//                            }
//                        }
//                        SPAttachmentCollection listItemAttachmentCollection = listItem.Attachments;
//                        List<DTAttachment> attachmentCollection = issue.Attachments;
//                        foreach (var attachment in attachmentCollection)
//                        {
//                            listItemAttachmentCollection.Add(attachment.Name, attachment.Data);
//                        }
//                        listItem.Update();
//                    }
//                }
//                return true; 
//            }
//            catch (Exception e)
//            {
                
//                MessageBox.Show("No se pudo impactar el incidente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return false;
//            }
//        }

//        //, DTUrl addInConfig
//        public List<DTField> GetFieldsIssue(string urlSite, string nameListIssues)
//        {
//            _listIssues = nameListIssues;
//            List<DTField> fieldsCollection = new List<DTField>();
//            using (SPSite site = new SPSite(urlSite))
//            {
//                using (SPWeb web = site.OpenWeb())
//                {
//                    SPList list = web.Lists[_listIssues];
//                    SPFieldCollection listFieldsCollection = list.Fields;
//                    foreach (SPField field in listFieldsCollection)
//                    {
//                        string name = field.Title;
//                        bool required = field.Required;
//                        bool hidden = field.Hidden;
//                        bool isReadOnly = field.ReadOnlyField;
//                        SPFieldType type = field.Type;
//                        List<string> choices;
//                        if (!(hidden || isReadOnly))
//                        {
//                            switch (type)
//                            {
//                                case SPFieldType.Attachments:
//                                    break;
//                                case SPFieldType.Boolean:
//                                    fieldsCollection.Add(new DTFieldAtomicBoolean(name, required, hidden, isReadOnly));
//                                    break;
//                                case SPFieldType.Calculated:
//                                    break;
//                                case SPFieldType.Choice:
//                                    choices = new List<string>();
//                                    StringCollection choicesCollection = ((SPFieldChoice)field).Choices;
//                                    foreach (var choice in choicesCollection)
//                                    {
//                                        choices.Add(choice);
//                                    }
//                                    fieldsCollection.Add(new DTFieldChoice(name, required, hidden, isReadOnly, choices));
//                                    break;
//                                case SPFieldType.Computed:
//                                    break;
//                                case SPFieldType.Counter:
//                                    fieldsCollection.Add(new DTFieldCounter(name, required, hidden, isReadOnly));
//                                    break;
//                                case SPFieldType.CrossProjectLink:
//                                    break;
//                                case SPFieldType.Currency:
//                                    break;
//                                case SPFieldType.DateTime:
//                                    bool isDateOnly = false;
//                                    if (((SPFieldDateTime)field).DisplayFormat == SPDateTimeFieldFormatType.DateOnly)
//                                    {
//                                        isDateOnly = true;
//                                    }
//                                    fieldsCollection.Add(new DTFieldAtomicDateTime(name, required, hidden, isReadOnly, isDateOnly));
//                                    break;
//                                case SPFieldType.Error:
//                                    break;
//                                case SPFieldType.File:
//                                    break;
//                                case SPFieldType.GridChoice:
//                                    break;
//                                case SPFieldType.Guid:
//                                    break;
//                                case SPFieldType.Integer:
//                                    break;
//                                case SPFieldType.Invalid:
//                                    break;
//                                case SPFieldType.Lookup:
//                                    choices = new List<string>();
//                                    SPFieldLookup fieldLookup = (SPFieldLookup)field;
//                                    string lookupField = fieldLookup.LookupField;
//                                    string lookupList = fieldLookup.LookupList;
//                                    choices.AddRange(GetChoicesFromList(web, lookupList, lookupField));
//                                    fieldsCollection.Add(new DTFieldChoiceLookup(name, required, hidden, isReadOnly, choices, lookupField, lookupList));
//                                    break;
//                                case SPFieldType.MaxItems:
//                                    break;
//                                case SPFieldType.ModStat:
//                                    break;
//                                case SPFieldType.MultiChoice:
//                                    break;
//                                case SPFieldType.Note:
//                                    fieldsCollection.Add(new DTFieldAtomicNote(name, required, hidden, isReadOnly));
//                                    break;
//                                case SPFieldType.Number:
//                                    fieldsCollection.Add(new DTFieldAtomicNumber(name, required, hidden, isReadOnly));
//                                    break;
//                                case SPFieldType.Recurrence:
//                                    break;
//                                case SPFieldType.Text:
//                                    fieldsCollection.Add(new DTFieldAtomicString(name, required, hidden, isReadOnly));
//                                    break;
//                                case SPFieldType.Threading:
//                                    break;
//                                case SPFieldType.URL:
//                                    break;
//                                case SPFieldType.User:
//                                    choices = new List<string>();
//                                    SPUserCollection userCollection = web.AllUsers;
//                                    foreach (SPUser user in userCollection)
//                                    {
//                                        choices.Add(user.Name);
//                                    }
//                                    fieldsCollection.Add(new DTFieldChoiceUser(name, required, hidden, isReadOnly, choices));
//                                    break;
//                                default:
//                                    break;
//                            }
//                        }
//                    }
//                }
//            }
//            return fieldsCollection;
//        }

//        #endregion


//        private List<string> GetChoicesFromList(SPWeb web, string listId, string fieldName)
//        {
//            List<string> listLookupChoices = new List<string>();
//            SPListCollection listCollection = web.Lists;
//            Guid listGuid = new Guid(listId);
//            SPList list = listCollection.GetList(listGuid, false);
//            SPListItemCollection itemCollection = list.Items;
//            foreach (SPListItem item in itemCollection)
//            {
//                listLookupChoices.Add(item[fieldName].ToString());
//            }
//            return listLookupChoices;
//        }
//    }
//}
