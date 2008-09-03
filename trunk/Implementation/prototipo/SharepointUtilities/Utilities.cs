﻿using System;
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
using SharepointUtilities.SP.Lists;
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

        public static void DeleteListItems(string listName, string id)
        {
        }

        public static void AddListItems(string listName, XmlElement items)
        {
            Client.UpdateListItems(listName, items);
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