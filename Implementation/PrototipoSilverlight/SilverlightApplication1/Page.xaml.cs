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
using SilverlightApplication1.Code;

namespace SilverlightApplication1
{
    public partial class Page : UserControl
    {
        private bool isEdit;

        public Page()
        {
            InitializeComponent();
            CargarGrilla();

        }

        private void CargarGrilla()
        {
            ServiceReference1.WebServiceSoapClient ws = new SilverlightApplication1.ServiceReference1.WebServiceSoapClient();
            ws.GetIssuesCompleted += new EventHandler<SilverlightApplication1.ServiceReference1.GetIssuesCompletedEventArgs>(ws_GetIssuesCompleted);
            ws.GetIssuesAsync();
        }
        void ws_GetIssuesCompleted(object sender, SilverlightApplication1.ServiceReference1.GetIssuesCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(MostrarGrilla(e.Result));
        }

        private Delegate MostrarGrilla(SilverlightApplication1.ServiceReference1.IssueListItem[] issue)
        {
            List<MyIssue> lstMyIssue = new List<MyIssue>();
            foreach (SilverlightApplication1.ServiceReference1.IssueListItem i in issue) 
            {
                lstMyIssue.Add(new MyIssue{
                                   ID = i.ows_ID, 
                                   Title = i.ows_Title,
                                   ReportedBy = i.ows_Reportedby,
                                   ReportedDate = i.ows_ReportedDate,
                                   WP = i.ows_WP,
                                   Ord = i.ows_Ord,
                                   Priority = i.ows_Priority,
                                   Category = i.ows_Category,
                                   Status = i.ows_Status,
                                   Resolution = i.ows_Resolution,
                                   LinkIssueIdNoMenu = i.ows_LinkIssueIDNoMenu
                                   });
            }
            grdDatos.ItemsSource = lstMyIssue;
            return null;
        }
               
        private void grdDatos_SelectionChanged(object sender, EventArgs e)
        {
                MyIssue issue = (MyIssue)grdDatos.SelectedItem;
                BtnNuevo.Visibility = Visibility.Visible;
                BtnModificar.Visibility = Visibility.Visible;
                LimpiarCampos();
                pnlNuevo.Visibility = Visibility.Collapsed;
                BtnNuevo.Visibility = Visibility.Visible;
                BtnModificar.Visibility = Visibility.Visible;
                BtnEliminar.Visibility = Visibility.Visible;
        }

        private void BtnNuevo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pnlNuevo.Visibility = Visibility.Visible;
            BtnModificar.Visibility = Visibility.Collapsed;
            BtnNuevo.Visibility = Visibility.Collapsed;
            BtnEliminar.Visibility = Visibility.Collapsed;
            isEdit = false;
        }

