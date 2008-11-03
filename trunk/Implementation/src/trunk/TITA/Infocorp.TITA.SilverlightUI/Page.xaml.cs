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
using Infocorp.TITA.SilverlightUI.UserControls;
using System.Reflection;
using Cooper.Silverlight.Controls;

namespace Infocorp.TITA.SilverlightUI
{
    
    public partial class Page : UserControl
    {
       
        public enum Option
        {
            WP,
            INCIDENT,
            CONTRACT,
            TASK,
            REPORT,
        }

        private string url = null;
        private DTItem item = new DTItem();
        List<DTItem> lstItem = new List<DTItem>();
        //List<DTItem> lstIssue = new List<DTItem>();

        List<Issue> lstIssue = new List<Issue>();
        List<DTReportedItem> my_lstReport_issues = new List<DTReportedItem>();
        List<DTWorkPackageReport> my_lstReport_deswp = new List<DTWorkPackageReport>();

        private DTItem resulItem = new DTItem();
        //private Progress progress = new Progress();
        private bool isEdit;
        private string map = "";
        private bool isIssueWP;
        private bool oneContract;
        private bool forReport = false;
        private List<WorkPackage> my_lstWP = new List<WorkPackage>();
        private List<Task> my_lstTask = new List<Task>();
        private List<DTContract> my_contract = new List<DTContract>();
        private DTContract my_con = null;
        private List<string> ColumnsToShow = null;// new List<string>() { "id", "title", "status", "priority", "category", "Reported Date", "Work Package", "Reported by", "Order", "Resolution", "IsLocal" };
        
        public Page()
        {
            InitializeComponent();
            ScrollViewerMouseWheelSupport.Initialize(GridPrincipal, MouseWheelAssociationMode.OnHover);
            ScrollViewerMouseWheelSupport.AddMouseWheelSupport(scroll_WP);
            ScrollViewerMouseWheelSupport.AddMouseWheelSupport(scroll_CONTRACT);
            ScrollViewerMouseWheelSupport.AddMouseWheelSupport(scroll_INCIDENT);
            ScrollViewerMouseWheelSupport.AddMouseWheelSupport(scroll_TASK);
            ScrollViewerMouseWheelSupport.AddMouseWheelSupport(scroll_REPORT);
            ShowCurrentContract();
            this.Loaded += new RoutedEventHandler(Page_Loaded);
           
            if (url != null)
            {
                ViewPendingChanges();
            }

            //grd_INCIDENT.AutoGeneratingColumn += new EventHandler<DataGridAutoGeneratingColumnEventArgs>(grd_INCIDENT_AutoGeneratingColumn);
        }

        void GetGrilla(object sender, RoutedEventArgs e)
        {
            GetIncidents();
        }

        #region Menu
        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            acc.AddItem("Contratos", "", "Sharepoint Utilities", "Contratos", this.Resources["ItemStyle1"] as Style);
            acc.AddItem("Incidentes", "", "Sharepoint Utilities", "Incidentes", this.Resources["ItemStyle1"] as Style);
            acc.AddItem("Workpakage", "", "Sharepoint Utilities", "Workpakage", this.Resources["ItemStyle1"] as Style);
            acc.AddItem("Tasks", "", "Sharepoint Utilities", "Tasks", this.Resources["ItemStyle1"] as Style);

            acc.AddItem("Desviacion WP", "", "Reportes", "Desviacion WP", this.Resources["ItemStyle1"] as Style);
            acc.AddItem("Issue", "", "Reportes", "Issue", this.Resources["ItemStyle1"] as Style);
            acc.AddItem("Todos Issue", "", "Reportes", "Todos Issue", this.Resources["ItemStyle1"] as Style);

            acc.AddItem("Incidentes", "", "Por impactar", "Incidentes_aplicar", this.Resources["ItemStyle1"] as Style);
            acc.AddItem("Workpackages", "", "Por impactar", "Workpakage_aplicar", this.Resources["ItemStyle1"] as Style);
            acc.AddItem("Tasks", "", "Por impactar", "Tasks_aplicar", this.Resources["ItemStyle1"] as Style);

            acc.AddItem("Incidentes", "", "Mapeo de Valores", "Incidentes_map", this.Resources["ItemStyle1"] as Style);
            acc.AddItem("Workpackages", "", "Mapeo de Valores", "Workpakage_map", this.Resources["ItemStyle1"] as Style);
            acc.AddItem("Tasks", "", "Mapeo de Valores", "Tasks_map", this.Resources["ItemStyle1"] as Style);

            Style aux = this.Resources["GroupStyle1"] as Style;
            acc.setGroupStyle("Sharepoint Utilities", this.Resources["GroupStyle1"] as Style);
            acc.setGroupStyle("Reportes", this.Resources["GroupStyle1"] as Style);
            acc.setGroupStyle("Por impactar", this.Resources["GroupStyle1"] as Style);
            acc.setGroupStyle("Mapeo de Valores", this.Resources["GroupStyle1"] as Style);

            acc.ItemSelect += new Infocorp.TITA.Controls.Silverlight.V2.Accordion.ItemSelectEvent(acc_ItemSelect);

            //pager.ItemsSource = lstItem;
            pager.ItemsSource = lstIssue;
            pager_incident_wp.ItemsSource = lstIssue;
            pager_wp.ItemsSource = my_lstWP;
            pager_contratos.ItemsSource = my_contract;
            pager_contractsReport.ItemsSource = my_contract;
            pager_tasks.ItemsSource = my_lstTask;
            pager_grd_REPORT_DESWP.ItemsSource = my_lstReport_deswp;
            pager_grd_REPORT_ISSUES.ItemsSource = my_lstReport_issues;
        }

        void acc_ItemSelect(object sender, Infocorp.TITA.Controls.Silverlight.V2.ItemEventArgs e)
        {
            switch (e.id)
            {
                case "Contratos":
                    ViewContrat();
                    break;
                case "Incidentes":
                    ViewIncident();
                    break;
                case "Workpakage":
                    ViewWorkpakage();
                    break;
                case "Tasks":
                    ViewTasks();
                    break;
                case "Desviacion WP":
                    ViewReportDESWP();
                    break;
                case "Issue":
                    oneContract = true;
                    ViewReportISSUESREPORT(true);
                    break;
                case "Todos Issue":
                    oneContract = false;
                    ViewReportISSUESREPORT(false);
                    break;
                case "Incidentes_aplicar":
                    ViewIncidentesPorAplicar();
                    break;
                case "Workpakage_aplicar":
                    ViewWorkpakagePorAplicar();
                    break;
                case "Tasks_aplicar":
                    ViewTasksPorAplicar();
                    break;
                case "Incidentes_map":
                    ViewIssueMap();
                    break;
                case "Workpakage_map":
                    ViewWorkpackageMap();
                    break;
                case "Tasks_map":
                    ViewTaskMap();
                    break;
                default:
                    break;
            }
        }

        #endregion

