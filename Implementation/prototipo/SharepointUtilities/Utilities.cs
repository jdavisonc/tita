using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using System.Runtime.Remoting.Contexts;
using System.Web.Configuration;
using System.Web.Security;
using System.Xml;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Security.Principal;
using SharepointUtilities.SP.List;
using System.IO;
using System.Xml;

namespace SharepointUtilities
{
    public class Utilities
    {

        public static ListItems<IssueListItem> GetIssues()
        {
            XmlNode xml = Client.GetListItems("Issues", string.Empty, null, null, string.Empty, null);
            return ListItems<IssueListItem>.FromXml(xml.OuterXml);
        }

        public static XmlNode StringToXmlNode(string str)
        {
            XmlTextReader xmlReader = new XmlTextReader(new StringReader(str));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);
            return (XmlNode)xmlDocument;
        }

        public static IssueListItem GetIssue(string ID)
        {
            
            string sQuery = String.Empty;
            sQuery += "<Query>";
            sQuery += "<Where>";
            sQuery += "<Eq>";
            sQuery += "<FieldRef Name=\"ID\" />";
            sQuery += "<Value Type=\"Number\">" + ID + "</Value>";
            sQuery += "</Eq>";
            sQuery += "</Where>";
            sQuery += "</Query>";

            XmlNode node = StringToXmlNode(sQuery);
            XmlNode xml = Client.GetListItems("Issues", string.Empty, node, null, string.Empty, null);
            return ListItems<IssueListItem>.FromXml(xml.OuterXml).RowData.ListItems[0];
            
            /*
            int id = int.Parse(ID);
            XmlNode xml = Client.GetListItems("Issues", string.Empty, null, null, string.Empty, null);
            RowData<IssueListItem> rowData = ListItems<IssueListItem>.FromXml(xml.OuterXml).RowData;
            for (int i = 0; i < rowData.ItemCount; i++)
            {
                if (rowData.ListItems[i].ID == id)
                {
                    return rowData.ListItems[i];
                }
            }
            return null;*/
        }

        private static void OpIssues(IssueListItem issue, string op)
        {
            string xmlIssue = issue.ToXml();
            string sBatch = string.Empty;
            sBatch += "<Batch>";
            sBatch += "<Method ID=\"1\" Cmd=\"" + op +"\">";
            sBatch += issue.ToXml();
            sBatch += "</Method>";
            sBatch += "</Batch>";
            XmlNode node = StringToXmlNode(sBatch);
            Client.UpdateListItems("Issues", node);
        }

        public static void AddIssue(IssueListItem issue)
        {
            OpIssues(issue, "New");
        }

        public static void UpdateIssue(IssueListItem issue)
        {
            OpIssues(issue, "Update");
        }

        public static void DeleteIssue(string id)
        {
            string sBatch = string.Empty;
            sBatch += "<Batch>";
            sBatch += "<Method ID=\"1\" Cmd=\"Delete\">";
            sBatch += "<Field Name=\"ID\">" + id + "</Field>";
            sBatch += "</Method>";
            sBatch += "</Batch>";
            XmlNode node = StringToXmlNode(sBatch);
            Client.UpdateListItems("Issues", node);
        }

        public static string UserName { get; set; }
        public static string Password { get; set; }
        private static Lists _client;
        public static Lists Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new Lists();
                    _client.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }

                return _client;
            }
        }
    }
}
