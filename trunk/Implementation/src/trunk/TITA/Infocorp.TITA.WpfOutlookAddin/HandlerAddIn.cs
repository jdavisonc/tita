using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Infocorp.TITA.OutlookSharePoint;
using Infocorp.TITA.WpfOutlookAddIn;
using Infocorp.TITA.DataTypes;




namespace Infocorp.TITA.WpfOutlookAddin
{
    public class HandlerAddIn:IHandlerAddIn
    {
        private IOutlookSharePoint _outlookSP;
        
        public HandlerAddIn()
        {
            _outlookSP = (new ControllerOutlookSharepoint()).GetOutlookSharepoint();

        
        }

        #region IHandlerAddIn Members

        public List<DTUrl> GetUrlContracts()
        {
            List<DTUrl> oListDataUrl= new List<DTUrl>();
            XmlDocument doc = new XmlDocument() ;
            doc.Load("Contracts.xml");
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

        

        public void BuildIssue()
        {
            //tomar los valores de la ventana y crear el issue para enviar
            //a Sharepoint

            throw new NotImplementedException();
        }

        #endregion

        
    }
}