        private void BtnModificar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pnlNuevo.Visibility = Visibility.Visible;
            BtnModificar.Visibility = Visibility.Collapsed;
            BtnNuevo.Visibility = Visibility.Collapsed;
            BtnEliminar.Visibility = Visibility.Collapsed;
            MostrarForm();
            isEdit = true;
       
        }

        private void BtnEliminar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ServiceReference1.WebServiceSoapClient ws = new SilverlightApplication1.ServiceReference1.WebServiceSoapClient();
            string id = ((MyIssue)grdDatos.SelectedItem).ID.ToString();
            ws.DeleteIssueCompleted += new EventHandler<SilverlightApplication1.ServiceReference1.DeleteIssueCompletedEventArgs>(ws_DeleteIssueCompleted);
            ws.DeleteIssueAsync(id);
            CargarGrilla();
        }

        void ws_DeleteIssueCompleted(object sender, SilverlightApplication1.ServiceReference1.DeleteIssueCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtnAceptar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ServiceReference1.WebServiceSoapClient ws = new SilverlightApplication1.ServiceReference1.WebServiceSoapClient();
            if (!isEdit)
            {
               SilverlightApplication1.ServiceReference1.IssueListItem i = new SilverlightApplication1.ServiceReference1.IssueListItem();
               i.ows_Title = txtTitle.Text;
               i.ows_Reportedby = txtReportedBy.Text;
               //i.ows_ReportedDate = txtReportedDate.Text;
               i.ows_WP = txtWP.Text;
               i.ows_Priority = txtPriority.Text;
               i.ows_Category = txtCategory.Text;
               i.ows_Status = txtStatus.Text;
               i.ows_Resolution = txtResolution.Text;
       
               bool errorOrd = false;
               bool errorLink = false;

               try
               {
                   i.ows_Ord = Convert.ToInt32(txtOrd.Text);
               }
               catch (Exception) {
                   errorOrd = true;
               }
               try
               {
                   i.ows_LinkIssueIDNoMenu = Convert.ToInt32(txtLinkIssueIdNoMenu.Text);
               }
               catch (Exception)
               {
                   errorLink = true;
               }

               if (!errorOrd && !errorLink)
               {
                   ws.AddIssueCompleted += new EventHandler<SilverlightApplication1.ServiceReference1.AddIssueCompletedEventArgs>(ws_AddIssueCompleted);
                   ws.AddIssueAsync(i);

                   LimpiarCampos();
                   pnlNuevo.Visibility = Visibility.Collapsed;
                   BtnNuevo.Visibility = Visibility.Visible;
                   BtnModificar.Visibility = Visibility.Visible;
                   CargarGrilla();
               }
               else {
                   if (errorLink && errorOrd) {
                       txtLinkIssueIdNoMenu.Text = "Debe ingresar un int";
                       txtOrd.Text = "Debe ingresar un float";
                   }
                   else if (errorLink)
                   {
                       txtLinkIssueIdNoMenu.Text = "Debe ingresar un int";
                       txtOrd.Text = i.ows_Ord.ToString();
                   }
                   else if (errorOrd)
                   {
                       txtOrd.Text = "Debe ingresar un float";
                       txtLinkIssueIdNoMenu.Text = i.ows_LinkIssueIDNoMenu.ToString();
                   }
                   else {
                       txtLinkIssueIdNoMenu.Text = i.ows_LinkIssueIDNoMenu.ToString();
                       txtOrd.Text = i.ows_Ord.ToString();
                   }
                   txtCategory.Text = i.ows_Category;
                   txtPriority.Text = i.ows_Priority;
                   txtReportedBy.Text = i.ows_Reportedby;
                   //txtReportedDate.Text = i.ows_ReportedDate;
                   txtStatus.Text = i.ows_Status;
                   txtTitle.Text = i.ows_Title;
                   txtWP.Text = i.ows_WP;
                   txtResolution.Text = i.ows_Resolution;             
               }
            }
            else if (isEdit) 
            {
                
                MyIssue issue = (MyIssue)grdDatos.SelectedItem;
                SilverlightApplication1.ServiceReference1.IssueListItem i = new SilverlightApplication1.ServiceReference1.IssueListItem();
                i.ows_ID = issue.ID;
                i.ows_Category = txtCategory.Text;
                i.ows_Priority = txtPriority.Text;
                i.ows_Reportedby = txtReportedBy.Text;
                //i.ows_ReportedDate = txtReportedDate.Text;
                i.ows_Status = txtStatus.Text;
                i.ows_Title = txtTitle.Text;
                i.ows_WP = txtWP.Text;
                i.ows_Resolution = txtResolution.Text;

                bool errorOrd = false;
                bool errorLink = false;

                try
                {
                    i.ows_Ord = Convert.ToInt32(txtOrd.Text);
                }
                catch (Exception)
                {
                    errorOrd = true;
                }
                try
                {
                    i.ows_LinkIssueIDNoMenu = Convert.ToInt32(txtLinkIssueIdNoMenu.Text);
                }
                catch (Exception)
                {
                    errorLink = true;
                }
                if (!errorOrd && !errorLink)
                {
                    ws.UpdateIssueCompleted += new EventHandler<SilverlightApplication1.ServiceReference1.UpdateIssueCompletedEventArgs>(ws_UpdateIssueCompleted);
                    ws.UpdateIssueAsync(i);

                    LimpiarCampos();
                    pnlNuevo.Visibility = Visibility.Collapsed;
                    BtnNuevo.Visibility = Visibility.Visible;
                    BtnModificar.Visibility = Visibility.Visible;
                    BtnEliminar.Visibility = Visibility.Visible;
                    CargarGrilla();
                }
                else {
                    if (errorLink && errorOrd)
                    {
                        txtLinkIssueIdNoMenu.Text = "Debe ingresar un int";
                        txtOrd.Text = "Debe ingresar un float";
                    }
                    else if (errorLink)
                    {
                        txtLinkIssueIdNoMenu.Text = "Debe ingresar un int";
                        txtOrd.Text = i.ows_Ord.ToString();
                    }
                    else if (errorOrd)
                    {
                        txtOrd.Text = "Debe ingresar un float";
                        txtLinkIssueIdNoMenu.Text = i.ows_LinkIssueIDNoMenu.ToString();
                    }
                    else
                    {
                        txtLinkIssueIdNoMenu.Text = i.ows_LinkIssueIDNoMenu.ToString();
                        txtOrd.Text = i.ows_Ord.ToString();
                    }
                    txtCategory.Text = i.ows_Category;
                    txtPriority.Text = i.ows_Priority;
                    txtReportedBy.Text = i.ows_Reportedby;
                    //txtReportedDate.Text = i.ows_ReportedDate;
                    txtStatus.Text = i.ows_Status;
                    txtTitle.Text = i.ows_Title;
                    txtWP.Text = i.ows_WP;
                    txtResolution.Text = i.ows_Resolution;             
                }
            }
        }

        void ws_UpdateIssueCompleted(object sender, SilverlightApplication1.ServiceReference1.UpdateIssueCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void ws_AddIssueCompleted(object sender, SilverlightApplication1.ServiceReference1.AddIssueCompletedEventArgs e)
        {
            //Dispatcher.BeginInvoke(MostrarGrilla(e.Result));
        }

        private void BtnCancelar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LimpiarCampos();
            pnlNuevo.Visibility = Visibility.Collapsed;
            BtnNuevo.Visibility = Visibility.Visible;
            BtnModificar.Visibility = Visibility.Visible;
            BtnEliminar.Visibility = Visibility.Visible;
        }

        private void MostrarForm()
        {
             MyIssue issue = (MyIssue)grdDatos.SelectedItem;
             txtCategory.Text = issue.Category;
             txtLinkIssueIdNoMenu.Text = issue.LinkIssueIdNoMenu.ToString();
             txtOrd.Text = issue.Ord.ToString();
             txtPriority.Text = issue.Priority;
             txtReportedBy.Text = issue.ReportedBy;
             txtReportedDate.Text = issue.ReportedDate;
             txtStatus.Text = issue.Status;
             txtTitle.Text = issue.Title;
             txtWP.Text = issue.WP;
             txtResolution.Text = issue.Resolution;
        }

        private void LimpiarCampos()
        {
            txtCategory.Text = string.Empty;
            txtLinkIssueIdNoMenu.Text = string.Empty; ;
            txtOrd.Text = string.Empty;
            txtPriority.Text = string.Empty;
            txtReportedBy.Text = string.Empty;
            txtReportedDate.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtWP.Text = string.Empty;
            txtResolution.Text = string.Empty;
        }
        
    }
}