        public void EnableOption(Option o)
        {
            isEdit = false;
            isIssueWP = false;
            ShowError("", false);
            lblConectContract.Text = "";
            grd_INCIDENT_WP.Visibility = Visibility.Collapsed;
            lblacceder_error.Visibility = Visibility.Collapsed;
            lblConectContract.Visibility = Visibility.Collapsed;
            CanvasIncident.Visibility = Visibility.Collapsed;
            scroll_INCIDENT.Visibility = Visibility.Collapsed;
            pnl_Contrato.Visibility = Visibility.Collapsed;
            scroll_CONTRACT.Visibility = Visibility.Collapsed;
            CanvasTASK.Visibility = Visibility.Collapsed;
            scroll_TASK.Visibility = Visibility.Collapsed;
            CanvasWP.Visibility = Visibility.Collapsed;
            scroll_WP.Visibility = Visibility.Collapsed;
            scroll_REPORT.Visibility = Visibility.Collapsed;
            contractsReport.Visibility = Visibility.Collapsed;
            lstContratos.Visibility = Visibility.Collapsed;
            CanvasMap.Visibility = Visibility.Collapsed;
            ClearReport();
            if (PnlForm_WP.Children != null)
                PnlForm_WP.Children.Clear();
            if (PnlForm_INCIDENT.Children != null)
                PnlForm_INCIDENT.Children.Clear();
            if (PnlForm_TASK.Children != null)
                PnlForm_TASK.Children.Clear();
            logo.Visibility = Visibility.Collapsed;
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
                    contractsReport.Visibility = Visibility.Visible;
                    break;
                case Option.TASK:
                    CanvasTASK.Visibility = Visibility.Visible;
                    scroll_TASK.Visibility = Visibility.Visible;
                    break;
                case Option.REPORT:
                    scroll_REPORT.Visibility = Visibility.Visible;
                    CanvasREPORT.Visibility = Visibility.Visible;
                    PnlOptionREPORT.Visibility = Visibility.Visible;
                    break;
                default:
                    logo.Visibility = Visibility.Visible;
                    break;
            }
        }

        public void ClearReport()
        {
            contractsReport.Visibility = Visibility.Collapsed;
            PnlOptionREPORT.Visibility = Visibility.Collapsed;
            grd_REPORT.Visibility = Visibility.Collapsed;
            PnlExportar_REPORT.Visibility = Visibility.Collapsed;
            BtnExportar_DESWP.Visibility = Visibility.Collapsed;
            pager_grd_REPORT_DESWP.Visibility = Visibility.Collapsed;
            pager_grd_REPORT_ISSUES.Visibility = Visibility.Collapsed;
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

        void ShowCurrentContract()
        {
            GetContract();
            lbl_Contrat.Text = "Contrato actual: ";
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
                ShowError("No se pudo?obtener los contratos:" + exp, true);
            }
        }

        void ws_GetContractsCompleted(object sender, GetContractsCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                my_contract = e.Result;
                pager_contratos.ItemsControl = lstContratos;
                pager_contratos.ItemsSource = my_contract;
                if (forReport)
                {
                    pager_contractsReport.ItemsControl = contractsReport;
                    pager_contractsReport.ItemsSource = my_contract;
                    if (contractsReport.Columns.Count != 0)
                    {
                        contractsReport.Columns[0].Visibility = Visibility.Collapsed;
                    }
                    contractsReport.Visibility = Visibility.Visible;
                    contractsReport.IsReadOnly = true;
                    contractsReport.CanUserResizeColumns = false;
                }
                else
                {
                    lstContratos.ItemsSource = my_contract;
                    if (lstContratos.Columns.Count != 0)
                    {
                        lstContratos.Columns[0].Visibility = Visibility.Collapsed;
                    }
                    lstContratos.Visibility = Visibility.Visible;
                    lstContratos.IsReadOnly = true;
                    lstContratos.CanUserResizeColumns = false;
                }
                if ((my_contract.Count > 0) && (my_con == null))
                {
                  cbx_contrat_up.ItemsSource = my_contract;
                  cbx_contrat_up.DisplayMemberPath = "Site";
                  cnv_current_contract.Visibility = Visibility.Visible;
                  cbx_contrat_up.SelectedIndex = -1;
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
                txtNombre.Text = cont.UserName;
                txtSite.Text = cont.Site;
                txtIssuesList.Text = cont.issuesList;
                txtWorkPackageList.Text = cont.workPackageList;
                txtTaskList.Text = cont.taskList;
            }
            else 
            {
                ShowError("Debe seleccionar un contrato", true);
                ok = false;
            }
            return ok;
        }

        private void CleanPanelContract()
        {
            contName.Text = "Nombre";
            contName.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
            txtNombre.Text = "";
            contSite.Text = "Site";
            txtSite.Text = "";
            contSite.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
            contLstTsk.Text = "Lista de Tareas";
            contLstTsk.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
            txtTaskList.Text = "";
            contLstIssue.Text = "Lista de Incidentes";
            contLstIssue.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
            txtIssuesList.Text = "";
            contLstWP.Text = "Lista de WorkPackage";
            contLstWP.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
            txtWorkPackageList.Text = "";
        }

        private void ShowErrorContract()
        {
            if (txtNombre.Text == "")
            {
                contName.Text = "Nombre *";
                contName.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
            }
            else 
            {
                contName.Text = "Nombre";
                contName.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
            }
            if (txtSite.Text == "")
            {
                contSite.Text = "Site *";
                contSite.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
            }
            else
            {
                contSite.Text = "Site";
                contSite.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
            }
            if (txtTaskList.Text == "")
            {
                contLstTsk.Text = "Lista de Tareas *";
                contLstTsk.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
            }
            else 
            {
                contLstTsk.Text = "Lista de Tareas";
                contLstTsk.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
            }
            if (txtIssuesList.Text == "")
            {
                contLstIssue.Text = "Lista de Incidentes *";
                contLstIssue.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
            }
            else 
            {
                contLstIssue.Text = "Lista de Incidentes";
                contLstIssue.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
            }
            if (txtWorkPackageList.Text == "")
            {
                contLstWP.Text = "Lista de WorkPackage *";
                contLstWP.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
            }
            else
            {
                contLstWP.Text = "Lista de WorkPackage";
                contLstWP.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
            }

        }

        private void BtnConectarContrato_Click(object sender, RoutedEventArgs e)
        {
            DTContract contract;
            contract = (DTContract)lstContratos.SelectedItem;
            url = contract.ContractId;
            //lblConectContract.Text = "Se ha conectado a " + contract.Site;
            cbx_contrat_up.SelectedItem = contract;
            //lblConectContract.Visibility = Visibility.Visible;
            lblacceder_error.Text = "Se ha conectado a " + contract.Site;
            lblacceder_error.Visibility = Visibility.Visible;

            //DTContract contract = (DTContract)lstContratos.SelectedItem;
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.IsContractAvailableCompleted += new EventHandler<IsContractAvailableCompletedEventArgs>(ws_IsContractAvailableCompleted);
            ws.IsContractAvailableAsync(contract.ContractId);        

        }

        void ws_IsContractAvailableCompleted(object sender, IsContractAvailableCompletedEventArgs e)
        {
            DTContract contract = (DTContract)lstContratos.SelectedItem;
            if (e.Result)
            {
                //lblConectContract.Text = "Se ha conectado a " + contract.Site;
                cbx_contrat_up.SelectedItem = contract;
                //lblConectContract.Visibility = Visibility.Visible;
                url = contract.ContractId;
                my_con = contract;
            }
            else 
            {
                lblConectContract.Text = "Error en la coneccion con " + contract.Site;
                lblConectContract.Visibility = Visibility.Visible;
            }
        }

        private void BtnNuevoContrato_Click(object sender, RoutedEventArgs e)
        {
            ShowError("", false);
            isEdit = false;
            CleanPanelContract();
            pnlEditContrato.Visibility = Visibility.Visible;
            PnlbtnsContrato.Visibility = Visibility.Collapsed;
            PnlActionContrato.Visibility = Visibility.Visible;
        }

        private void BtnModificarContrato_Click(object sender, RoutedEventArgs e)
        {
            CleanPanelContract();
            if (LoadPanelEditContrato())
            {
                isEdit = true;
                pnlEditContrato.Visibility = Visibility.Visible;
                PnlbtnsContrato.Visibility = Visibility.Collapsed;
                PnlActionContrato.Visibility = Visibility.Visible;
            }
        }

        private void BtnEliminarContrato_Click(object sender, RoutedEventArgs e)
        {
            isEdit = false;
            DTContract cont = (DTContract)lstContratos.SelectedItem;
            MessageBoxResult msg = MessageBox.Show("Esta seguro que desea eliminar este contrato?", "Contrato", MessageBoxButton.OKCancel);
            if (msg == MessageBoxResult.OK)
            {
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.DeleteContractCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_DeleteContractCompleted);
                ws.DeleteContractAsync(cont.ContractId);
            }
            else 
            {
                pnlEditContrato.Visibility = Visibility.Collapsed;
                PnlbtnsContrato.Visibility = Visibility.Visible;
                PnlActionContrato.Visibility = Visibility.Collapsed;
            }
        }

        void ws_DeleteContractCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            GetContract();
        }

        bool ValidarCampos(DTContract cont)
        {
            return ((cont.UserName != "") && (cont.Site != "") && (cont.workPackageList != "") &&
                    (cont.taskList != "") && (cont.issuesList != ""));
        }

        private void BtnAceptarContrato_Click(object sender, RoutedEventArgs e)
        {
            if (!isEdit)
            {
                DTContract cont = new DTContract
                {
                    UserName = txtNombre.Text,
                    Site = txtSite.Text,
                    issuesList = txtIssuesList.Text,
                    workPackageList = txtWorkPackageList.Text,
                    taskList = txtTaskList.Text
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
                    ShowErrorContract();
                    pnlEditContrato.Visibility = Visibility.Visible;
                    PnlbtnsContrato.Visibility = Visibility.Collapsed;
                    PnlActionContrato.Visibility = Visibility.Visible;
                }
            }
            else if (isEdit)
            {
                DTContract cont = (DTContract)lstContratos.SelectedItem;

                DTContract c = new DTContract();
                c.ContractId = cont.ContractId.Trim();
                c.Site = txtSite.Text;
                c.UserName = txtNombre.Text;
                c.taskList = txtTaskList.Text;
                c.workPackageList = txtWorkPackageList.Text;
                c.issuesList = txtIssuesList.Text;

                if (ValidarCampos(c))
                {
                    isEdit = false;
                    WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                    ws.ModifyContractCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_ModifyContractCompleted);
                    ws.ModifyContractAsync(c);

                    PnlbtnsContrato.Visibility = Visibility.Visible;
                    pnlEditContrato.Visibility = Visibility.Collapsed;
                    PnlActionContrato.Visibility = Visibility.Collapsed;
                }
                else 
                {
                    ShowErrorContract();
                    pnlEditContrato.Visibility = Visibility.Visible;
                    PnlbtnsContrato.Visibility = Visibility.Collapsed;
                    PnlActionContrato.Visibility = Visibility.Visible;
                }
            }
            GetContract();
            scroll_CONTRACT.ScrollToVerticalOffset(0);
        }

        void ws_ModifyContractCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
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

        private void ViewContrat()
        {
            EnableOption(Option.CONTRACT);
            forReport = false;
            GetContract();
        }

        #endregion

        #region WorkPackage

        private void ViewWorkpakage()
        {
            ClearReport();
            if (url != null)
            {
                EnableOption(Option.WP);
                GetWPS();
            }
            else
            {
                ShowError("Debe conectarse previamente a un contrato.", true);
            }
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
            //Dispatcher.BeginInvoke(LoadWPS(e.Result));
            UIThread.Run(delegate() { LoadWPS(e.Result);});
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
                    if ((!field.Hidden) || (field.Name == "ID" && field.Hidden))  
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
            //grd_WP.ItemsSource = lstWP;
            //if (grd_WP.Columns.Count != 0)
            //{
            //    grd_WP.Columns[0].Visibility = Visibility.Collapsed;
            //}
            grd_WP.IsReadOnly = true;
            grd_WP.CanUserResizeColumns = false;
            PnlOption_WP.Visibility = Visibility.Visible;
            pager_wp.ItemsControl = grd_WP;
            pager_wp.ItemsSource = lstWP;
            my_lstWP = lstWP;
            return null;
        }

        private void BtnNuevoWP_Click(object sender, RoutedEventArgs e)
        {
            isEdit = false;
            PnlOption_WP.Visibility = Visibility.Collapsed;
            PnlForm_WP.Visibility = Visibility.Visible;
            progress.play();
            //PnlForm_WP.Children.Add(progress);
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
            MessageBoxResult msg = MessageBox.Show("Esta seguro que desea eliminar este Workpackage?", "Workpackage", MessageBoxButton.OKCancel);
            if (msg == MessageBoxResult.OK)
            {
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.DeleteWorkPackageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_DeleteWorkPackageCompleted);
                ws.DeleteWorkPackageAsync(id, url);
            }
            else
            {
                PnlForm_WP.Children.Clear();
                PnlForm_WP.Visibility = Visibility.Collapsed;
                PnlAction_WP.Visibility = Visibility.Collapsed;
                PnlForm_WP.Visibility = Visibility.Visible;
                PnlOption_WP.Visibility = Visibility.Visible;
            }
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
            isEdit = true;
            string strMy_pnl = "PnlForm_" + Option.WP;
            StackPanel my_pnl = (StackPanel)GridPrincipal.FindName(strMy_pnl);
            PnlOption_WP.Visibility = Visibility.Collapsed;
            if (item.Fields == null)
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
            scroll_WP.ScrollToVerticalOffset(0);
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
            scroll_WP.ScrollToVerticalOffset(0);
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
            PnlOption_WP.Visibility = Visibility.Visible;
        }

        private void BtnApplyWP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.ApplyChangesCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_ApplyChangesWPCompleted);
                ws.ApplyChangesAsync(url, ItemType.WORKPACKAGE);
            }
            catch (Exception exp)
            {
                ShowError("Error al impactar cambios:" + exp.Message, true);
            }
        }

        void ws_ApplyChangesWPCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            GetWPS();
        }

        private void BtnViewIncidentsWP_Click(object sender, RoutedEventArgs e)
        {
            if (grd_WP.SelectedItem == null)
            {
                MessageBoxResult msg = MessageBox.Show("Debe seleccionar un workpackage.", "ERROR", MessageBoxButton.OK);
            }
            else
            {
                WorkPackage wp = (WorkPackage)grd_WP.SelectedItem;
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.GetIssuesWPCompleted += new EventHandler<GetIssuesWPCompletedEventArgs>(ws_GetIssuesWPCompleted);
                ws.GetIssuesWPAsync(url, (wp.Id).ToString());
                //ws.GetIssuesWPAsync(url, wp.Title);
            }

        }

        void ws_GetIssuesWPCompleted(object sender, GetIssuesWPCompletedEventArgs e)
        {
            isIssueWP = true;
            //Dispatcher.BeginInvoke(LoadIncidents(e.Result));
            UIThread.Run(delegate() { LoadIncidents(e.Result);});
        }

        #endregion
        
        #region Incident

        private void ViewIncident()
        {
            ClearReport();
            if (url != null)
            {
                EnableOption(Option.INCIDENT);
                GetIncidents();
            }
            else
            {
                ShowError("Debe conectarse previamente a un contrato.", true);
            }
        }

        private void GetIncidents()
        {
            grd_INCIDENT.Columns.Clear();
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.GetIssuesCompleted += new EventHandler<Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesCompletedEventArgs>(ws_GetIssuesCompleted);
            ws.GetIssuesAsync(url);
        }

        void ws_GetIssuesCompleted(object sender, Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesCompletedEventArgs e)
        {
            lstItem = e.Result;
            //Dispatcher.BeginInvoke(LoadIncidents(e.Result));
            UIThread.Run(delegate() {LoadIncidents(e.Result);});
        }

        private Delegate LoadIncidents(List<DTItem> list)
        {
            Issue i;
            //List<Issue> lstIssue = new List<Issue>();
            lstIssue = new List<Issue>();

            foreach (DTItem issue in list)
            {
                i = new Issue();

                foreach (DTField field in issue.Fields)
                {
                    if ((!field.Hidden) || (field.Name == "ID" && field.Hidden))
                    {
                        switch (field.Name)
                        {
                            case "ID":
                                i.Id = int.Parse(((DTFieldCounter)field).Value.ToString());
                                break;
                            case "Title":
                                i.Title = ((DTFieldAtomicString)field).Value;
                                break;
                            case "Status":
                                i.Status = ((DTFieldChoice)field).Value;
                                break;
                            case "Priority":
                                i.Priority = ((DTFieldChoice)field).Value;
                                break;
                            case "Category":
                                i.Category = ((DTFieldChoice)field).Value;
                                break;
                            case "Reported Date":
                                i.ReportedDate = ((DTFieldAtomicDateTime)field).Value;
                                break;
                            case "Work Package":
                                i.WorkPackage = ((DTFieldChoiceLookup)field).Value;
                                break;
                            case "Reported by":
                                i.ReportedBy = ((DTFieldChoice)field).Value;
                                break;
                            case "Assigned To":
                                i.AssignedTo = ((DTFieldChoice)field).Value;
                                break;
                            case "Priority Order":
                                i.PriorityOrder = double.Parse(((DTFieldAtomicNumber)field).Value.ToString());
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

            if (isIssueWP) // para ver los incidentes de un wp
            {
                if (grd_INCIDENT_WP.Columns.Count != 0)
                {
                    grd_INCIDENT_WP.Columns[0].Visibility = Visibility.Collapsed;
                }
                grd_INCIDENT_WP.IsReadOnly = true;
                grd_INCIDENT_WP.CanUserResizeColumns = false;
                grd_INCIDENT_WP.Visibility = Visibility.Visible;

                pager_incident_wp.ItemsControl = grd_INCIDENT_WP;
                pager_incident_wp.ItemsSource = lstIssue;
                pager_incident_wp.Visibility = Visibility.Visible;
            }
            else
            {
                if (grd_INCIDENT.Columns.Count != 0)
                {
                    grd_INCIDENT.Columns[0].Visibility = Visibility.Collapsed;
                }
                grd_INCIDENT.IsReadOnly = true;
                grd_INCIDENT.CanUserResizeColumns = false;

                pager.ItemsControl = grd_INCIDENT;
                pager.ItemsSource = lstIssue;
            }
            return null;
        }

        void grd_INCIDENT_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //bool show = ColumnsToShow != null && ColumnsToShow.Contains(e.Property.Name.ToLower().Trim());
            
            //e.Cancel = !show;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        { }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            PnlForm_INCIDENT.Children.Clear();
            PnlForm_INCIDENT.Visibility = Visibility.Collapsed;
            PnlAction_INCIDENT.Visibility = Visibility.Collapsed;
            PnlForm_INCIDENT.Visibility = Visibility.Visible;
            PnlOption_INCIDENT.Visibility = Visibility.Visible;
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            isEdit = false;
            PnlOption_INCIDENT.Visibility = Visibility.Collapsed;
            PnlForm_INCIDENT.Visibility = Visibility.Visible;
            progress.play();
            //PnlForm_INCIDENT.Children.Add(progress);
            LoadFormsIncedent();
            progress.stop();
        }

        private void LoadFormsIncedent()
        {
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.GetIssueTemplateCompleted += new EventHandler<GetIssueTemplateCompletedEventArgs>(ws_GetIssueTemplateCompleted);
            ws.GetIssueTemplateAsync(url);
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
            scroll_INCIDENT.ScrollToVerticalOffset(0);
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
            isEdit = true;
            string strMy_pnl = "PnlForm_" + Option.INCIDENT;
            StackPanel my_pnl = (StackPanel)GridPrincipal.FindName(strMy_pnl);
            //progress.play();
            //my_pnl.Children.Add(progress);
            PnlOption_INCIDENT.Visibility = Visibility.Collapsed;
            if (item.Fields == null)
            {
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.GetIssueTemplateCompleted += new EventHandler<GetIssueTemplateCompletedEventArgs>(ws_GetIssueTemplateCompleted2);
                ws.GetIssueTemplateAsync(url);
            }
            else
            {
                LoadChangeFields(Option.INCIDENT);
            }
            PnlAction_INCIDENT.Visibility = Visibility.Visible;
            scroll_INCIDENT.ScrollToVerticalOffset(0);
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
            MessageBoxResult msg = MessageBox.Show("Esta seguro que desea eliminar este Incidente?", "Incidente", MessageBoxButton.OKCancel);
            if (msg == MessageBoxResult.OK)
            {
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.DeleteIssueCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_DeleteIssueCompleted);
                ws.DeleteIssueAsync(id, url);
            }
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
            double width = 180;
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
                        txt.Margin = new Thickness(0, 10, 0, 10);
                        numCtrl = numCtrl + 1;
                        row = row + 1;

                        if (field is DTFieldAtomicBoolean)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;

                            CheckBox chk = new CheckBox();
                            chk.SetValue(NameProperty, "chk_" + field.Name);
                            chk.Width = 40;
                            chk.Margin = new Thickness(0,10, 0, 10);
                            chk.SetValue(Grid.ColumnProperty, 1);

                            txt.SetValue(Grid.RowProperty, row);
                            txt.SetValue(Grid.ColumnProperty, 0);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(chk);
                        }
                        else if (field is DTFieldChoiceLookup)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            //ListBox chlkp = new ListBox();
                            ComboBox chlkp = new ComboBox();
                            chlkp.SetValue(NameProperty, "chlkp_" + field.Name);
                            chlkp.ItemsSource = ((DTFieldChoiceLookup)field).Choices;
                            chlkp.Width = 150;
                            chlkp.Height = 20;
                            chlkp.Margin = new Thickness(0, 10, 0, 10);
                            if (field.Required)
                            {
                                chlkp.SelectedIndex = -1;
                            }
                            else
                            {
                                chlkp.SelectedIndex = 0;
                                chlkp.UpdateLayout();
                            }
                            //chlkp.SelectedIndexWorkaround();
                            chlkp.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(chlkp);
                        }
                        else if (field is DTFieldChoiceUser)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            //ListBox chuser = new ListBox();
                            ComboBox chuser = new ComboBox();
                            chuser.SetValue(NameProperty, "chuser_" + field.Name);
                            chuser.ItemsSource = ((DTFieldChoiceUser)field).Choices;
                            chuser.Width = 150;
                            chuser.Height = 20;
                            chuser.Margin = new Thickness(0, 10, 0, 10);
                            if (field.Required)
                            {
                                chuser.SelectedIndex = -1;
                            }
                            else
                            {
                                chuser.SelectedIndex = 0;
                                chuser.UpdateLayout();
                            }
                            //chuser.SelectedIndexWorkaround();
                            chuser.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(chuser);
                        }
                        else if (field is DTFieldChoice)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            //ListBox lstbx = new ListBox();
                            ComboBox lstbx = new ComboBox();
                            lstbx.SetValue(NameProperty, "lstbx_" + field.Name);
                            lstbx.Width = 150;
                            lstbx.Height = 20;
                            lstbx.Margin = new Thickness(0, 10, 0, 10);
                            lstbx.ItemsSource = ((DTFieldChoice)field).Choices;
                            if (field.Required)
                            {
                                lstbx.SelectedIndex = -1;
                            }
                            else
                            {
                                lstbx.SelectedIndex = 0;
                                lstbx.UpdateLayout();
                            }
                            //lstbx.SelectedIndexWorkaround();
                            lstbx.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(lstbx);
                        }
                        else if (field is DTFieldAtomicDateTime)
                        {
                            numCtrl = numCtrl + 3;
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
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
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox nt = new TextBox();
                            nt.SetValue(NameProperty, "nt_" + field.Name);
                            nt.Text = "";
                            nt.Width = 300;
                            nt.Height = 100;
                            nt.AcceptsReturn = true;
                            nt.Margin = new Thickness(0, 10, 0, 10);
                            nt.TextWrapping = TextWrapping.Wrap;
                            nt.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(nt);
                        }
                        else if (field is DTFieldAtomicNumber)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox num = new TextBox();
                            num.SetValue(NameProperty, "num_" + field.Name);
                            num.Text = "";
                            num.Width = 150;
                            num.Margin = new Thickness(0, 10, 0, 10);
                            num.TextWrapping = TextWrapping.Wrap;
                            num.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(num);
                        }
                        else if (field is DTFieldCounter)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox cnt = new TextBox();
                            cnt.SetValue(NameProperty, "cnt_" + field.Name);
                            cnt.Text = "";
                            cnt.Width = 80;
                            cnt.Margin = new Thickness(0, 10, 0, 10);
                            cnt.TextWrapping = TextWrapping.Wrap;
                            cnt.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(cnt);
                        }
                      
                        else 
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox bx = new TextBox();
                            bx.SetValue(NameProperty, "bx_" + field.Name);
                            bx.Text = "";
                            bx.Width = 300;
                            bx.Margin = new Thickness(0, 10, 0, 10);
                            bx.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(bx);
                        }                       

                        my_pnl.Children.Add(newGrd);
                    }
                }
                //my_pnl.Children.Remove(progress);
                progress.stop();
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
                            resultField.InternalName = field.InternalName;
                            resulItem.Fields.Add(resultField);
                        }
                        else if (field is DTFieldChoiceLookup)
                        {
                            ComboBox chlkp = (ComboBox)my_pnl.FindName("chlkp_" + field.Name);
                            TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                            if ((field.Required) && (chlkp.SelectedItem == null))
                            {
                                txt.Text = field.Name + "*";
                                txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
                                ok = false;
                            }
                            else
                            {
                                DTFieldChoiceLookup resultField = new DTFieldChoiceLookup();
                                resultField.Value = chlkp.SelectedItem.ToString();
                                resultField.LookupField = ((DTFieldChoiceLookup)field).LookupField;
                                resultField.LookupList = ((DTFieldChoiceLookup)field).LookupList;
                                resultField.Choices = ((DTFieldChoiceLookup)field).Choices;
                                resultField.Name = field.Name;
                                resultField.InternalName = field.InternalName;
                                resulItem.Fields.Add(resultField);
                                txt.Text = field.Name;
                                txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
                            }
                        }
                        else if (field is DTFieldChoiceUser)
                        {
                            ComboBox chuser = (ComboBox)my_pnl.FindName("chuser_" + field.Name);
                            TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                            if ((field.Required) && (chuser.SelectedItem == null))
                            {
                                txt.Text = field.Name + " *";
                                txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
                                ok = false;
                            }
                            else
                            {
                                DTFieldChoiceUser resultField = new DTFieldChoiceUser();
                                resultField.Value = chuser.SelectedItem.ToString();
                                resultField.Choices = ((DTFieldChoiceUser)field).Choices;
                                resultField.Name = field.Name;
                                resultField.InternalName = field.InternalName;
                                resulItem.Fields.Add(resultField);
                                txt.Text = field.Name;
                                txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
                            }
                        }
                        else if (field is DTFieldChoice)
                        {
                            TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                            ComboBox lst = (ComboBox)my_pnl.FindName("lstbx_" + field.Name);
                            if ((field.Required) && (lst.SelectedItem == null))
                            {
                                txt.Text = field.Name + " *";
                                txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
                                ok = false;
                            }
                            else
                            {
                                DTFieldChoice resultField = new DTFieldChoice();
                                resultField.Value = lst.SelectedItem.ToString();
                                resultField.Choices = ((DTFieldChoice)field).Choices;
                                resultField.Name = field.Name;
                                resultField.InternalName = field.InternalName;
                                resulItem.Fields.Add(resultField);
                                txt.Text = field.Name;
                                txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
                            }
                        }
                        else if (field is DTFieldAtomicDateTime)
                        {
                            Calendar cal = (Calendar)my_pnl.FindName("cal_" + field.Name);
                            TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                            if ((field.Required) && (cal.SelectedDate.Value.ToShortDateString().ToString() == ""))
                            {
                                txt.Text = field.Name + " *";
                                txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
                                ok = false;
                            }
                            else
                            {
                                DTFieldAtomicDateTime resultField = new DTFieldAtomicDateTime();
                                resultField.Value = cal.SelectedDate.Value;
                                resultField.Name = field.Name;
                                resultField.InternalName = field.InternalName;
                                resulItem.Fields.Add(resultField);
                                txt.Text = field.Name;
                                txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
                            }
                        }
                        else if (field is DTFieldAtomicNote)
                        {
                            TextBox nt = (TextBox)my_pnl.FindName("nt_" + field.Name);
                            TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                            if ((field.Required) && (nt.Text == ""))
                            {
                                txt.Text = field.Name + " *";
                                txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
                                ok = false;
                            }
                            else
                            {
                                DTFieldAtomicNote resultField = new DTFieldAtomicNote();
                                resultField.Value = nt.Text;
                                resultField.Name = field.Name;
                                resultField.InternalName = field.InternalName;
                                resulItem.Fields.Add(resultField);
                                txt.Text = field.Name;
                                txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
                            }
                        }
                        else if (field is DTFieldAtomicNumber)
                        {
                            TextBox num = (TextBox)my_pnl.FindName("num_" + field.Name);
                            TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                            try
                            {
                                double value = double.Parse(num.Text.Trim());

                                DTFieldAtomicNumber resultField = new DTFieldAtomicNumber();
                                resultField.Value = value;
                                resultField.Name = field.Name;
                                resultField.InternalName = field.InternalName;
                                resulItem.Fields.Add(resultField);
                                txt.Text = field.Name;
                                txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
                            }
                            catch (Exception)
                            {
                                if (field.Required) // mal formato o vacio
                                {
                                    if (num.Text == "")
                                        txt.Text = field.Name + " *";
                                    else
                                        txt.Text = field.Name + " (Formato invalido)";
                                    
                                    ok = false;
                                    txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
                                }
                                else if ((num.Text != ""))
                                {
                                    txt.Text = field.Name + " (Formato invalido)";
                                    txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
                                    ok = false;
                                }
                            }
                        }
                        else if (field is DTFieldCounter)
                        {
                            TextBox cnt = (TextBox)my_pnl.FindName("cnt_" + field.Name);
                            TextBlock txt = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                            try
                            {
                                int value = int.Parse(cnt.Text);

                                DTFieldCounter resultField = new DTFieldCounter();
                                resultField.Value = value;
                                resultField.Name = field.Name;
                                resultField.InternalName = field.InternalName;
                                resulItem.Fields.Add(resultField);
                                txt.Text = field.Name;
                                txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
                            }
                            catch (Exception)
                            {
                                if (field.Required)//mal formato o vacio
                                {
                                    if (cnt.Text == "")
                                        txt.Text = field.Name + " *";
                                    else
                                        txt.Text = field.Name + " (Formato invalido)";
                                    
                                    ok = false;
                                    txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
                                }
                                else if ((cnt.Text != ""))
                                {
                                    ok = false;
                                    txt.Text = field.Name + " (Formato invalido)";
                                    txt.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
                                }
                            }
                        }
                        else
                        {
                            TextBox txt = (TextBox)my_pnl.FindName("bx_" + field.Name);
                            TextBlock t = (TextBlock)my_pnl.FindName("txt_" + field.Name);
                            if ((field.Required) && (txt.Text.ToString() == ""))
                            {
                                t.Text = field.Name + " *";
                                t.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush (Colors.Red));
                                ok = false;
                            }
                            else
                            {
                                DTFieldAtomicString resultField = new DTFieldAtomicString();
                                resultField.Value = txt.Text;
                                resultField.Name = field.Name;
                                resultField.InternalName = field.InternalName;
                                resulItem.Fields.Add(resultField);
                                t.Text = field.Name;
                                t.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));
                            }
                        }
                    }
                    else if (field.Name == "ID")
                    {
                        DTFieldCounter resultField = new DTFieldCounter();
                        resultField = ((DTFieldCounter)field);
                        resulItem.Fields.Add(resultField);
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
                        txt.Margin = new Thickness(0, 10, 0, 10);
                        numCtrl = numCtrl + 1;
                        row = row + 1;

                        if (field is DTFieldAtomicBoolean)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;

                            CheckBox chk = new CheckBox();
                            chk.SetValue(NameProperty, "chk_" + field.Name);
                            chk.IsChecked = ((DTFieldAtomicBoolean)field).Value;
                            chk.Width = 40;
                            chk.Margin = new Thickness(0, 10, 0, 10);
                            chk.SetValue(Grid.ColumnProperty, 1);

                            txt.SetValue(Grid.RowProperty, row);
                            txt.SetValue(Grid.ColumnProperty, 0);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(chk);
                        }
                        else if (field is DTFieldChoiceLookup)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            //ListBox chlkp = new ListBox();
                            ComboBox chlkp = new ComboBox();
                            chlkp.SetValue(NameProperty, "chlkp_" + field.Name);
                            chlkp.ItemsSource = ((DTFieldChoiceLookup)field).Choices;
                            chlkp.SelectedIndex = chlkp.Items.IndexOf(((DTFieldChoiceLookup)field).Value);
                            chlkp.UpdateLayout();
                            //chlkp.SelectedIndexWorkaround();
                            chlkp.Width = 150;
                            chlkp.Height = 20;
                            chlkp.Margin = new Thickness(0, 10, 0, 10);
                            chlkp.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(chlkp);
                        }
                        else if (field is DTFieldChoiceUser)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            //ListBox chuser = new ListBox();
                            ComboBox chuser = new ComboBox();
                            chuser.SetValue(NameProperty, "chuser_" + field.Name);
                            chuser.ItemsSource = ((DTFieldChoiceUser)field).Choices;
                            chuser.SelectedIndex = chuser.Items.IndexOf(((DTFieldChoiceUser)field).Value);
                            chuser.UpdateLayout();
                            //chuser.SelectedIndexWorkaround();
                            chuser.Width = 150;
                            chuser.Height = 20;
                            chuser.Margin = new Thickness(0, 10, 0, 10);
                            chuser.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(chuser);
                        }
                        else if (field is DTFieldChoice)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            //ListBox lstbx = new ListBox();
                            ComboBox lstbx = new ComboBox();
                            lstbx.SetValue(NameProperty, "lstbx_" + field.Name);
                            lstbx.ItemsSource = ((DTFieldChoice)field).Choices;
                            lstbx.SelectedIndex = lstbx.Items.IndexOf(((DTFieldChoice)field).Value);
                            lstbx.UpdateLayout();
                            //lstbx.SelectedIndexWorkaround();
                            lstbx.Width = 150;
                            lstbx.Height = 20;
                            lstbx.Margin = new Thickness(0, 10, 0, 10);
                            lstbx.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(lstbx);
                        }
                        else if (field is DTFieldAtomicDateTime)
                        {
                            numCtrl = numCtrl + 3;
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            Calendar cal = new Calendar();
                            cal.SetValue(NameProperty, "cal_" + field.Name);
                            cal.Width = 280;
                            cal.Height = 200;
                            cal.SelectedDate = ((DTFieldAtomicDateTime)field).Value;
                            cal.DisplayDate = cal.SelectedDate.Value;
                            cal.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(cal);
                        }
                        else if (field is DTFieldAtomicNote)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox nt = new TextBox();
                            nt.SetValue(NameProperty, "nt_" + field.Name);
                            nt.Text = ((DTFieldAtomicNote)field).Value;
                            nt.Width = 300;
                            nt.Height = 100;
                            nt.AcceptsReturn = true;
                            nt.Margin = new Thickness(0, 10, 0, 10);
                            nt.TextWrapping = TextWrapping.Wrap;
                            nt.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(nt);
                        }
                        else if (field is DTFieldAtomicNumber)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox num = new TextBox();
                            num.SetValue(NameProperty, "num_" + field.Name);
                            num.Text = ((DTFieldAtomicNumber)field).Value.ToString();
                            num.Width = 150;
                            num.Margin = new Thickness(0, 10, 0, 10);
                            num.TextWrapping = TextWrapping.Wrap;
                            num.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(num);
                        }
                        else if (field is DTFieldCounter)
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox cnt = new TextBox();
                            cnt.SetValue(NameProperty, "cnt_" + field.Name);
                            cnt.Text = ((DTFieldCounter)field).Value.ToString();
                            cnt.Width = 80;
                            cnt.Margin = new Thickness(0, 10, 0, 10);
                            cnt.TextWrapping = TextWrapping.Wrap;
                            cnt.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(cnt);
                        }
                        else
                        {
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Width = width;
                            txt.SetValue(Grid.ColumnProperty, 0);

                            TextBox bx = new TextBox();
                            bx.SetValue(NameProperty, "bx_" + field.Name);
                            bx.Text = ((DTFieldAtomicString)field).Value;
                            bx.Width = 300;
                            bx.Margin = new Thickness(0, 10, 0, 10);
                            bx.SetValue(Grid.ColumnProperty, 1);

                            newGrd.Children.Add(txt);
                            newGrd.Children.Add(bx);
                        }

                        my_pnl.Children.Add(newGrd);
                    }
                }
                //my_pnl.Children.Remove(progress);
                progress.stop();
            }
            return ok;
        }

        private void BtnApplyINCIDENT_Click(object sender, RoutedEventArgs e)
        {
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.ApplyChangesCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_ApplyChangesINCIDENTCompleted);
            ws.ApplyChangesAsync(url, ItemType.ISSUE);
        }

        void ws_ApplyChangesINCIDENTCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            GetIncidents();
        }

        #endregion

        #region Task

        public void ViewTasks()
        {
            ClearReport();
            if (url != null)
            {
                EnableOption(Option.TASK);
                GetTask();
            }
            else
            {
                ShowError("Debe conectarse previamente a un contrato.", true);
            }
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
            lstItem = e.Result;
            //Dispatcher.BeginInvoke(LoadTask(e.Result));
            UIThread.Run(delegate() { LoadTask(e.Result); });
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
                    if ((!field.Hidden) || (field.Name == "ID" && field.Hidden))  
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
                            case "% Complete":
                                t.PorComplete = ((DTFieldAtomicNumber)field).Value;
                                break;
                            case "Assigned To":
                                t.AssignedTo = ((DTFieldChoice)field).Value;
                                break;
                            case "Start Date":
                                t.StartDate = ((DTFieldAtomicDateTime)field).Value;
                                break;
                            case "Due Date":
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
            //grd_TASK.ItemsSource = lstTask;
            pager_tasks.ItemsControl = grd_TASK;
            pager_tasks.ItemsSource = lstTask;
            my_lstTask = lstTask;
            if (grd_TASK.Columns.Count != 0)
            {
                grd_TASK.Columns[0].Visibility = Visibility.Collapsed;
            }
            grd_TASK.IsReadOnly = true;
            grd_TASK.CanUserResizeColumns = false;
            return null;
        }

        private void BtnNuevoTASK_Click(object sender, RoutedEventArgs e)
        {
            isEdit = false;
            PnlOption_TASK.Visibility = Visibility.Collapsed;
            PnlForm_TASK.Visibility = Visibility.Visible;
            progress.play();
            //PnlForm_TASK.Children.Add(progress);
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
            isEdit = true;
            string strMy_pnl = "PnlForm_" + Option.TASK;
            StackPanel my_pnl = (StackPanel)GridPrincipal.FindName(strMy_pnl);
            progress.play();
            //my_pnl.Children.Add(progress);
            PnlOption_TASK.Visibility = Visibility.Collapsed;
            if (item == null)
            {
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.GetTaskTemplateCompleted +=new EventHandler<GetTaskTemplateCompletedEventArgs>(ws_GetTaskTemplateCompleted2);
                ws.GetTaskTemplateAsync(url);
            }
            else
            {
                LoadChangeFields(Option.TASK);
            }
            PnlAction_TASK.Visibility = Visibility.Visible;
            scroll_TASK.ScrollToVerticalOffset(0);
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
            MessageBoxResult msg = MessageBox.Show("Esta seguro que desea eliminar este Incidente?", "Incidente", MessageBoxButton.OKCancel);
            if (msg == MessageBoxResult.OK)
            {
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.DeleteTaskCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_DeleteTaskCompleted);
                ws.DeleteTaskAsync(id, url);
            }
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
            scroll_TASK.ScrollToVerticalOffset(0);
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
            StackPanel cnv = (StackPanel)CanvasTASK.FindName(strPnl);
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

        private void BtnApplyTASK_Click(object sender, RoutedEventArgs e)
        {
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.ApplyChangesCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_ApplyChangesTASKCompleted);
            ws.ApplyChangesAsync(url, ItemType.TASK);
        }

        void ws_ApplyChangesTASKCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            GetTask();
        }

        #endregion

        #region Reports

        #region Desviacion de work package

        private void ViewReportDESWP()
        {
            cal_inicial.SelectedDate = null;
            cal_final.SelectedDate = null;
            BtnGenerateReport_ISSUES.Visibility = Visibility.Collapsed;
            BtnExportar_ISSUES.Visibility = Visibility.Collapsed;
            pager_grd_REPORT_ISSUES.Visibility = Visibility.Collapsed;
            EnableOption(Option.REPORT);
            forReport = true;
            GetContract();
            BtnGenerateReport_DESWP.Visibility = Visibility.Visible;        
        }
        
        private void BtnGenerateReportClick_DESWP(object sender, RoutedEventArgs e)
        {
            ShowError("", false);
            if ((cal_inicial.SelectedDate == null || 
                cal_final.SelectedDate == null) || 
                (contractsReport.SelectedItem == null))
            {
                ShowError("Debe ingresar todos los datos.", true);
            }
            else
            {
                DTContract contract = (DTContract)contractsReport.SelectedItem;
                DateTime fch_inicial = cal_inicial.SelectedDate.Value;
                DateTime fch_final = cal_final.SelectedDate.Value;
                if (fch_final < fch_inicial)
                {
                    MessageBoxResult msg = MessageBox.Show("Fecha fin debe ser mayor a fecha inicio.", "ERROR", MessageBoxButton.OK);
                }
                else
                {
                    LoadReportResult_DESWP(contract.ContractId, fch_inicial, fch_final);
                }
            }
        }

        private void LoadReportResult_DESWP(string contractId, DateTime fch_inicial, DateTime fch_final)
        {
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.ReportDesvWorkPackageCompleted += new EventHandler<ReportDesvWorkPackageCompletedEventArgs>(ws_ReportDesvWorkPackageCompleted);
            ws.ReportDesvWorkPackageAsync(contractId,fch_inicial,fch_final);
        }

        void ws_ReportDesvWorkPackageCompleted(object sender, ReportDesvWorkPackageCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                grd_REPORT.Columns.Clear();
                grd_REPORT.Visibility = Visibility.Visible;
                pager_grd_REPORT_DESWP.ItemsControl = grd_REPORT;
                pager_grd_REPORT_DESWP.ItemsSource = e.Result;
                pager_grd_REPORT_DESWP.Visibility = Visibility.Visible;
                my_lstReport_deswp = e.Result;
                PnlExportar_REPORT.Visibility = Visibility.Visible;
                BtnExportar_DESWP.Visibility = Visibility.Visible;
            }
        }

        private void BtnExportar_Click_DESWP(object sender, RoutedEventArgs e)
        {
            //List<DTWorkPackageReport> lst = (List<DTWorkPackageReport>)grd_REPORT.ItemsSource;
            List<DTWorkPackageReport> lst = (List<DTWorkPackageReport>)pager_grd_REPORT_DESWP.ItemsSource;
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.ExportDesWPCompleted +=new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_ExportDesWPCompleted);
            ws.ExportDesWPAsync(lst);
        }

        void ws_ExportDesWPCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {}

        #endregion

        #region Incidentes

        private void ViewReportISSUESREPORT(bool isOneContract)
        {
            cal_inicial.SelectedDate = null;
            cal_final.SelectedDate = null;
            BtnGenerateReport_DESWP.Visibility = Visibility.Collapsed;
            BtnExportar_DESWP.Visibility = Visibility.Collapsed;
            pager_grd_REPORT_DESWP.Visibility = Visibility.Collapsed;
            EnableOption(Option.REPORT);
            forReport = true;
            if (isOneContract)
            {
                GetContract();
            }
            BtnGenerateReport_ISSUES.Visibility = Visibility.Visible;
        }
                
        private void BtnGenerateReportClick_ISSUES(object sender, RoutedEventArgs e)
        {
            ShowError("", false);
            if ((cal_inicial.SelectedDate == null ||
                cal_final.SelectedDate == null) ||
                (oneContract &&(contractsReport.SelectedItem == null)))
            {
                ShowError("Debe ingresar todos los datos.", true);
            }
            else
            {
                DateTime fch_inicial = cal_inicial.SelectedDate.Value;
                DateTime fch_final = cal_final.SelectedDate.Value;
                if (fch_final < fch_inicial)
                {
                    MessageBoxResult msg = MessageBox.Show("Fecha fin debe ser mayor a fecha inicio.", "ERROR", MessageBoxButton.OK);
                }
                else
                {
                    String contractId = null;
                    if (oneContract)
                        contractId = ((DTContract)contractsReport.SelectedItem).ContractId;
                    LoadReportResult_ISSUES(contractId, fch_inicial, fch_final);
                }
            }
        }

        private void LoadReportResult_ISSUES(string contractId, DateTime fch_inicial, DateTime fch_final)
        {
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.IssuesReportCompleted += new EventHandler<IssuesReportCompletedEventArgs>(ws_IssuesReportCompleted);
            ws.IssuesReportAsync(contractId, fch_inicial, fch_final);
        }

        void ws_IssuesReportCompleted(object sender, IssuesReportCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                grd_REPORT.Columns.Clear();
                grd_REPORT.Visibility = Visibility.Visible;
                pager_grd_REPORT_ISSUES.ItemsControl = grd_REPORT;
                pager_grd_REPORT_ISSUES.ItemsSource = e.Result;
                pager_grd_REPORT_ISSUES.Visibility = Visibility.Visible;
                my_lstReport_issues = e.Result;
                PnlExportar_REPORT.Visibility = Visibility.Visible;
                BtnExportar_ISSUES.Visibility = Visibility.Visible;
            }
        }

        private void BtnExportar_Click_ISSUES(object sender, RoutedEventArgs e)
        {
            List<DTReportedItem> lst = (List<DTReportedItem>)grd_REPORT.ItemsSource;
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.ExportISSUESCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_ExportISSUESCompleted);
            ws.ExportISSUESAsync(lst);
        }

        void ws_ExportISSUESCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {}
        
        #endregion

        #endregion

        private void lnk_acceder_click(object sender, RoutedEventArgs e)
        {
            DTContract contract = (DTContract)cbx_contrat_up.SelectedItem;
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.IsContractAvailableCompleted += new EventHandler<IsContractAvailableCompletedEventArgs>(ws_IsContractAvailableCompleted2);
            ws.IsContractAvailableAsync(contract.ContractId);
        }

        void ws_IsContractAvailableCompleted2(object sender, IsContractAvailableCompletedEventArgs e)
        {
            DTContract contract = (DTContract)cbx_contrat_up.SelectedItem;
            url = contract.ContractId;
            if (e.Result)
            {
                cbx_contrat_up.SelectedItem = contract;
                my_con = contract;
                lblacceder_error.Text = "Se ha conectado a " + contract.Site;
                url = contract.ContractId;
                lblacceder_error.Visibility = Visibility.Visible;
            }
            else 
            {
                lblacceder_error.Text = "Error en la coneccion con " + contract.Site;
                lblacceder_error.Visibility = Visibility.Visible;
            }
        }

        #region Por Impactar
        
        public void ViewIncidentesPorAplicar()
        { 

        }

        public void ViewWorkpakagePorAplicar()
        {
        }

        public void ViewTasksPorAplicar()
        {
        }

        #endregion

        #region Mapeo de Valores

        public void ViewMap()
        {
            BtnMap.Visibility = Visibility.Visible;
            CanvasMap.Visibility = Visibility.Visible;
            PnlActionMap.Visibility = Visibility.Visible;
            DTContract c = (DTContract)cbx_contrat_up.SelectedItem;
            txt_endValue_map.Text = "endValue";
            txt_endValue_map.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));

            txt_property_map.Text = "Property";
            txt_property_map.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));

            txt_initialValue_map.Text = "Initial Value";
            txt_initialValue_map.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));

            BusacrTempletes(c);
        }

        public void ViewIssueMap()
        {
            titulo_Map.Text = "Mapeo de Valores para Incidentes";
            map = "Issue";
            ViewMap();
        }

        public void ViewTaskMap()
        {
            titulo_Map.Text = "Mapeo de Valores para Tasks";
            map = "Task";
            ViewMap();
        }

        public void ViewWorkpackageMap()
        {
            titulo_Map.Text = "Mapeo de Valores para Workpackage";
            map = "WP";
            ViewMap();
        }

        void BusacrTempletes(DTContract c)
        {
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            if (c != null)
            {

                if (map == "Issue")
                {
                    ws.GetIssueTemplateCompleted += new EventHandler<GetIssueTemplateCompletedEventArgs>(ws_GetIssueTemplateCompletedMap);
                    ws.GetIssueTemplateAsync(c.ContractId);
                }
                else if (map == "Task")
                {
                    ws.GetTaskTemplateCompleted += new EventHandler<GetTaskTemplateCompletedEventArgs>(ws_GetTaskTemplateCompletedMap);
                    ws.GetTaskTemplateAsync(c.ContractId);
                }
                else if (map == "WP")
                {
                    ws.GetWorkPackageTemplateCompleted += new EventHandler<GetWorkPackageTemplateCompletedEventArgs>(ws_GetWorkPackageTemplateCompletedMap);
                    ws.GetWorkPackageTemplateAsync(c.ContractId);
                }
            }
            else
            {
                cmbx_property_map.ItemsSource = null;
                cmbx_initialValue_map.ItemsSource = null;
            }
        }

        void ws_GetTaskTemplateCompletedMap(object sender, GetTaskTemplateCompletedEventArgs e)
        {
            List<DTField> lstField = new List<DTField>();
            foreach (DTField f in e.Result.Fields)
            {
                if ((f is DTFieldChoice) || (f is DTFieldChoiceLookup) || (f is DTFieldChoiceUser))
                {
                    lstField.Add(f);
                }
            }
            cmbx_property_map.ItemsSource = lstField;
            cmbx_property_map.DisplayMemberPath = "Name";
            cmbx_property_map.SelectedIndex = -1;
            cmbx_property_map.Visibility = Visibility.Visible;
            cmbx_property_map.SelectionChanged += new SelectionChangedEventHandler(cmbx_property_map_SelectionChanged);
        }

        void ws_GetWorkPackageTemplateCompletedMap(object sender, GetWorkPackageTemplateCompletedEventArgs e)
        {
            List<DTField> lstField = new List<DTField>();
            foreach (DTField f in e.Result.Fields)
            {
                if ((f is DTFieldChoice) || (f is DTFieldChoiceLookup) || (f is DTFieldChoiceUser))
                {
                    lstField.Add(f);
                }
            }
            cmbx_property_map.ItemsSource = lstField;
            cmbx_property_map.DisplayMemberPath = "Name";
            cmbx_property_map.SelectedIndex = -1;
            cmbx_property_map.Visibility = Visibility.Visible;
            cmbx_property_map.SelectionChanged += new SelectionChangedEventHandler(cmbx_property_map_SelectionChanged);
        }

        void ws_GetIssueTemplateCompletedMap(object sender, GetIssueTemplateCompletedEventArgs e)
        {
            List<DTField> lstField = new List<DTField>();
            foreach (DTField f in e.Result.Fields)
            {
                if ((f is DTFieldChoice) || (f is DTFieldChoiceLookup) || (f is DTFieldChoiceUser))
                {
                    lstField.Add(f);
                }
            }
            cmbx_property_map.ItemsSource = lstField;
            cmbx_property_map.DisplayMemberPath = "Name";
            cmbx_property_map.SelectedIndex = -1;
            cmbx_property_map.Visibility = Visibility.Visible;
            cmbx_property_map.SelectionChanged += new SelectionChangedEventHandler(cmbx_property_map_SelectionChanged);
        }

        void cmbx_property_map_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DTField f = (DTField)cmbx_property_map.SelectedItem;
            if (f is DTFieldChoice) 
            {
                cmbx_initialValue_map.ItemsSource = ((DTFieldChoice)f).Choices;
            }
            else if(f is DTFieldChoiceLookup)
            {
                cmbx_initialValue_map.ItemsSource = ((DTFieldChoiceLookup)f).Choices;
            }
            else if (f is DTFieldChoiceUser)
            {
                cmbx_initialValue_map.ItemsSource = ((DTFieldChoiceUser)f).Choices;
            }
            cmbx_initialValue_map.SelectedIndex = -1;
            cmbx_initialValue_map.Visibility = Visibility.Visible;
        }
        
        private void BtnMap_Click(object sender, RoutedEventArgs e)
        {
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            DTContract c = (DTContract)cbx_contrat_up.SelectedItem;
            DTField f = (DTField)cmbx_property_map.SelectedItem;
            string v = (string)cmbx_initialValue_map.SelectedItem;
            txt_endValue_map.Text = "End Value";
            txt_endValue_map.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));

            txt_property_map.Text = "Property";
            txt_property_map.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));

            txt_initialValue_map.Text = "Initial Value";
            txt_initialValue_map.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Black));


            if (c != null && f != null && v != null && bx_endValue_map.Text != "")
            {
                if (map == "Issue")
                {
                    ws.SiteMapPropertyValueIssuesCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_SiteMapPropertyValueIssuesCompleted);
                    ws.SiteMapPropertyValueIssuesAsync(c.ContractId, f.Name, v, bx_endValue_map.Text);
                }
                else if (map == "Task")
                {
                    ws.SiteMapPropertyValueTasksCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_SiteMapPropertyValueTasksCompleted);
                    ws.SiteMapPropertyValueTasksAsync(c.ContractId, f.Name, v, bx_endValue_map.Text);
                }
                else if (map == "WP")
                {
                    ws.SiteMapPropertyValueWorkPackagesCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_SiteMapPropertyValueWorkPackagesCompleted);
                    ws.SiteMapPropertyValueWorkPackagesAsync(c.ContractId, f.Name, v, bx_endValue_map.Text);
                }
            }
            else if (bx_endValue_map.Text == "")
            {
                txt_endValue_map.Text = txt_endValue_map.Text + "*";
                txt_endValue_map.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
            }
            else if (f == null)
            {
                txt_property_map.Text = txt_property_map.Text + "*";
                txt_property_map.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
            }
            else if (v == null)
            {
                txt_initialValue_map.Text = txt_initialValue_map.Text + "*";
                txt_initialValue_map.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red));
            }
        }

        void ws_SiteMapPropertyValueWorkPackagesCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            bx_endValue_map.Text = "";
        }

        void ws_SiteMapPropertyValueTasksCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            bx_endValue_map.Text = "";
        }

        void ws_SiteMapPropertyValueIssuesCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            bx_endValue_map.Text = "";
        }

        #endregion


    }
 
}