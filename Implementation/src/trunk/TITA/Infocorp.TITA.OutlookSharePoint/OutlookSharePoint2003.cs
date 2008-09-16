using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using System.IO;
using Infocorp.TITA.DataTypes;
using System.Collections.Specialized;

namespace Infocorp.TITA.OutlookSharePoint
{
    public class OutlookSharePoint2003: IOutlookSharePoint
    {
        public OutlookSharePoint2003()
        {
        }

        #region IOutlookSharePoint Members

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
                        List<DTField> fieldCollection = issue.Fields;
                        foreach (var field in fieldCollection)
                        {
                            listItem[field.Name] = field.Value;
                        }
                        SPAttachmentCollection listItemAttachmentCollection = listItem.Attachments;
                        List<DTAttachment> attachmentCollection = issue.Attachments;
                        foreach (var attachment in attachmentCollection)
                        {
                            listItemAttachmentCollection.Add(attachment.Name, attachment.Data);
                        }
                        listItem.Update();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<DTField> GetFieldsIssue(string urlSite)
        {
            List<DTField> fieldsCollection = new List<DTField>();
            using (SPSite site = new SPSite(urlSite))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists["Issues"];
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
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return fieldsCollection;
        }

        #endregion

    }
}
