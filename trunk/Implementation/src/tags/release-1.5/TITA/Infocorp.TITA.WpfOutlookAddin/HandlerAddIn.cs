using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Infocorp.TITA.OutlookSharePoint;
using Infocorp.TITA.WpfOutlookAddIn;
using Infocorp.TITA.DataTypes;
using System.Windows;
using System.Windows.Controls;
using AC.AvalonControlsLibrary.Controls;




namespace Infocorp.TITA.WpfOutlookAddin
{
    public class HandlerAddIn : IHandlerAddIn
    {
        private IOutlookSharePoint _outlookSP;
        private string _urlContract = string.Empty;
        private Contracts _contract;
        private Window1 _oIssueWindow;

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

        private void btnContracts_Click(object sender, RoutedEventArgs e)
        {

            _urlContract = GetUrlContracts()[_contract.comboBox1.SelectedIndex].ContractUrl;
            List<DTField> oListaAtrib = _outlookSP.GetFieldsIssue(_urlContract);
            _contract.Close();

            //aca genera la ventana para mostrar los campos del Issue
            _oIssueWindow = new Window1(oListaAtrib);
            _oIssueWindow.SendIssueButton.Click += new RoutedEventHandler(SendIssueButton_Click);
            _oIssueWindow.Show();

        }


        private void SendIssueButton_Click(object sender, RoutedEventArgs e)
        {
            bool isRequiredOK = true;
            Dictionary<DTField,Control> dictionaryElements = _oIssueWindow.MapElements;
            var keyElements = dictionaryElements.Keys;
            foreach (DTField item in keyElements)
            {
                
                switch (item.Type)

	            {
		            case DTField.Types.Integer:
                    case DTField.Types.String:
                    case DTField.Types.Note:
                        if (((TextBox)dictionaryElements[item]).Text.Length == 0 && item.Required)
                        {
                            isRequiredOK = false;
                        }
                        else
                        {
                            item.Value = ((TextBox)dictionaryElements[item]).Text;
                        }
                        
                        break;
                    
                    case DTField.Types.Choice:
                    case DTField.Types.Boolean:
                    case DTField.Types.User:
                        item.Value = ((ComboBox)dictionaryElements[item]).SelectedValue.ToString();                        
                        break;
                    case DTField.Types.DateTime:
                        item.Value = ((DatePicker)dictionaryElements[item]).CurrentlySelectedDate.ToString();                        
                        break;
                    default:
                        break;
	            }

            }
            if (isRequiredOK)
            {
                List<DTField> oList = new List<DTField>(dictionaryElements.Keys);
                BuildIssue(oList);
                _oIssueWindow.Close();
            }
            else
            {
                MessageBox.Show("Faltan campos obligatorios por completar", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public List<DTUrl> GetUrlContracts()
        {
            List<DTUrl> oListDataUrl = new List<DTUrl>();
            XmlDocument doc = new XmlDocument();
            string path = string.Empty;
            try
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TITA Soft\\Contracts.xml";
                doc.Load(path);
            }
            catch (Exception e)
            {
                MessageBox.Show("No se ha podido acceder al archivo Contracts.xml.\n\n" +
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

        public void BuildIncidentWindow()
        {
            _contract = new Contracts(GetUrlContracts());
            _contract.btnContracts.Click += new RoutedEventHandler(btnContracts_Click);
            _contract.Show();
        }



        public void BuildIssue(List<DTField> fields)
        {
            //tomar los valores de la ventana y crear el issue para enviar
            //a Sharepoint
            List<DTAttachment> attachments= new List<DTAttachment>();
            DTIssue oIssue = new DTIssue(fields, attachments);
            _outlookSP.AddIssue(_urlContract, oIssue);
        }

        #endregion


    }
}
