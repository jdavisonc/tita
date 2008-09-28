using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Infocorp.TITA.OutlookSharePoint;
using Infocorp.TITA.WpfOutlookAddIn;
using Infocorp.TITA.DataTypes;
using System.Windows;




namespace Infocorp.TITA.WpfOutlookAddin
{
    public class HandlerAddIn:IHandlerAddIn
    {
        private IOutlookSharePoint _outlookSP;
        
        private static HandlerAddIn _instanceHandlerAddIn = null;

        private HandlerAddIn()
        {
            _outlookSP = (new ControllerOutlookSharepoint()).GetOutlookSharepoint();
        }

        public static HandlerAddIn GetInstanceHandlerAddIn()
        {
            //lock (syncRoot)
            //{
                if (_instanceHandlerAddIn == null)
                {
                    _instanceHandlerAddIn = new HandlerAddIn();
                }
            //}
            
            return _instanceHandlerAddIn;
        }

        #region IHandlerAddIn Members

                public List<DTUrl> GetUrlContracts() 
        {
            List<DTUrl> oListDataUrl= new List<DTUrl>() ;
            XmlDocument doc = new XmlDocument() ;
            string path = string.Empty;
            try
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TITA Soft\\Contracts.xml";
                doc.Load(path);
            }
            catch (Exception e)
            {
                MessageBox.Show("No se ha podido acceder al archivo Contracts.xml.\n\n"+
                "Verifique que el archivo se encuentre en " + path, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                throw e;
            }
            
            XmlNodeList bookList = doc.GetElementsByTagName("Contract");
            foreach (XmlNode node in bookList)
            {
                XmlElement bookElement = (XmlElement)node;
                string title = string.Empty;
                string url = string.Empty;
                if (bookElement.HasAttributes)
                {
                    DTUrl oElementUrl = new DTUrl(bookElement.Attributes["Name"].InnerText, bookElement.Attributes["Url"].InnerText);
                    oListDataUrl.Add(oElementUrl);
                }
            }
            return oListDataUrl;
        }

        public void BuildIncidentWindow(DTUrl url)
        {
            
            List<DTField> oListaAtrib = _outlookSP.GetFieldsIssue(url.ContractUrl);
            
            //aca generar la ventana para mostrar los campos del Issue
            Window1 oIssueWindow = new Window1(oListaAtrib);
            oIssueWindow.Show();
        }

        

        public void BuildIssue(string urlSite, DTIssue issue)
        {
            //tomar los valores de la ventana y crear el issue para enviar
            //a Sharepoint

            _outlookSP.AddIssue(urlSite, issue);
        }

        #endregion

        
    }
}
