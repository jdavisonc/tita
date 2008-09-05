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

        private static void OpIssues(IssueListItem issue, string op)
        {
            string xmlIssue = issue.ToXml();
            string sBatch = string.Empty;
            sBatch += "<Batch>";
            sBatch += "<Method ID=\"1\" Cmd=\"" + op +"\">";
            sBatch += issue.ToXml();
            sBatch += "</Method>";
            sBatch += "</Batch>";
            XmlTextReader xmlReader = new XmlTextReader(new StringReader(sBatch));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);
            XmlNode node = (XmlNode)xmlDocument;
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
            XmlTextReader xmlReader = new XmlTextReader(new StringReader(sBatch));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);
            XmlNode node = (XmlNode)xmlDocument;
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
