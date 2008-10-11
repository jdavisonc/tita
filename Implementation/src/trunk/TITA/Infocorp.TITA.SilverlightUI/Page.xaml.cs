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
using Infocorp.TITA.SilverlightUI.UserControls;

namespace Infocorp.TITA.SilverlightUI
{
    public static class ListBoxExtensions 
    {
        public static void SelectedIndexWorkaround(this ListBox listBox)
        {
            int selectedIndex = listBox.SelectedIndex;
            bool set = false;
            listBox.LayoutUpdated += delegate
            {
                if (!set)
                {
                    // Toggle value to force the change
                    listBox.SelectedIndex = -1;
                    listBox.SelectedIndex = selectedIndex;
                    set = true;
                }
            };
        }

        public static void IsSelectedWorkaround(this ListBoxItem listBoxItem)
        {
            bool isSelected = listBoxItem.IsSelected;
            bool set = false;
            listBoxItem.LayoutUpdated += delegate
            {
                if (!set)
                {
                    // Toggle value to force the change
                    listBoxItem.IsSelected = !isSelected;
                    listBoxItem.IsSelected = isSelected;
                    set = true;
                }
            };
        }
    }

    public partial class Page : UserControl
    {

        public enum Option
        {
            WP,
            INCIDENT,
            CONTRACT,
            TASK,
        }
        private string url ="http://localhost/infocorp";
        private DTItem item = new DTItem();
        List<DTItem> lstItem = new List<DTItem>();
        List<DTItem> lstTask = new List<DTItem>();
        private DTItem resulItem = new DTItem();
        private Loading loading = new Loading();
        private bool isEdit;

        private DTItem my_issue_template = null;
        List<DTContract> my_contract = new List<DTContract>();
        private bool isDelete;
       

        public Page()
        {
            InitializeComponent();
            ViewPendingChanges();
        }

        public void EnableOption(Option o)
        {
            isEdit = false;
            CanvasIncident.Visibility = Visibility.Collapsed;
            scroll_INCIDENT.Visibility = Visibility.Collapsed;
            pnl_Contrato.Visibility = Visibility.Collapsed;
            scroll_CONTRACT.Visibility = Visibility.Collapsed;
            CanvasTASK.Visibility = Visibility.Collapsed;
            scroll_TASK.Visibility = Visibility.Collapsed;
            CanvasWP.Visibility = Visibility.Collapsed;
            scroll_WP.Visibility = Visibility.Collapsed;
            if (PnlForm_WP.Children != null)
                PnlForm_WP.Children.Clear();
            if (PnlForm_INCIDENT.Children != null)
                PnlForm_INCIDENT.Children.Clear();
            if (PnlForm_TASK.Children != null)
                PnlForm_TASK.Children.Clear();
            switch (o)
            {
                case Option.WP:
                    CanvasWP.Visibility = Visibility.Visible;
                    scroll_WP.Visibility = Visibility.Visible;
                    break;
                case Option.INCIDENT:
                    CanvasIncident.Visibility = Visibility.Visible;
                    scroll_INCIDENT.Visibility = Visibility.Visible;
                    break;
                case Option.CONTRACT:
                    pnl_Contrato.Visibility = Visibility.Visible;
                    scroll_CONTRACT.Visibility = Visibility.Visible;
                    break;
                case Option.TASK:
                    CanvasTASK.Visibility = Visibility.Visible;
                    scroll_TASK.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        public void ShowError(string msg, bool show)
        {
            if (show)
            {
                CanvasError.Visibility = Visibility.Visible;
                lblError.Text = msg;
            }
            else 
            {
                CanvasError.Visibility = Visibility.Collapsed;
                lblError.Text = "";
            }
        }

        public void ViewPendingChanges()
        {
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.HasPendingChangesCompleted += new EventHandler<HasPendingChangesCompletedEventArgs>(ws_HasPendingChangesCompleted);
            ws.HasPendingChangesAsync(url);
        }

        void ws_HasPendingChangesCompleted(object sender, HasPendingChangesCompletedEventArgs e)
        {
            if (e.Result)
            {
                lblPending.Visibility = Visibility.Visible;
                lblPending.Text = "Hay cambios pendientes";
            }
            else 
            {
                lblPending.Visibility = Visibility.Collapsed;
                lblPending.Text = "";
            }
        }

        #region Contrato

        private void GetContract()
        {
            try
            {
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.GetContractsCompleted += new EventHandler<GetContractsCompletedEventArgs>(ws_GetContractsCompleted);
                ws.GetContractsAsync();
            }
            catch (Exception exp)
            {
                ShowError("No se pudo obtener los contratos:" + exp, true);
            }
        }

        void ws_GetContractsCompleted(object sender, GetContractsCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                my_contract = e.Result;
                lstContratos.ItemsSource = e.Result;
                if (lstContratos.Columns.Count != 0)
                {
                    lstContratos.Columns[0].Visibility = Visibility.Collapsed;
                }
            }
        }

        private void LoadLstContratos(List<DTContract> my_contract)
        {
            if (my_contract.Count() != 0)
            {
                lstContratos.ItemsSource = my_contract;
            }
        }

        private bool LoadPanelEditContrato()
        {
            bool ok = true;
            if (lstContratos.SelectedItem != null)
            {
                DTContract cont = (DTContract)lstContratos.SelectedItem;
                txtNombre.Text = cont.UserName.ToString();
                txtUrl.Text = cont.Site.ToString();
            }
            else 
            {
                ShowError("Debe seleccionar un contrato", true);
                ok = false;
            }
            return ok;
        }

        private void CleanPanel()
        {
            txtNombre.Text = "";
            txtUrl.Text = "";
        }

        private void BtnNuevoContrato_Click(object sender, RoutedEventArgs e)
        {
            ShowError("", false);
            isEdit = false;
            CleanPanel();
            pnlEditContrato.Visibility = Visibility.Visible;
            PnlbtnsContrato.Visibility = Visibility.Collapsed;
            PnlActionContrato.Visibility = Visibility.Visible;
        }

        private void BtnModificarContrato_Click(object sender, RoutedEventArgs e)
        {
            //if (LoadPanelEditContrato())
            //{
            //    isEdit = true;
            //    isDelete = false;
            //    pnlEditContrato.Visibility = Visibility.Visible;
            //    PnlbtnsContrato.Visibility = Visibility.Collapsed;
            //    PnlActionContrato.Visibility = Visibility.Visible;
            //}
            //else 
            //{

            //}
        }

        private void BtnEliminarContrato_Click(object sender, RoutedEventArgs e)
        {
            isDelete = true;
            isEdit = false;
            DTContract cont = (DTContract)lstContratos.SelectedItem;
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.DeleteContractCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_DeleteContractCompleted);
            ws.DeleteContractAsync(cont.ContractId);
        }

