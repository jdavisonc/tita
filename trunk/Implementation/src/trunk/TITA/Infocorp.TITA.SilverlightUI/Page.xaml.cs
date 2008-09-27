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
        private string url ="http://localhost/infocorp";
        private List<DTIssue> my_issue = new List<DTIssue>();
        private DTIssue my_issue_template = null;
        List<DTContract> my_contract = new List<DTContract>();
        private bool isEdit;
        private bool isDelete;

        public Page()
        {
            InitializeComponent();
            ViewPendingChanges();
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
            lblPending.Visibility = Visibility.Visible;
            lblPending.Text = "Hay cambios pendientes";
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
            }
        }

        private void LoadLstContratos(List<DTContract> my_contract)
        {
            if (my_contract.Count() != 0)
                lstContratos.ItemsSource = my_contract;
        }

        private bool LoadPanelEditContrato()
        {
            bool ok = true;
            if (lstContratos.SelectedIndex != -1)
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
            if (LoadPanelEditContrato())
            {
                isEdit = true;
                isDelete = false;
                pnlEditContrato.Visibility = Visibility.Visible;
                PnlbtnsContrato.Visibility = Visibility.Collapsed;
                PnlActionContrato.Visibility = Visibility.Visible;
            }
            else 
            {

            }
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

        private void BtnAceptarContrato_Click(object sender, RoutedEventArgs e)
        {
            if (!isEdit)
            {
                DTContract cont = new DTContract
                {
                    UserName = txtNombre.Text.ToString(),
                    Site = txtUrl.Text.ToString()
                };

                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.AddNewContractCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_AddNewContractCompleted);
                ws.AddNewContractAsync(cont);
                
                PnlbtnsContrato.Visibility = Visibility.Visible;
                pnlEditContrato.Visibility = Visibility.Collapsed;
                PnlActionContrato.Visibility = Visibility.Collapsed;

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

        //http://localhost:2030/Infocorp.TITA.SilverlightUIWeb/WSTita.asmx
        //WSTitaReference
        private void ButtonWP_Click(object sender, RoutedEventArgs e)
        {
            EnableOption(Option.WP);
            GetWPS();
        }

        public void GetWPS() 
        {
            grdWP.Columns.Clear();
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.GetWPSCompleted += new EventHandler<Infocorp.TITA.SilverlightUI.WSTitaReference.GetWPSCompletedEventArgs>(ws_GetWPSCompleted);
            ws.GetWPSAsync();
        }

        void ws_GetWPSCompleted(object sender, Infocorp.TITA.SilverlightUI.WSTitaReference.GetWPSCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(LoadWPS(e.Result));
        }

        private Delegate LoadWPS(List<DTIssue> list)
        {
            return null;
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
            if (grdIncident.Columns.Count != 0)
            {
                grdIncident.Columns[0].Visibility = Visibility.Collapsed;
            }
            my_issue = list;
            return null;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            PnlNew.Children.Clear();
            PnlNew.Visibility = Visibility.Collapsed;
            PnlAction.Visibility = Visibility.Collapsed;
            PnlbtnNuevo.Visibility = Visibility.Visible;
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            PnlbtnNuevo.Visibility = Visibility.Collapsed;
            PnlNew.Visibility = Visibility.Visible;
            ShowNewPanel();
            PnlAction.Visibility = Visibility.Visible;
        }

        private void ShowNewPanel()
        {
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.GetIssueTemplateCompleted += new EventHandler<GetIssueTemplateCompletedEventArgs>(ws_GetIssueTemplateCompleted);
            ws.GetIssueTemplateAsync();
        }

        void ws_GetIssueTemplateCompleted(object sender, GetIssueTemplateCompletedEventArgs e)
        {
            isEdit = false;
            Canvas cnv = (Canvas)CanvasIncident.FindName("PnlNew");
            int numCtrl = 0;
            DTIssue issue = e.Result;
            my_issue_template = e.Result;
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
                            txt.Margin = new Thickness(50, numCtrl * 50, 0, 0);
                            txt.Width = 80;

                            ListBox lstbx = new ListBox();
                            lstbx.SetValue(NameProperty, "lstbx_" + field.Name);
                            lstbx.Margin = new Thickness(140, numCtrl * 50, 0, 0);
                            lstbx.Width = 80;
                            lstbx.ItemsSource = field.Choices;
                            lstbx.SelectedIndex = -1;

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
                            txt.Margin = new Thickness(50, numCtrl * 20, 0, 0);
                            txt.Width = 80;

                            Calendar cal = new Calendar();
                            cal.SetValue(NameProperty, "cal_" + field.Name);
                            cal.Margin = new Thickness(140, numCtrl * 20, 0, 0);
                            cal.Width = 280;
                            cal.Height = 200;
                            cal.SelectedDate = DateTime.Today;

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

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            Canvas cnv = (Canvas)CanvasIncident.FindName("PnlNew");
            DTIssue i = my_issue_template;

            DTIssue issue = new DTIssue();
            issue.Fields = new List<DTField>();
            foreach (DTField f in i.Fields)
            {
                DTField field = new DTField();
                switch (f.Type)
                {
                    case Types.Boolean:
                        CheckBox info = (CheckBox)cnv.FindName("chk_" + f.Name);
                        field.Value = info.IsChecked.ToString();
                        field.Type = f.Type;
                        field.Name = f.Name;
                        break;
                    case Types.Choice:
                        ListBox lst = (ListBox)cnv.FindName("lstbx_" + f.Name);
                        field.Value = lst.SelectedItem as string;
                        field.Type = f.Type;
                        field.Name = f.Name;
                        break;
                    case Types.DateTime:
                        Calendar cal = (Calendar)cnv.FindName("cal_" + f.Name);
                        field.Value = cal.SelectedDate.Value.ToShortDateString();
                        field.Type = f.Type;
                        field.Name = f.Name;
                        break;
                    default:
                        if (f.Name != "ID")
                        {
                            TextBox txt = (TextBox)cnv.FindName("bx_" + f.Name);
                            field.Value = txt.Text;
                            field.Type = f.Type;
                            field.Name = f.Name;
                        }
                        else if ((f.Name == "ID") &&(isEdit))
                        {
                            Issue select_issue = (Issue)grdIncident.SelectedItem;
                            field.Name = "ID";
                            field.Value = select_issue.Id.ToString();
                            field.Type = Types.Integer;
                        }
                        break;
                }
                issue.Fields.Add(field);

            }

            if (!isEdit)
            {
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.AddIssueCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_AddIssueCompleted);
                ws.AddIssueAsync(issue);
            }
            else if (isEdit) 
            {   
                WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
                ws.ModifyIssueCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_ModifyIssueCompleted);
                ws.ModifyIssueAsync(issue);
            }
        }

        void ws_ModifyIssueCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) 
        {
            Canvas cnv = (Canvas)CanvasIncident.FindName("PnlNew");
            cnv.Children.Clear();
            PnlAction.Visibility = Visibility.Collapsed;
            PnlbtnNuevo.Visibility = Visibility.Visible;
            GetIncidents(); 
        }

        void ws_AddIssueCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Canvas cnv = (Canvas)CanvasIncident.FindName("PnlNew");
            cnv.Children.Clear();
            PnlAction.Visibility = Visibility.Collapsed;
            PnlbtnNuevo.Visibility = Visibility.Visible;
            GetIncidents();
        }
        
        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            PnlbtnNuevo.Visibility = Visibility.Collapsed;

            Issue issue = (Issue)grdIncident.SelectedItem;
            int id = issue.Id;
            DTIssue change = new DTIssue();
            foreach (DTIssue i in my_issue)
            {
                foreach (DTField f in i.Fields)
                {
                    if((f.Name == "ID") && (f.Value == id.ToString()))
                    {
                        change = i;
                    }
                }
            }
            isEdit = true;
            ShowPanelforEdit(change);
            PnlAction.Visibility = Visibility.Visible;
        }

        public void ShowPanelforEdit(DTIssue issue) 
        {
            Canvas cnv = (Canvas)CanvasIncident.FindName("PnlNew");
            int numCtrl = 0;
            foreach (DTField field in issue.Fields)
            {
                if (field.Name != "ID" && field.Name != "")
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
                            chk.IsChecked = (field.Value == "True");
                            chk.Margin = new Thickness(140, numCtrl * 20, 0, 0);
                            chk.Width = 40;

                            cnv.Children.Add(txt);
                            cnv.Children.Add(chk);
                            break;
                        case Types.Choice:
                            txt.Text = field.Name;
                            txt.SetValue(NameProperty, "txt_" + field.Name);
                            txt.Margin = new Thickness(50, numCtrl * 50, 0, 0);
                            txt.Width = 80;

                            ListBox lstbx = new ListBox();
                            lstbx.SetValue(NameProperty, "lstbx_" + field.Name);
                            foreach (DTField field_lst in my_issue_template.Fields)
                            {
                                if((field_lst.Type == Types.Choice) && (field_lst.Name == field.Name))
                                    lstbx.ItemsSource = field_lst.Choices;
                            }
                            lstbx.SelectedItem = field.Value;
                            lstbx.Margin = new Thickness(140, numCtrl * 50, 0, 0);
                            lstbx.Width = 80;

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
                            txt.Margin = new Thickness(50, numCtrl * 20, 0, 0);
                            txt.Width = 80;

                            Calendar cal = new Calendar();
                            cal.SetValue(NameProperty, "cal_" + field.Name);
                            cal.Margin = new Thickness(140, numCtrl * 20, 0, 0);
                            cal.Width = 280;
                            cal.Height = 200;
                            cal.SelectedDate = Convert.ToDateTime(field.Value);

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
                            bx.Text = field.Value;
                            bx.Margin = new Thickness(140, numCtrl * 20, 0, 0);
                            bx.Width = 80;

                            cnv.Children.Add(txt);
                            cnv.Children.Add(bx);
                            break;
                    }
                }

            }
        }
        
        private void BtnDelete_Action_Click(object sender, RoutedEventArgs e)
        {
            Issue issue = (Issue)grdIncident.SelectedItem;
            int id = issue.Id;
            WSTitaReference.WSTitaSoapClient ws = new Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoapClient();
            ws.DeleteIssueCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ws_DeleteIssueCompleted);
            ws.DeleteIssueAsync(id);
        }

        void ws_DeleteIssueCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Canvas cnv = (Canvas)CanvasIncident.FindName("PnlNew");
            cnv.Children.Clear();
            PnlAction.Visibility = Visibility.Collapsed;
            PnlbtnNuevo.Visibility = Visibility.Visible;
            GetIncidents();
        }

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
        }
        #endregion


    }
}
