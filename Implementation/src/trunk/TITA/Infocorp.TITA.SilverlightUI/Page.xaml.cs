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
using Liquid;

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

        private List<DTIssue> my_issue = new List<DTIssue>();
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
        private Delegate LoadIncidents(List<DTIssue> list)
        {
            Issue i;
            List<Issue> lstIssue = new List<Issue>();
            foreach (DTIssue issue in list)
            {
                i = new Issue();
                foreach (DTField field in issue.Fields)
                {
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
                }
                lstIssue.Add(i);
            }
            grdIncident.ItemsSource = lstIssue;
            my_issue = list;
            return null;
        }

        #endregion

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            PnlbtnNuevo.Visibility = Visibility.Collapsed;
            ShowNewPanel();
            PnlAction.Visibility = Visibility.Visible;
        }

        private void ShowNewPanel()
        {
            int numCtrl = 0;
            Canvas cnv = (Canvas)CanvasIncident.FindName("PnlNew");

            foreach (DTIssue issue in my_issue)
            {
                foreach (DTField field in issue.Fields)
                {
                    if (field.Name != "ID")
                    {
                        TextBlock txt = new TextBlock();
                        numCtrl = numCtrl + 1;
                        switch (field.Type)
                        {
                            case Types.Boolean:

                                txt.Text = field.Name;
                                txt.SetValue(NameProperty, "txt_" + field.Name);
                                txt.Margin = new Thickness(50, numCtrl * 20, 0, 0);
                                txt.Width = 80;

                                CheckBox chk = new CheckBox();
                                chk.SetValue(NameProperty, "chk_" + field.Name);
                                chk.Margin = new Thickness(140, numCtrl * 20, 0, 0);
                                chk.Width = 40;

                                cnv.Children.Add(txt);
                                cnv.Children.Add(chk);
                                break;
                            case Types.Choice:
                                txt.Text = field.Name;
                                txt.SetValue(NameProperty, "txt_" + field.Name);
                                txt.Margin = new Thickness(50, numCtrl * 20, 0, 0);
                                txt.Width = 80;

                                ListBox lstbx = new ListBox();
                                lstbx.SetValue(NameProperty, "lstbx_" + field.Name);
                                lstbx.Margin = new Thickness(140, numCtrl * 20, 0, 0);
                                lstbx.Width = 80;
                                lstbx.ItemsSource = field.Choices;

                                //DropDownList drp = new DropDownList();
                                //drp.SetValue(NameProperty, "drp_" + field.Name);
                                //foreach (string option in field.Choices){
                                //    drp.Items.Add(new ListItem(option,option));
                                //}
                                //drp.DataBind();
                                //drp.Margin = new Thickness(140, numCtrl * 20, 0, 0);

                                cnv.Children.Add(txt);
                                cnv.Children.Add(lstbx);
                                break;
                            case Types.DateTime:
                                numCtrl = numCtrl + 3;
                                txt.Text = field.Name;
                                txt.SetValue(NameProperty, "txt_" + field.Name);
                                txt.Margin = new Thickness(50, numCtrl * 20, 0, 10);
                                txt.Width = 80;

                                Calendar cal = new Calendar();
                                cal.SetValue(NameProperty, "cal_" + field.Name);
                                cal.Margin = new Thickness(140, numCtrl * 20, 0, 0);
                                cal.Width = 280;
                                cal.Height = 200;

                                cnv.Children.Add(txt);
                                cnv.Children.Add(cal);
                                break;
                            default:
                                txt.Text = field.Name;
                                txt.SetValue(NameProperty, "txt_" + field.Name);
                                txt.Margin = new Thickness(50, numCtrl * 20, 0, 0);
                                txt.Width = 80;

                                TextBox bx = new TextBox();
                                bx.SetValue(NameProperty, "bx_" + field.Name);
                                bx.Margin = new Thickness(140, numCtrl * 20, 0, 0);
                                bx.Width = 80;

                                cnv.Children.Add(txt);
                                cnv.Children.Add(bx);
                                break;
                        }
                    }
                }
            }
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
           DTIssue issue = new DTIssue();
           issue.Fields = new List<DTField>();
           DTField field = new DTField();
           Canvas cnv = (Canvas)CanvasIncident.FindName("PnlNew");

           foreach (DTIssue i in my_issue)
           {
                foreach (DTField f in i.Fields)
                {
                    switch (f.Type)
                    {
                        case Types.Boolean:
                            //CheckBox info = (CheckBox)cnv.FindName("chk_" + f.Name);
                            //field.Value = info.Text;
                            //field.Type = field.Type;
                            //field.Name = f.Name;
                            break;
                        case Types.Choice:

                            break;
                        case Types.DateTime:

                            break;
                        default:
                            TextBox info = (TextBox)cnv.FindName("bx_" + f.Name);
                            field.Value = info.Text;
                            field.Type = field.Type;
                            field.Name = f.Name;
                            break;
                    }
                }
                issue.Fields.Add(field);
            }
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.AddIssueCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_AddIssueCompleted);
            ws.AddIssueAsync(issue);
        }

        void ws_AddIssueCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {}

    }
}