        void ws_DeleteContractCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            GetContract();
        }

        bool ValidarCampos(DTContract cont)
        {
            return ((cont.UserName.ToString() != "") && (cont.Site.ToString() != ""));
        }

        private void BtnAceptarContrato_Click(object sender, RoutedEventArgs e)
        {
            if (!isEdit)
            {
                DTContract cont = new DTContract
                {
                    UserName = txtNombre.Text.ToString(),
                    Site = txtUrl.Text.ToString()
                };
                if (ValidarCampos(cont))
                {
                    WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                    ws.AddNewContractCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_AddNewContractCompleted);
                    ws.AddNewContractAsync(cont);

                    PnlbtnsContrato.Visibility = Visibility.Visible;
                    pnlEditContrato.Visibility = Visibility.Collapsed;
                    PnlActionContrato.Visibility = Visibility.Collapsed;
                }
                else 
                {
                    if (txtNombre.Text == "")
                        txtNombre.Text = "Campo requerido";
                    if (txtUrl.Text == "")
                        txtUrl.Text = "Campo requerido";
                    pnlEditContrato.Visibility = Visibility.Visible;
                    PnlbtnsContrato.Visibility = Visibility.Collapsed;
                    PnlActionContrato.Visibility = Visibility.Visible;
                }
            }
            else if (isEdit)
            {
                DTContract cont = (DTContract)lstContratos.SelectedItem;

                DTContract c = new DTContract();
                c.ContractId = cont.ContractId;
                c.Site = txtUrl.Text.ToString();
                c.UserName = txtNombre.Text.ToString();

                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.ModifyContractCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_ModifyContractCompleted);
                ws.ModifyContractAsync(c);

                
                PnlbtnsContrato.Visibility = Visibility.Visible;
                pnlEditContrato.Visibility = Visibility.Collapsed;
                PnlActionContrato.Visibility = Visibility.Collapsed;
                isEdit = false;
            }
            GetContract();
        }

        void ws_ModifyContractCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void ws_AddNewContractCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }

        private void BtnCancelarContrato_Click(object sender, RoutedEventArgs e)
        {
            pnlEditContrato.Visibility = Visibility.Collapsed;
            PnlbtnsContrato.Visibility = Visibility.Visible;
            PnlActionContrato.Visibility = Visibility.Collapsed;
        }

        private void ButtonContract_Click(object sender, RoutedEventArgs e)
        {
            EnableOption(Option.CONTRACT);
            GetContract();
        }

        #endregion

        #region WorkPackage

        private void ButtonWP_Click(object sender, RoutedEventArgs e)
        {
            EnableOption(Option.WP);
            GetWPS();
        }

        public void GetWPS()
        {
            grd_WP.Columns.Clear();
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.GetWorkPackagesCompleted += new EventHandler<GetWorkPackagesCompletedEventArgs>(ws_GetWorkPackagesCompleted);
            ws.GetWorkPackagesAsync(url);
        }

        void ws_GetWorkPackagesCompleted(object sender, GetWorkPackagesCompletedEventArgs e)
        {
            lstItem = e.Result;
            Dispatcher.BeginInvoke(LoadWPS(e.Result));
        }

        private Delegate LoadWPS(List<DTItem> list)
        {
            WorkPackage wp;
            List<WorkPackage> lstWP = new List<WorkPackage>();
            foreach (DTItem w in list)
            {
                wp = new WorkPackage();
                foreach (DTField field in w.Fields)
               {
                    if (!field.Hidden)
                    {
                        switch (field.Name)
                        {
                            case "ID":
                                wp.Id = int.Parse(((DTFieldCounter)field).Value.ToString());
                                break;
                            case "Title":
                                wp.Title = ((DTFieldAtomicString)field).Value;
                                break;
                            case "Start Date":
                                wp.StartDate = ((DTFieldAtomicDateTime)field).Value;
                                break;
                            case "End Date":
                                wp.EndDate = ((DTFieldAtomicDateTime)field).Value;
                                break;
                            case "Proposed End Date":
                                wp.ProposedEndDate = ((DTFieldAtomicDateTime)field).Value;
                                break;
                            case "Status":
                                wp.Status = ((DTFieldChoice)field).Value;
                                break;
                            case "Estimation":
                                wp.Estimation = ((DTFieldAtomicNumber)field).Value;
                                break;
                            case "IsLocal":
                                wp.IsLocal = ((DTFieldAtomicBoolean)field).Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
                lstWP.Add(wp);
            }
            grd_WP.ItemsSource = lstWP;
            if (grd_WP.Columns.Count != 0)
            {
                grd_WP.Columns[0].Visibility = Visibility.Collapsed;
            }
            PnlOption_WP.Visibility = Visibility.Visible;
            return null;
        }

        private void BtnNuevoWP_Click(object sender, RoutedEventArgs e)
        {
            PnlOption_WP.Visibility = Visibility.Collapsed;
            PnlForm_WP.Visibility = Visibility.Visible;
            PnlForm_WP.Children.Add(loading);
            LoadFormsWP();
        }

        private void LoadFormsWP()
        {
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.GetWorkPackageTemplateCompleted += new EventHandler<GetWorkPackageTemplateCompletedEventArgs>(ws_GetWorkPackageTemplateCompleted);
            ws.GetWorkPackageTemplateAsync(url);
        }

        void ws_GetWorkPackageTemplateCompleted(object sender, GetWorkPackageTemplateCompletedEventArgs e)
        {
            PnlAction_WP.Visibility = Visibility.Visible;
            item = e.Result;
            bool ok = LoadForms(Option.WP, grdForm_WP, false);
        }

        private void BtnDeleteWP_Click(object sender, RoutedEventArgs e)
        {
            WorkPackage wp = (WorkPackage)grd_WP.SelectedItem;
            int id = wp.Id;
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.DeleteWorkPackageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_DeleteWorkPackageCompleted);
            ws.DeleteWorkPackageAsync(id, url);
        }

        void ws_DeleteWorkPackageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            StackPanel cnv = (StackPanel)CanvasWP.FindName("PnlForm_WP");
            cnv.Children.Clear();
            PnlAction_WP.Visibility = Visibility.Collapsed;
            PnlOption_WP.Visibility = Visibility.Visible;
            GetWPS();
        }

        private void BtnChangeWP_Click(object sender, RoutedEventArgs e)
        {
            string strMy_pnl = "PnlForm_" + Option.WP;
            StackPanel my_pnl = (StackPanel)GridPrincipal.FindName(strMy_pnl);
            my_pnl.Children.Add(loading);
            PnlOption_WP.Visibility = Visibility.Collapsed;
            if (item == null)
            {
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.GetWorkPackageTemplateCompleted +=new EventHandler<GetWorkPackageTemplateCompletedEventArgs>(ws_GetWorkPackageTemplateCompleted2);
                ws.GetWorkPackageTemplateAsync(url);
            }
            else
            {
                LoadChangeFields(Option.WP);
            }
            PnlAction_WP.Visibility = Visibility.Visible;
        }

        void ws_GetWorkPackageTemplateCompleted2(object sender, GetWorkPackageTemplateCompletedEventArgs e)
        {
            item = e.Result;
            LoadChangeFields(Option.WP);
        }

        private void BtnAcceptWP_Click(object sender, RoutedEventArgs e)
        {
            bool ok = LoadForms(Option.WP, grdForm_WP, true);
            if (ok)
            {
                if (!isEdit)
                {
                    WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                    ws.AddWorkPackageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_AddWorkPackageCompleted);
                    ws.AddWorkPackageAsync(resulItem, url);
                }
                else if (isEdit)
                {
                    WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                    ws.ModifyWPCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_ModifyWPCompleted);
                    ws.ModifyWPAsync(resulItem, url);
                }
            }
        }

        void ws_ModifyWPCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string strMy_pnl = "PnlForm_" + Option.WP;
            StackPanel my_pnl = (StackPanel)GridPrincipal.FindName(strMy_pnl);
            my_pnl.Children.Clear();
            PnlAction_WP.Visibility = Visibility.Collapsed;
            PnlOption_WP.Visibility = Visibility.Visible;
            GetWPS();
        }

        void ws_AddWorkPackageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string strPnl = "PnlForm_" + Option.WP;
            StackPanel cnv = (StackPanel)CanvasWP.FindName(strPnl);
            cnv.Children.Clear();
            PnlAction_WP.Visibility = Visibility.Collapsed;
            PnlOption_WP.Visibility = Visibility.Visible;
            GetWPS();
        }

        private void BtnCancelWP_Click(object sender, RoutedEventArgs e)
        {
            PnlForm_WP.Children.Clear();
            PnlForm_WP.Visibility = Visibility.Collapsed;
            PnlAction_WP.Visibility = Visibility.Collapsed;
            PnlForm_WP.Visibility = Visibility.Visible;
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
            grd_INCIDENT.Columns.Clear();
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.GetIssuesCompleted += new EventHandler<Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesCompletedEventArgs>(ws_GetIssuesCompleted);
            ws.GetIssuesAsync();
        }

        void ws_GetIssuesCompleted(object sender, Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesCompletedEventArgs e)
        {
            lstItem = e.Result;
            Dispatcher.BeginInvoke(LoadIncidents(e.Result));
        }
        
        private Delegate LoadIncidents(List<DTItem> list)
        {
            Issue i;
            List<Issue> lstIssue = new List<Issue>();
            foreach (DTItem issue in list)
            {
                i = new Issue();

                foreach (DTField field in issue.Fields)
                {
                    if (!field.Hidden)
                    {
                        switch (field.Name)
                        {
                            case "ID":
                                i.Id = int.Parse(((DTFieldCounter)field).Value.ToString());
                                break;
                            case "Title":
                                i.Title = ((DTFieldAtomicString)field).Value;
                                break;
                            //case "Status":
                            //    i.Status = ((DTFieldChoice)field).Value;
                            //    break;
                            case "Priority":
                                i.Priority = ((DTFieldChoice)field).Value;
                                break;
                            case "Category":
                                i.Category = ((DTFieldChoice)field).Value;
                                break;
                            case "Reported Date":
                                i.ReportedDate = ((DTFieldAtomicDateTime)field).Value;
                                break;
                            case "WP":
                                i.WP = ((DTFieldChoice)field).Value;
                                break;
                            case "Reported by":
                                i.ReportedBy = ((DTFieldChoice)field).Value;
                                break;
                            case "Order":
                                i.Ord = float.Parse(((DTFieldAtomicNumber)field).Value.ToString());
                                break;
                            case "Resolution":
                                i.Resolution = ((DTFieldAtomicNote)field).Value;
                                break;
                            case "IsLocal":
                                i.IsLocal = ((DTFieldAtomicBoolean)field).Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
                 lstIssue.Add(i);
            }
            grd_INCIDENT.ItemsSource = lstIssue;
            if (grd_INCIDENT.Columns.Count != 0)
            {
                grd_INCIDENT.Columns[0].Visibility = Visibility.Collapsed;
            }
            return null;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            PnlForm_INCIDENT.Children.Clear();
            PnlForm_INCIDENT.Visibility = Visibility.Collapsed;
            PnlAction_INCIDENT.Visibility = Visibility.Collapsed;
            PnlForm_INCIDENT.Visibility = Visibility.Visible;
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            PnlOption_INCIDENT.Visibility = Visibility.Collapsed;
            PnlForm_INCIDENT.Visibility = Visibility.Visible;
            PnlForm_INCIDENT.Children.Add(loading);
            LoadFormsIncedent();
        }

        private void LoadFormsIncedent()
        {
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.GetIssueTemplateCompleted += new EventHandler<GetIssueTemplateCompletedEventArgs>(ws_GetIssueTemplateCompleted);
            ws.GetIssueTemplateAsync();
        }

        void ws_GetIssueTemplateCompleted(object sender, GetIssueTemplateCompletedEventArgs e)
        {
            PnlAction_INCIDENT.Visibility = Visibility.Visible;
            item = e.Result;
            bool ok = LoadForms(Option.INCIDENT, grdForm_INCIDENT, false);
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            bool ok = LoadForms(Option.INCIDENT, grdForm_INCIDENT, true);
            if (ok)
            {
                if (!isEdit)
                {
                    WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                    ws.AddIssueCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_AddIssueCompleted);
                    ws.AddIssueAsync(resulItem,url);
                }
                else if (isEdit)
                {
                    WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                    ws.ModifyIssueCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_ModifyIssueCompleted);
                    ws.ModifyIssueAsync(resulItem,url);
                }
            }
        }

        void ws_ModifyIssueCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) 
        {
            string strMy_pnl = "PnlForm_" + Option.INCIDENT;
            StackPanel my_pnl = (StackPanel)GridPrincipal.FindName(strMy_pnl);
            my_pnl.Children.Clear();
            PnlAction_INCIDENT.Visibility = Visibility.Collapsed;
            PnlOption_INCIDENT.Visibility = Visibility.Visible;
            GetIncidents(); 
        }

        void ws_AddIssueCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string strPnl = "PnlForm_" + Option.INCIDENT;
            StackPanel cnv = (StackPanel)CanvasIncident.FindName(strPnl);
            cnv.Children.Clear();
            PnlAction_INCIDENT.Visibility = Visibility.Collapsed;
            PnlOption_INCIDENT.Visibility = Visibility.Visible;
            GetIncidents();
        }
        
        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            string strMy_pnl = "PnlForm_" + Option.INCIDENT;
            StackPanel my_pnl = (StackPanel)GridPrincipal.FindName(strMy_pnl);
            my_pnl.Children.Add(loading);
            PnlOption_INCIDENT.Visibility = Visibility.Collapsed;
            if (item == null)
            {
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.GetIssueTemplateCompleted += new EventHandler<GetIssueTemplateCompletedEventArgs>(ws_GetIssueTemplateCompleted2);
                ws.GetIssueTemplateAsync();
            }
            else
            {
                LoadChangeFields(Option.INCIDENT);
            }
            PnlAction_INCIDENT.Visibility = Visibility.Visible;
        }

        private void LoadChangeFields(Option pnl)
        {
            string strMy_grd = "grd_" + pnl.ToString();
            DataGrid my_grd = (DataGrid)GridPrincipal.FindName(strMy_grd);
            int id; 
            switch (pnl)
            {
                case Option.WP:
                    WorkPackage wp = (WorkPackage)my_grd.SelectedItem;
                    id = wp.Id;
                    break;
                case Option.INCIDENT:
                    Issue issue = (Issue)my_grd.SelectedItem;
                    id = issue.Id;
                    break;
                case Option.TASK:
                    Task tk = (Task)my_grd.SelectedItem;
                    id = tk.Id;
                    break;
                default:
                    id = int.MinValue;
                    break;
            }
                        
            DTItem change = new DTItem();
            foreach (DTItem i in lstItem)
            {
                foreach (DTField f in i.Fields)
                {
                    if (f.Name == "ID")
                    {
                        if (((DTFieldCounter)f).Value == id)
                        {
                            change = i;
                            item = change;
                        }
                  }
                }
            }
            isEdit = true;
            string str_grdForm = "grdForm_" + pnl.ToString();
            Grid grdForm = (Grid)GridPrincipal.FindName(str_grdForm);
            bool ok = LoadForms(pnl, grdForm, false);

        }

        void ws_GetIssueTemplateCompleted2(object sender, GetIssueTemplateCompletedEventArgs e)
        {
            item = e.Result;
            LoadChangeFields(Option.INCIDENT);
        }

        private void BtnDelete_Action_Click(object sender, RoutedEventArgs e)
        {
            Issue issue = (Issue)grd_INCIDENT.SelectedItem;
            int id = issue.Id;
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.DeleteIssueCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_DeleteIssueCompleted);
            ws.DeleteIssueAsync(id,url);
        }

        void ws_DeleteIssueCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            StackPanel cnv = (StackPanel)CanvasIncident.FindName("PnlForm_INCIDENT");
            cnv.Children.Clear();
            PnlAction_INCIDENT.Visibility = Visibility.Collapsed;
            PnlOption_INCIDENT.Visibility = Visibility.Visible;
            GetIncidents();
        }

        private bool LoadForms(Option pnl, Grid grd, bool isCheck)
        {
            bool ok = true;
            string strMy_pnl = "PnlForm_" + pnl.ToString();
            StackPanel my_pnl = (StackPanel)GridPrincipal.FindName(strMy_pnl);
            grd.ColumnDefinitions.Add(new ColumnDefinition());
            grd.ColumnDefinitions.Add(new ColumnDefinition());
            if (!isCheck && !isEdit)
            {
                int numCtrl = 0;
                int row = 0;

                foreach (DTField field in item.Fields)
                {
                    if (field.Name != "ID" && !field.IsReadOnly && !field.Hidden)
                    {
                        Grid newGrd = new Grid();
                        newGrd.ColumnDefinitions.Add(new ColumnDefinition());
                        newGrd.ColumnDefinitions.Add(new ColumnDefinition());
                        newGrd.ColumnDefinitions[0].Width = new GridLength(200);
                        TextBlock txt = new TextBlock();
                        numCtrl = numCtrl + 1;
                        row = row + 1;

                        if (field is DTFieldAtomicBoolean)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;

                            CheckBox chk = new CheckBox();
                            chk.SetValue(NameProperty, "chk_" + field.Name);
                            chk.Width = 40;
                            chk.SetValue(Grid.ColumnProperty, 1);

                            txt.SetValue(Grid.RowProperty, row);
                            txt.SetValue(Grid.ColumnProperty, 0);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(chk);
                        }
                        else if (field is DTFieldChoice)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            ListBox lstbx = new ListBox();
                            lstbx.SetValue(NameProperty, "lstbx_" + field.Name);
                            lstbx.Width = 80;
                            lstbx.ItemsSource = ((DTFieldChoice)field).Choices;
                            lstbx.SelectedIndex = -1;
                            lstbx.SelectedIndexWorkaround();
                            lstbx.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(lstbx);
                        }
                        else if (field is DTFieldChoiceUser)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            ListBox chuser = new ListBox();
                            chuser.SetValue(NameProperty, "chuser_" + field.Name);
                            chuser.ItemsSource = ((DTFieldChoiceUser)field).Choices;
                            chuser.Width = 80;
                            chuser.SelectedIndex = -1;
                            chuser.SelectedIndexWorkaround();
                            chuser.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(chuser);
                        }
                        else if (field is DTFieldAtomicDateTime)
                        {
                            numCtrl = numCtrl + 3;
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            Calendar cal = new Calendar();
                            cal.SetValue(NameProperty, "cal_" + field.Name);
                            cal.Width = 280;
                            cal.Height = 200;
                            cal.SelectedDate = DateTime.Today;
                            cal.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(cal);
                        }
                        else if (field is DTFieldAtomicNote)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox nt = new TextBox();
                            nt.SetValue(NameProperty, "nt_" + field.Name);
                            nt.Width = 300;
                            nt.Height = 100;
                            nt.TextWrapping = TextWrapping.Wrap;
                            nt.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(nt);
                        }
                        else if (field is DTFieldAtomicNumber)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox num = new TextBox();
                            num.SetValue(NameProperty, "num_" + field.Name);
                            num.Width = 80;
                            num.TextWrapping = TextWrapping.Wrap;
                            num.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(num);
                        }
                        else if (field is DTFieldCounter)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox cnt = new TextBox();
                            cnt.SetValue(NameProperty, "cnt_" + field.Name);
                            cnt.Width = 80;
                            cnt.TextWrapping = TextWrapping.Wrap;
                            cnt.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(cnt);
                        }
                        else 
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox bx = new TextBox();
                            bx.SetValue(NameProperty, "bx_" + field.Name);
                            bx.Width = 80;
                            bx.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(bx);
                        }

                        my_pnl.Children.Add(newGrd);
                    }
                }
                my_pnl.Children.Remove(loading);
            }
            else if (isCheck)
            {
                resulItem.Fields = new List<DTField>();
                foreach (DTField field in item.Fields)
                {
                    if (field.Name != "ID" && !field.Hidden && !field.IsReadOnly)
                    {

                        if (field is DTFieldAtomicBoolean)
                        {
                            CheckBox info = (CheckBox)my_pnl.FindName("chk_" + field.Name);
                            DTFieldAtomicBoolean resultField = new DTFieldAtomicBoolean();
                            resultField.Value = info.IsChecked.Value;
                            resultField.Name = field.Name;
                            resulItem.Fields.Add(resultField);
                        }
                        else if (field is DTFieldChoice)
                        {
                            ListBox lst = (ListBox)my_pnl.FindName("lstbx_" + field.Name);
                            if ((field.Required) && (lst.SelectedItem == null))
                            {
                                TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                                txt.Text = field.Name + "*";
                                ok = false;
                            }
                            DTFieldChoice resultField = new DTFieldChoice();
                            resultField.Value = lst.SelectedItem.ToString();
                            resultField.Name = field.Name;
                            resulItem.Fields.Add(resultField);
                        }
                        else if (field is DTFieldChoiceUser)
                        {
                            ListBox chuser = (ListBox)my_pnl.FindName("chuser_" + field.Name);
                            if ((field.Required) && (chuser.SelectedItem == null))
                            {
                                TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                                txt.Text = field.Name + "*";
                                ok = false;
                            }
                            DTFieldChoiceUser resultField = new DTFieldChoiceUser();
                            resultField.Value = chuser.SelectedItem.ToString();
                            resultField.Name = field.Name;
                            resulItem.Fields.Add(resultField);
                        }
                        else if (field is DTFieldAtomicDateTime)
                        {
                            Calendar cal = (Calendar)my_pnl.FindName("cal_" + field.Name);
                            if ((field.Required) && (cal.SelectedDate.Value.ToShortDateString().ToString() == ""))
                            {
                                TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                                txt.Text = field.Name + "*";
                                ok = false;
                            }
                            DTFieldAtomicDateTime resultField = new DTFieldAtomicDateTime();
                            resultField.Value = cal.SelectedDate.Value;
                            resultField.Name = field.Name;
                            resulItem.Fields.Add(resultField);
                        }
                        else if (field is DTFieldAtomicNote)
                        {
                            TextBox nt = (TextBox)my_pnl.FindName("nt_" + field.Name);
                            if ((field.Required) && (nt.Text == ""))
                            {
                                TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                                txt.Text = field.Name + "*";
                                ok = false;
                            }
                            DTFieldAtomicNote resultField = new DTFieldAtomicNote();
                            resultField.Value = nt.Text;
                            resultField.Name = field.Name;
                            resulItem.Fields.Add(resultField);
                        }
                        else if (field is DTFieldAtomicNumber)
                        {
                            TextBox num = (TextBox)my_pnl.FindName("num_" + field.Name);
                            if ((field.Required) && (num.Text == ""))
                            {
                                TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                                txt.Text = field.Name + "*";
                                ok = false;
                            }
                            DTFieldAtomicNumber resultField = new DTFieldAtomicNumber();
                            resultField.Value = double.Parse(num.Text.ToString());
                            resultField.Name = field.Name;
                            resulItem.Fields.Add(resultField);
                        }
                        else if (field is DTFieldCounter)
                        {
                            TextBox cnt = (TextBox)my_pnl.FindName("cnt_" + field.Name);
                            if ((field.Required) && (cnt.Text == ""))
                            {
                                TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                                txt.Text = field.Name + "*";
                                ok = false;
                            }
                            DTFieldCounter resultField = new DTFieldCounter();
                            resultField.Value = int.Parse(cnt.Text);
                            resultField.Name = field.Name;
                            resulItem.Fields.Add(resultField);
                        }
                        else
                        {
                            TextBox txt = (TextBox)my_pnl.FindName("bx_" + field.Name);
                            if ((field.Required) && (txt.Text.ToString() == ""))
                            {
                                txt.Text = field.Name + "*";
                                ok = false;
                            }
                            DTFieldAtomicString resultField = new DTFieldAtomicString();
                            resultField.Value = txt.Text;
                            resultField.Name = field.Name;

                            resulItem.Fields.Add(resultField);
                        }
                    }
                }
            }
            else if (isEdit && !isCheck)
            {
                int numCtrl = 0;
                int row = 0;

                foreach (DTField field in item.Fields)
                {
                    if (field.Name != "ID" && !field.IsReadOnly && !field.Hidden)
                    {
                        Grid newGrd = new Grid();
                        newGrd.ColumnDefinitions.Add(new ColumnDefinition());
                        newGrd.ColumnDefinitions.Add(new ColumnDefinition());
                        newGrd.ColumnDefinitions[0].Width = new GridLength(200);
                        TextBlock txt = new TextBlock();
                        numCtrl = numCtrl + 1;
                        row = row + 1;

                        if (field is DTFieldAtomicBoolean)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;

                            CheckBox chk = new CheckBox();
                            chk.SetValue(NameProperty, "chk_" + field.Name);
                            chk.IsChecked = ((DTFieldAtomicBoolean)field).Value;
                            chk.Width = 40;
                            chk.SetValue(Grid.ColumnProperty, 1);

                            txt.SetValue(Grid.RowProperty, row);
                            txt.SetValue(Grid.ColumnProperty, 0);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(chk);
                        }
                        else if (field is DTFieldChoice)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            ListBox lstbx = new ListBox();
                            lstbx.SetValue(NameProperty, "lstbx_" + field.Name);
                            lstbx.ItemsSource = ((DTFieldChoice)field).Choices;
                            lstbx.SelectedIndex = lstbx.Items.IndexOf(((DTFieldChoice)field).Value);
                            lstbx.SelectedIndexWorkaround();
                            lstbx.Width = 80;
                            lstbx.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(lstbx);
                        }
                        else if (field is DTFieldChoiceUser)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            ListBox chuser = new ListBox();
                            chuser.SetValue(NameProperty, "chuser_" + field.Name);
                            chuser.ItemsSource = ((DTFieldChoiceUser)field).Choices;
                            chuser.SelectedIndex = chuser.Items.IndexOf(((DTFieldChoiceUser)field).Value);
                            chuser.SelectedIndexWorkaround();
                            chuser.Width = 80;
                            chuser.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(chuser);
                        }
                        else if (field is DTFieldAtomicDateTime)
                        {
                            numCtrl = numCtrl + 3;
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            Calendar cal = new Calendar();
                            cal.SetValue(NameProperty, "cal_" + field.Name);
                            cal.Width = 280;
                            cal.Height = 200;
                            cal.SelectedDate = DateTime.Today;
                            cal.SelectedDate = ((DTFieldAtomicDateTime)field).Value;
                            cal.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(cal);
                        }
                        else if (field is DTFieldAtomicNote)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox nt = new TextBox();
                            nt.SetValue(NameProperty, "nt_" + field.Name);
                            nt.Text = ((DTFieldAtomicNote)field).Value;
                            nt.Width = 300;
                            nt.Height = 100;
                            nt.TextWrapping = TextWrapping.Wrap;
                            nt.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(nt);
                        }
                        else if (field is DTFieldAtomicNumber)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox num = new TextBox();
                            num.SetValue(NameProperty, "num_" + field.Name);
                            num.Text = ((DTFieldAtomicNumber)field).Value.ToString();
                            num.Width = 80;
                            num.TextWrapping = TextWrapping.Wrap;
                            num.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(num);
                        }
                        else if (field is DTFieldCounter)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox cnt = new TextBox();
                            cnt.SetValue(NameProperty, "cnt_" + field.Name);
                            cnt.Text = ((DTFieldCounter)field).Value.ToString();
                            cnt.Width = 80;
                            cnt.TextWrapping = TextWrapping.Wrap;
                            cnt.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(cnt);
                        }
                        else
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = 80;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox bx = new TextBox();
                            bx.SetValue(NameProperty, "bx_" + field.Name);
                            bx.Text = ((DTFieldAtomicString)field).Value;
                            bx.Width = 80;
                            bx.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(bx);
                        }

                        my_pnl.Children.Add(newGrd);
                    }
                }
                my_pnl.Children.Remove(loading);
            }
            return ok;
        }

        #endregion

        #region Task

        private void ButtonTask_Click(object sender, RoutedEventArgs e)
        {
            EnableOption(Option.TASK);
            GetTask();
        }

        private void GetTask()
        {
            grd_TASK.Columns.Clear();
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.GetTasksCompleted += new EventHandler<GetTasksCompletedEventArgs>(ws_GetTasksCompleted);
            ws.GetTasksAsync(url);
        }

        void ws_GetTasksCompleted(object sender, GetTasksCompletedEventArgs e)
        {
            lstTask = e.Result;
            Dispatcher.BeginInvoke(LoadTask(e.Result));
        }

        private Delegate LoadTask(List<DTItem> list)
        {
            Task t;
            List<Task> lstTask = new List<Task>();
            foreach (DTItem task in list)
            {
                t = new Task();

                foreach (DTField field in task.Fields)
                {
                    if (!field.Hidden)
                    {
                        switch (field.Name)
                        {
                            case "ID":
                                t.Id = int.Parse(((DTFieldCounter)field).Value.ToString());
                                break;
                            case "Title":
                                t.Title = ((DTFieldAtomicString)field).Value;
                                break;
                            case "Status":
                                t.Status = ((DTFieldChoice)field).Value;
                                break;
                            case "Priority":
                                t.Priority = ((DTFieldChoice)field).Value;
                                break;
                            case "PorComplete":
                                t.PorComplete = ((DTFieldAtomicNumber)field).Value;
                                break;
                            case "AssignedTo":
                                t.AssignedTo = ((DTFieldAtomicString)field).Value;
                                break;
                            case "Description":
                                t.Description = ((DTFieldAtomicNote)field).Value;
                                break;
                            case "StartDate":
                                t.StartDate = ((DTFieldAtomicDateTime)field).Value;
                                break;
                            case "DueDate":
                                t.DueDate = ((DTFieldAtomicDateTime)field).Value;
                                break;
                            case "IsLocal":
                                t.IsLocal = ((DTFieldAtomicBoolean)field).Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
                lstTask.Add(t);
            }
            grd_TASK.ItemsSource = lstTask;
            if (grd_TASK.Columns.Count != 0)
            {
                grd_TASK.Columns[0].Visibility = Visibility.Collapsed;
            }
            return null;
        }

        private void BtnNuevoTASK_Click(object sender, RoutedEventArgs e)
        {
            PnlOption_TASK.Visibility = Visibility.Collapsed;
            PnlForm_TASK.Visibility = Visibility.Visible;
            PnlForm_TASK.Children.Add(loading);
            LoadFormsTask();
        }

        private void LoadFormsTask()
        {
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.GetTaskTemplateCompleted += new EventHandler<GetTaskTemplateCompletedEventArgs>(ws_GetTaskTemplateCompleted);
            ws.GetTaskTemplateAsync(url);
        }

        void ws_GetTaskTemplateCompleted(object sender, GetTaskTemplateCompletedEventArgs e)
        {
            PnlAction_TASK.Visibility = Visibility.Visible;
            item = e.Result;
            bool ok = LoadForms(Option.TASK, grdForm_TASK, false);
        }

        private void BtnChangeTASK_Click(object sender, RoutedEventArgs e)
        {
            string strMy_pnl = "PnlForm_" + Option.TASK;
            StackPanel my_pnl = (StackPanel)GridPrincipal.FindName(strMy_pnl);
            my_pnl.Children.Add(loading);
            PnlOption_TASK.Visibility = Visibility.Collapsed;
            if (item == null)
            {
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.GetTaskTemplateCompleted +=new EventHandler<GetTaskTemplateCompletedEventArgs>(ws_GetTaskTemplateCompleted2);
                ws.GetIssueTemplateAsync();
            }
            else
            {
                LoadChangeFields(Option.TASK);
            }
            PnlAction_TASK.Visibility = Visibility.Visible;
        }

        void ws_GetTaskTemplateCompleted2(object sender, GetTaskTemplateCompletedEventArgs e)
        {
            item = e.Result;
            LoadChangeFields(Option.TASK);
        }

        private void BtnDeleteTASK_Action_Click(object sender, RoutedEventArgs e)
        {
            Task task = (Task)grd_TASK.SelectedItem;
            int id = task.Id;
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.DeleteTaskCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_DeleteTaskCompleted);
            ws.DeleteTaskAsync(id, url);
        }

        void ws_DeleteTaskCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            StackPanel cnv = (StackPanel)CanvasTASK.FindName("PnlForm_TASK");
            cnv.Children.Clear();
            PnlAction_TASK.Visibility = Visibility.Collapsed;
            PnlOption_TASK.Visibility = Visibility.Visible;
            GetTask();
        }

        private void BtnAcceptTASK_Click(object sender, RoutedEventArgs e)
        {
            bool ok = LoadForms(Option.TASK, grdForm_TASK, true);
            if (ok)
            {
                if (!isEdit)
                {
                    WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                    ws.AddTaskCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_AddTaskCompleted);
                    ws.AddTaskAsync(resulItem, url);
                }
                else if (isEdit)
                {
                    WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                    ws.ModifyTaskCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_ModifyTaskCompleted);
                    ws.ModifyTaskAsync(resulItem, url);
                }
            }
        }

        void ws_ModifyTaskCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string strMy_pnl = "PnlForm_" + Option.TASK;
            StackPanel my_pnl = (StackPanel)GridPrincipal.FindName(strMy_pnl);
            my_pnl.Children.Clear();
            PnlAction_TASK.Visibility = Visibility.Collapsed;
            PnlOption_TASK.Visibility = Visibility.Visible;
            GetTask(); 
        }

        void ws_AddTaskCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string strPnl = "PnlForm_" + Option.TASK;
            StackPanel cnv = (StackPanel)CanvasIncident.FindName(strPnl);
            cnv.Children.Clear();
            PnlAction_TASK.Visibility = Visibility.Collapsed;
            PnlOption_TASK.Visibility = Visibility.Visible;
            GetTask();
        }

        private void BtnDeleteTASK_Click(object sender, RoutedEventArgs e)
        {
            StackPanel cnv = (StackPanel)CanvasTASK.FindName("PnlForm_TASK");
            cnv.Children.Clear();
            PnlAction_TASK.Visibility = Visibility.Collapsed;
            PnlOption_TASK.Visibility = Visibility.Visible;
            GetTask();
        }

        #endregion

        #region Reports

        //private void ButtonRporte_Click(object sender, RoutedEventArgs e) EVENTO DEL MENU
        //{
        //    EnableOption(Option.REPORTS);
        //}

        //private void BtnExportar_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void BtnGenerarREPORTE_Click(object sender, RoutedEventArgs e)
        //{

        //}

        #endregion

        #region Apply
        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.ApplyChangesCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_ApplyChangesCompleted);
                ws.ApplyChangesAsync(url);
            }
            catch(Exception exp)
            {
                ShowError("Error al impactar cambios:" + exp.Message, true);
            }
        }

        void ws_ApplyChangesCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            GetIncidents();
        }
        #endregion

    }
}
