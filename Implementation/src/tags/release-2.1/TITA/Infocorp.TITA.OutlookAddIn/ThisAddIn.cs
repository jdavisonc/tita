using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

using Microsoft.Office.Core;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infocorp.TITA.WpfOutlookAddin;
using Microsoft.Office.Interop.Outlook;



namespace OutlookAddInTitaSoft
{
    public partial class ThisAddIn
    {
        #region CONTEXTUALMENU
        private Office.CommandBarButton btn;
        private Outlook.MailItem mail;
        private bool _inUse = false;
        #endregion

        #region TOOLBUTTONS
        Office.CommandBar commandBar;
        Office.CommandBarButton firstButton;
       // Office.CommandBarButton secondButton;
        #endregion

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _inUse = true;
            this.Application.ItemContextMenuDisplay += new Outlook.ApplicationEvents_11_ItemContextMenuDisplayEventHandler(Application_ItemContextMenuDisplay);
            AddToolbar();
        }

        #region CONTEXTUALMENU
        void Application_ItemContextMenuDisplay(Office.CommandBar CommandBar, Outlook.Selection Selection)
        {
            if (Selection.Count > 0)
            {
                mail = Selection[1] as Outlook.MailItem;
                Attachments oAttach = mail.Attachments;
                if (mail != null)
                {
                    btn = CommandBar.Controls.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, missing) as Office.CommandBarButton;
                    btn.Caption = "Reportar &Incidente";
                    btn.Click += new Office._CommandBarButtonEvents_ClickEventHandler(btn_Click);
                }
            }
        }
       

        void btn_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            if (mail != null)
            {
                //string subject = mail.Subject;
                //string filter = @"@SQL=""urn:schemas:httpmail:subject"" like '%" + subject + "%'";
                //Outlook.Table tbl = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox).GetTable(filter, Outlook.OlTableContents.olUserItems);
                //string result = "";
                //while (!tbl.EndOfTable)
                //{
                //    Outlook.Row row = tbl.GetNextRow();
                //    string EntryID = row["EntryID"].ToString();
                //    Outlook.MailItem oMail = (Outlook.MailItem)Application.Session.GetItemFromID(EntryID, Type.Missing);
                //    result += oMail.Subject + " from " + oMail.SenderName + " on " + oMail.SentOn.ToString() + System.Environment.NewLine;// + EntryID.ToString();
                //}
                FormWPFCreate();
            }
            //sos emo ??
        }
        #endregion


        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void FormWPFCreate()
        {
            //Create a Windows form with WPF namespace
            HandlerAddIn oHandlerAddIn = HandlerAddIn.GetInstanceHandlerAddIn();
            if (mail != null)
            { 
                //MyMail oMyMailSelected =
                oHandlerAddIn.MailSelected = new MyMail(mail);
            }
            oHandlerAddIn.BuildIncidentWindow();
            
            #region Comentada No va MAS
            ////Create the Grid Control
            //Grid newgrid = new Grid();
            ////Show the Grid Lines
            //newgrid.ShowGridLines = true;

            ////Define Row of Grid
            //RowDefinition rowDef1 = new RowDefinition();
            //rowDef1.Height = new GridLength(102, GridUnitType.Pixel);
            //RowDefinition rowDef2 = new RowDefinition();
            //rowDef2.Height = new GridLength(171, GridUnitType.Pixel);
            ////Add the row to the grid component
            //newgrid.RowDefinitions.Add(rowDef1);
            //newgrid.RowDefinitions.Add(rowDef2);

            //// Create a name scope for the Grid
            //// and assign a name
            //NameScope.SetNameScope(newgrid, new NameScope());

            #region Label Travel Agency
            //System.Windows.Controls.Label label1 = new System.Windows.Controls.Label();
            //label1.Height = 32.2766666666667;
            //label1.Margin = new System.Windows.Thickness(113.37, 4.7233333333333, 0, 0);
            //label1.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //label1.FontSize = 16;
            //label1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //label1.Width = 150.63;
            //label1.Content = "Infocorp";
            //// Add to Grid the component Label1
            //Grid.SetRow(label1, 0);
            #endregion

            #region Label World Tour
            //System.Windows.Controls.Label label2 = new System.Windows.Controls.Label();
            //label2.Height = 34.2766666666667;
            //label2.Margin = new System.Windows.Thickness(184.37, 27.7233333333333, 142, 40.0000000000001);
            //label2.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //label2.FontSize = 20;
            //System.Windows.Media.FontFamily myFont = new System.Windows.Media.FontFamily("Arial");
            //label2.FontFamily = myFont;
            //label2.Foreground = Brushes.Blue;
            //label2.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            //label2.Width = 125.63;
            //label2.Content = "Incidente";
            //Grid.SetRow(label2, 0);
            #endregion

            #region Image Travel Agency
            ////System.Windows.Controls.Image image1 = new System.Windows.Controls.Image();
            ////image1.Width = 100;
            ////System.Windows.Media.Imaging.BitmapImage imageSource = new System.Windows.Media.Imaging.BitmapImage();
            ////imageSource.BeginInit();
            //////Put your directory where is located
            //////your png image
            //////imageSource.UriSource = new Uri(@"C:\Fulvio\img\yast_suse_tour.png");
            ////imageSource.DecodePixelHeight = 100;
            ////imageSource.EndInit();
            ////image1.Margin = new System.Windows.Thickness(-180, 6, 175, 8);
            ////image1.Source = imageSource;
            //////image1.Height = 88;
            ////image1.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            ////image1.Stretch = Stretch.Uniform;
            ////Grid.SetRow(image1, 0);
            #endregion

            #region Label "Fill Form"
            //System.Windows.Controls.Label label3 = new System.Windows.Controls.Label();
            //label3.Height = 23.2766666666667;
            //label3.Margin = new System.Windows.Thickness(20.37, 5.93852320675109, 0, 0);
            //label3.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //label3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //label3.Width = 86.63;
            //label3.Content = "Datos Incidente:";
            //Grid.SetRow(label3, 1);
            #endregion

            #region Label Name
            //System.Windows.Controls.Label label4 = new System.Windows.Controls.Label();
            //label4.Height = 23.2766666666667;
            //label4.Margin = new System.Windows.Thickness(18.37, 28.938523206751, 0, 0);
            //label4.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //label4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //label4.Width = 70.63;
            //label4.Content = "Name: ";
            //Grid.SetRow(label4, 1);
            #endregion

            #region Label Sourname
            //System.Windows.Controls.Label label5 = new System.Windows.Controls.Label();
            //label5.Margin = new System.Windows.Thickness(17.37, 53.938523206751, 0, 93.7848101265823);
            //label5.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //label5.Width = 71.63;
            //label5.Content = "Sourname: ";
            //Grid.SetRow(label5, 1);
            #endregion

            #region Label Email
            //System.Windows.Controls.Label label6 = new System.Windows.Controls.Label();
            //label6.Margin = new System.Windows.Thickness(18.3699999999999, 77.7233333333333, 0, 70);
            //label6.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //label6.Width = 35.63;
            //label6.Content = "Email:";
            //Grid.SetRow(label6, 1);
            #endregion

            #region Name's TextBox
            //System.Windows.Controls.TextBox TextBox1 = new System.Windows.Controls.TextBox();
            //TextBox1.Height = 19;
            //TextBox1.Width = 100;
            //TextBox1.Name = "Name";
            //// Register TextBox2's name with newgrid
            //newgrid.RegisterName(TextBox1.Name, TextBox1);
            //TextBox1.Margin = new System.Windows.Thickness(140, 31, 0, 0);
            //TextBox1.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //TextBox1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //Grid.SetRow(TextBox1, 1);
            #endregion

            #region Sourname's TextBox
            //System.Windows.Controls.TextBox TextBox2 = new System.Windows.Controls.TextBox();
            //TextBox2.Height = 19;
            //TextBox2.Width = 100;
            //TextBox2.Name = "Sourname";
            //// Register TextBox2's name with newgrid
            //newgrid.RegisterName(TextBox2.Name, TextBox2);
            //TextBox2.Margin = new System.Windows.Thickness(140, 55, 0, 0);
            //TextBox2.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //TextBox2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //Grid.SetRow(TextBox2, 1);
            #endregion

            #region Email's TextBox
            //System.Windows.Controls.TextBox TextBox4 = new System.Windows.Controls.TextBox();
            //TextBox4.Height = 19;
            //TextBox4.Width = 100;
            //TextBox4.Name = "Email";
            ////Register TextBox3's Name with newgrid
            //newgrid.RegisterName(TextBox4.Name, TextBox4);
            //TextBox4.Margin = new System.Windows.Thickness(141, 80, 0, 72);
            //TextBox4.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //TextBox4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //Grid.SetRow(TextBox4, 1);
            #endregion

            #region Label Copyright
            //System.Windows.Controls.Label labelCopyright = new System.Windows.Controls.Label();
            //labelCopyright.Content = "Copyrights 2008-2009 TITA Soft Grupo 02 'The best'";
            //labelCopyright.Background = Brushes.BurlyWood;
            //labelCopyright.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            //labelCopyright.Margin = new Thickness(0, 1.7233333333333, -266, 0);
            //labelCopyright.Width = 269.63;
            //labelCopyright.Height = 24.2766666666667;
            //labelCopyright.VerticalAlignment = VerticalAlignment.Top;
            //labelCopyright.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            //// Create a RotateTransform to rotate
            //// the labelCopyright 90 degrees about its
            //// top-left corner.
            //RotateTransform labelCopyrightRotateTransform = new RotateTransform(90);
            //labelCopyright.RenderTransform = labelCopyrightRotateTransform;
            //Grid.SetRow(labelCopyright, 0);
            #endregion

            #region Button
            //System.Windows.Controls.Button button1 = new System.Windows.Controls.Button();
            //button1.Height = 39;
            //button1.Margin = new System.Windows.Thickness(16, 0, 0, 16);
            //button1.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            //button1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //button1.Width = 191;
            //button1.Content = "Impactar";
            //button1.Click += new RoutedEventHandler(button1_Click);
            //Grid.SetRow(button1, 1);
            #endregion

            #endregion

            mail = null;
        }
         
        #region Button1 Event NO SE USA
        //void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        System.Windows.Controls.Button btn1 = sender as System.Windows.Controls.Button;
        //        Grid grid1 = (Grid)btn1.Parent;
        //        //Find from NameScope all TextBox controls 
        //        System.Windows.Controls.TextBox txtName = (System.Windows.Controls.TextBox)grid1.FindName("Name");
        //        System.Windows.Controls.TextBox txtSourname = (System.Windows.Controls.TextBox)grid1.FindName("Sourname");
        //        System.Windows.Controls.TextBox txtEmail = (System.Windows.Controls.TextBox)grid1.FindName("Email");
        //        //Call SendEmail Method to create email
        //        //SendEmail(txtName.Text, txtSourname.Text, txtEmail.Text);
        //        System.Windows.MessageBox.Show("HOLA VENTANA");

        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.MessageBox.Show("Error: " + ex.Message.ToString());
        //    }


        //}
        #endregion

        #region Create and Open WPF form
        /// <param name="ctrl">Create WPF Form runtime</param>
        private void buttonOne_Click(Office.CommandBarButton ctrl, ref bool cancel)
        {
            Outlook.Selection Selection = Application.ActiveExplorer().Selection;
            if (Selection.Count > 0)
            {
                mail = Selection[1] as Outlook.MailItem;
               
            }
            if (mail != null)
            {
                //string subject = mail.Subject;
                //string filter = @"@SQL=""urn:schemas:httpmail:subject"" like '%" + subject + "%'";
                //Outlook.Table tbl = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox).GetTable(filter, Outlook.OlTableContents.olUserItems);
                //string result = "";
                //while (!tbl.EndOfTable)
                //{
                //    Outlook.Row row = tbl.GetNextRow();
                //    string EntryID = row["EntryID"].ToString();
                //    Outlook.MailItem oMail = (Outlook.MailItem)Application.Session.GetItemFromID(EntryID, Type.Missing);
                //    result += oMail.Subject + " from " + oMail.SenderName + " on " + oMail.SentOn.ToString() + System.Environment.NewLine;// + EntryID.ToString();
                //}

                FormWPFCreate();
               
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Debe seleccionar un mail","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

       

        #region TOOLBUTTONS
        private void AddToolbar()
        {
            try
            {
                commandBar = ((CommandBars)this.Application.ActiveExplorer().CommandBars)["Test"];
            }
            catch (ArgumentException e)
            {
                // Toolbar named Test does not exist so we should create it.
            }

            if (commandBar == null)
            {
                // Add a commandbar named Test.
                commandBar = Application.ActiveExplorer().CommandBars.Add("Test", 1, missing, true);
            }

            try
            {
                // Add a button to the command bar and an event handler.
                firstButton = (Office.CommandBarButton)commandBar.Controls.Add(
                    1, missing, missing, missing, missing);

                firstButton.Style = Office.MsoButtonStyle.msoButtonCaption;
                firstButton.Caption = "Reportar &Incidente";
                firstButton.Tag = "Reportar Incidente";
                firstButton.Click += new Office._CommandBarButtonEvents_ClickEventHandler(buttonOne_Click);
                //commandBar
                commandBar.Visible = true;
            }
            catch (ArgumentException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        // Handles the event when a button on the new toolbar is clicked.
        //private void ButtonClick(Office.CommandBarButton ctrl, ref bool cancel)
        //{
        //    System.Windows.Forms.MessageBox.Show("You clicked: " + ctrl.Caption);
        //    //FormWPFCreate();
        //}


        #endregion

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
