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
using Microsoft.Office.Interop.Outlook;







namespace Infocorp.TITA.WpfOutlookAddin
{
    public class HandlerAddIn : IHandlerAddIn
    {
        private IOutlookSharePoint _outlookSP;
        private string _urlContract = string.Empty;
        private string _listName = string.Empty;
        private Contracts _contract;
        private Window1 _oIssueWindow;
        private MyMail _mailSelected = null;


        public MyMail MailSelected
        {
            set { _mailSelected = value; }
        }

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
            if (_contract.comboBox1.SelectedIndex != -1)
            {
                try
                {

                    DTUrl oUrlSelected = GetUrlContracts()[_contract.comboBox1.SelectedIndex];

                    _urlContract = oUrlSelected.ContractUrl;
                    _listName = oUrlSelected.IssueList;

                    List<DTField> oListaAtrib = _outlookSP.GetFieldsIssue(_urlContract, _listName);
                    foreach (DTField item in oListaAtrib)
                    {
                        if (item.Name == oUrlSelected.MailBodyField)
                        {
                            ((DTFieldAtomicNote)item).Value = _mailSelected.GetMail().Body;
                        }
                        else if (item.Name == oUrlSelected.TitleField)
                        {
                            ((DTFieldAtomicString)item).Value = _mailSelected.GetMail().Subject;
                        }

                    }

                    _contract.Close();

                    //aca genera la ventana para mostrar los campos del Issue
                    _oIssueWindow = new Window1(oListaAtrib);

                    _oIssueWindow.SendIssueButton.Click += new RoutedEventHandler(SendIssueButton_Click);
                    _oIssueWindow.Show();
                }
                catch (System.Exception ex){
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un contrato", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void SendIssueButton_Click(object sender, RoutedEventArgs e)
        {
            bool isRequiredOK = true;
            Dictionary<DTField,Control> dictionaryElements = _oIssueWindow.MapElements;
            var keyElements = dictionaryElements.Keys;
            try
            {
                foreach (DTField item in keyElements)
                {

                    switch (item.GetCustomType())
                    {
                        case DTField.Types.Number:
                            if (((TextBox)dictionaryElements[item]).Text.Length == 0 && item.Required)
                            {
                                isRequiredOK = false;
                            }
                            else if (((TextBox)dictionaryElements[item]).Text.Length > 0)
                            {
                                ((DTFieldAtomicNumber)item).Value = int.Parse(((TextBox)dictionaryElements[item]).Text);
                            }
                            break;
                        case DTField.Types.String:
                            if (((TextBox)dictionaryElements[item]).Text.Length == 0 && item.Required)
                            {
                                isRequiredOK = false;
                            }
                            else
                            {
                                ((DTFieldAtomicString)item).Value = ((TextBox)dictionaryElements[item]).Text;
                            }
                            break;
                        case DTField.Types.Note:
                            if (((TextBox)dictionaryElements[item]).Text.Length == 0 && item.Required)
                            {
                                isRequiredOK = false;
                            }
                            else
                            {
                                ((DTFieldAtomicNote)item).Value = ((TextBox)dictionaryElements[item]).Text;
                            }
                            break;
                        case DTField.Types.Choice:
                            ((DTFieldChoice)item).Value = ((ComboBox)dictionaryElements[item]).SelectedValue.ToString();
                            break;
                        case DTField.Types.Boolean:
                            if (((ComboBox)dictionaryElements[item]).SelectedValue.ToString().CompareTo("True") == 0)
                            {
                                ((DTFieldAtomicBoolean)item).Value = true;
                            }
                            else
                            {
                                ((DTFieldAtomicBoolean)item).Value = false;
                            }
                            break;
                        case DTField.Types.User:
                            ((DTFieldChoiceUser)item).Value = ((ComboBox)dictionaryElements[item]).SelectedValue.ToString();
                            break;
                        case DTField.Types.DateTime:
                            ((DTFieldAtomicDateTime)item).Value = ((DatePicker)dictionaryElements[item]).CurrentlySelectedDate;
                            break;
                        case DTField.Types.Lookup:
                            ((DTFieldChoiceLookup)item).Value = ((ComboBox)dictionaryElements[item]).SelectedValue.ToString();
                            break;
                        case DTField.Types.Default:
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
            }catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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
            catch (System.Exception e)
            {
                MessageBox.Show("No se ha podido acceder al archivo Contracts.xml.\n\n" +
                "Verifique que el archivo se encuentre en " + path, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                throw e;
            }
            try
            {
                XmlNodeList bookList = doc.GetElementsByTagName("Contract");
                foreach (XmlNode node in bookList)
                {
                    XmlElement bookElement = (XmlElement)node;
                    string title = string.Empty;
                    string url = string.Empty;
                    if (bookElement.HasAttributes)
                    {
                        XmlElement issueListElement = (XmlElement)bookElement.GetElementsByTagName("IssueList")[0];
                        XmlElement titleFieldElement = (XmlElement)bookElement.GetElementsByTagName("TitleField")[0];
                        XmlElement mailBodyFieldElement = (XmlElement)bookElement.GetElementsByTagName("MailField")[0];
                        DTUrl oElementUrl = new DTUrl(bookElement.Attributes["Name"].InnerText, bookElement.Attributes["Url"].InnerText,
                            issueListElement.Attributes["Name"].InnerText, titleFieldElement.Attributes["Name"].InnerText, mailBodyFieldElement.Attributes["Name"].InnerText);
                        oListDataUrl.Add(oElementUrl);
                    }
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show("El archivo Contracts.xml esta mal formado.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                throw e;
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
            if (_mailSelected != null)
            {
                DTAttachment oAttachToSend = new DTAttachment("mail.msg", _mailSelected.GetByteArrayWithObject());
                attachments.Add(oAttachToSend);
            }
            
            
            DTItem oIssue = new DTItem(fields, attachments);
            if (_outlookSP.AddIssue(_urlContract, _listName, oIssue)) 
            {
                MessageBox.Show("Se reporto el incidente exitosamente", "Reporte inicidente", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
        }

        #endregion


    }
}
