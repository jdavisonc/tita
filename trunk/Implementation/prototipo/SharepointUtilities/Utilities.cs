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

namespace SharepointUtilities
{
    public class Utilities
    {

        public static XmlNode GetListCollection()
        {
            return Client.GetListCollection();
        }

        public static void AddListItems(string listName, XmlElement items)
        {/*
            string xmlIssue = issue.ToXml();
            string sBatch = string.Empty; 
            sBatch += "<Batch>"; 
            sBatch += "<Method ID=\"1\" Cmd=\"Update\">"; 
            sBatch += issue.ToXml();
            sBatch += "</Method>"; 
            sBatch += "</Batch>";
            XmlTextReader xmlReader = new XmlTextReader(new StringReader(sBatch));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);
            XmlNode node = (XmlNode)xmlDocument;
           // Client.UpdateListItems("Issues", node);

            Client.UpdateListItems(listName, items);*/
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

        public static void DeleteIssue(IssueListItem issue)
        {
            OpIssues(issue, "Delete");
        }

        private static void GetClientCredentials(ClientCredentials clientCredentials)
        {
            clientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
        }

        public static string UserName { get; set; }
        public static string Password { get; set; }
        private static ListsSoapClient _client;
        public static ListsSoapClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new ListsSoapClient();
                    GetClientCredentials(_client.ClientCredentials); ;
                }

                return _client;
            }
        }
    }
}
