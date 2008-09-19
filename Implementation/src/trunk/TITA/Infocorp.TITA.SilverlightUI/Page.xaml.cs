using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Infocorp.TITA.SilverlightUI.Code;
using Infocorp.TITA.SilverlightUI.WSTitaReference;

namespace Infocorp.TITA.SilverlightUI
{
    public partial class Page : UserControl
    {
        public enum Option
        {
            WP,
            INCIDENT,
            CONTRACT,
        }

        private bool isEdit;
        private bool isDelete;

        public Page()
        {
            InitializeComponent();
        }

        public void EnableOption(Option o)
        {
            CanvasIncident.Visibility = Visibility.Collapsed;
            pnl_Contrato.Visibility = Visibility.Collapsed;
            CanvasWP.Visibility = Visibility.Collapsed;
            switch (o)
            {
                case Option.WP:
                    CanvasWP.Visibility = Visibility.Visible;
                    break;
                case Option.INCIDENT:
                    CanvasIncident.Visibility = Visibility.Visible;
                    break;
                case Option.CONTRACT:
                    pnl_Contrato.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        #region Contrato

        private void LoadLstContratos()
        {
            List<Contrato> lst = new List<Contrato>();
            lst.Add(new Contrato
            {
                Id = 1,
                Nombre = "prueba1_NOMBRE",
                Url = "prueba1_URL"
            });

            lst.Add(new Contrato
            {
                Id = 2,
                Nombre = "prueba2_NOMBRE",
                Url = "prueba2_URL"
            });
            lstContratos.ItemsSource = lst;

        }

        private void LoadPanelEditContrato()
        {
            Contrato cont = (Contrato)lstContratos.SelectedItem;
            txtNombre.Text = cont.Nombre.ToString();
            txtUrl.Text = cont.Url.ToString();
        }

        private void lstItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isEdit)
            {
                LoadPanelEditContrato();
                BtnNuevoContrato.Visibility = Visibility.Collapsed;
            }

            if (isDelete)
            {
                Contrato cont = (Contrato)lstContratos.SelectedItem;
                int id = cont.Id;
                Functions func = new Functions();
                func.DeleteContrato(id);
                LoadLstContratos();
                lstContratos.Visibility = Visibility.Visible;
                BtnNuevoContrato.Visibility = Visibility.Visible;
                ButtonContract.Visibility = Visibility.Visible;
                ButtonIncident.Visibility = Visibility.Visible;
                ButtonWP.Visibility = Visibility.Visible;
                ButtonReports.Visibility = Visibility.Visible;
            }
        }

        private void BtnNuevoContrato_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isEdit = false;
            pnlEditContrato.Visibility = Visibility.Visible;
            BtnNuevoContrato.Visibility = Visibility.Collapsed;
        }

        private void BtnModificarContrato_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isEdit = true;
            isDelete = false;
            pnlEditContrato.Visibility = Visibility.Visible;
        }

        private void BtnEliminarContrato_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDelete = true;
            isEdit = false;
        }

        private void BtnAceptarContrato_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isEdit)
            {
                Functions func = new Functions();
                Contrato cont = new Contrato
                {
                    Nombre = txtNombre.Text.ToString(),
                    Url = txtUrl.Text.ToString()
                };
                func.AddContrato(cont);
                BtnNuevoContrato.Visibility = Visibility.Visible;
                pnlEditContrato.Visibility = Visibility.Collapsed;

            }
            else if (isEdit)
            {
                Contrato cont = (Contrato)lstContratos.SelectedItem;
                int id = cont.Id;
                Functions func = new Functions();
                func.UpdateContrato(txtNombre.Text.ToString(), txtUrl.Text.ToString(), id);
                BtnNuevoContrato.Visibility = Visibility.Visible;
                pnlEditContrato.Visibility = Visibility.Collapsed;
                isEdit = false;
            }
            LoadLstContratos();
        }

        private void BtnCancelarContrato_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pnlEditContrato.Visibility = Visibility.Collapsed;
            BtnNuevoContrato.Visibility = Visibility.Visible;
        }

        private void ButtonContract_Click(object sender, RoutedEventArgs e)
        {
            EnableOption(Option.CONTRACT);
            LoadLstContratos();
        }

        #endregion


        #region WorkPackage

        private void ButtonWP_Click(object sender, RoutedEventArgs e)
        {
            CanvasWP.Visibility = Visibility;
            //DataWorkPackage dataWP1 = new DataWorkPackage();
            //dataWP1.IdWP = 1;

            //DataObject dataObj = new DataObject();
            //dataObj.NameField = "Nombre";
            //dataObj.TypeField = "string";
            //dataObj.IsRequired = true;

            //dataWP1.DataList.Add(dataObj);

            //ListBox list = (ListBox)CanvasWP.Children;

            //PrintGeneric printG = PrintGeneric.Instance;
            //List<object> lin = new List<object>();
            //lin.Add(dataWP1);

            //List<object> lout = printG.printDataQueryWP(lin);

            //foreach (object o in lout)
            //{
            //    list.DataContext(o.GetHashCode());
            //}
        }

        private void ButtonNewWP_Click(object sender, RoutedEventArgs e)
        {
            //CanvasNewWP.Visibility = Visibility;
        }

        private void ListBoxWP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion


        #region Incident

        private void ButtonIncident_Click(object sender, RoutedEventArgs e)
        {
            EnableOption(Option.INCIDENT);
            GetIncidents();
        }

        private void GetIncidents()
        {
            grdIncident.Columns.Clear();
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.GetIssuesCompleted += new EventHandler<Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesCompletedEventArgs>(ws_GetIssuesCompleted);
            ws.GetIssuesAsync();
        }
        void ws_GetIssuesCompleted(object sender, Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(LoadIncidents(e.Result));
        }
        private Delegate LoadIncidents(List<Infocorp.TITA.SilverlightUI.WSTitaReference.DTIssue> list) 
        {
            Issue i;
            List<Issue> lstIssue = new List<Issue>();
            foreach (Infocorp.TITA.SilverlightUI.WSTitaReference.DTIssue issue in list)
            {
                foreach (Infocorp.TITA.SilverlightUI.WSTitaReference.DTField field in issue.Fields)
                {
                    i = new Issue();
                    switch (field.Name)
                    {
                        case "ID":
                            i.Id = Convert.ToInt32(field.Value);
                            break;
                        case "Title":
                            i.Title = field.Value;
                            break;
                        case "Status":
                            i.Status = field.Value;
                            break;
                        case "Priority":
                            i.Priority = field.Value;
                            break;
                        case "Category":
                            i.Category = field.Value;
                            break;
                        case "ReportedDate":
                            i.ReportedDate = field.Value;
                            break;
                        case "WP":
                            i.WP = field.Value;
                            break;
                        case "ReportedBy":
                            i.ReportedBy = field.Value;
                            break;
                        case "Ord":
                            i.Ord = float.Parse(field.Value);
                            break;
                        case "Resolution":
                            i.Resolution = field.Value;
                            break;
                        default:
                            break;
                    }
                    lstIssue.Add(i);
                }
            }
            grdIncident.ItemsSource = lstIssue;
            return null;
        }

        #endregion
    }
}
