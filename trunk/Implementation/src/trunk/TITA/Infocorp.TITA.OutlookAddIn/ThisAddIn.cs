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



namespace OutlookAddInTitaSoft
{
    public partial class ThisAddIn
    {
        #region CONTEXTUALMENU
        private Office.CommandBarButton btn;
        private Outlook.MailItem mail;
        #endregion

        #region TOOLBUTTONS
        Office.CommandBar commandBar;
        Office.CommandBarButton firstButton;
        Office.CommandBarButton secondButton;
        #endregion

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            this.Application.ItemContextMenuDisplay += new Outlook.ApplicationEvents_11_ItemContextMenuDisplayEventHandler(Application_ItemContextMenuDisplay);
            AddToolbar();
        }

        #region CONTEXTUALMENU
        void Application_ItemContextMenuDisplay(Office.CommandBar CommandBar, Outlook.Selection Selection)
        {
            if (Selection.Count > 0)
            {
                mail = Selection[1] as Outlook.MailItem;
                if (mail != null)
                {
                    btn = CommandBar.Controls.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, missing) as Office.CommandBarButton;
                    btn.Caption = "HOLA GUAPO...";
                    btn.Click += new Office._CommandBarButtonEvents_ClickEventHandler(btn_Click);
                }
            }
        }
       

        void btn_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            if (mail != null)
            {
                string subject = mail.Subject;
                string filter = @"@SQL=""urn:schemas:httpmail:subject"" like '%" + subject + "%'";
                Outlook.Table tbl = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox).GetTable(filter, Outlook.OlTableContents.olUserItems);
                string result = "";

                while (!tbl.EndOfTable)
                {
                    Outlook.Row row = tbl.GetNextRow();
                    string EntryID = row["EntryID"].ToString();
                    Outlook.MailItem oMail = (Outlook.MailItem)Application.Session.GetItemFromID(EntryID, Type.Missing);
                    result += oMail.Subject + " from " + oMail.SenderName + " on " + oMail.SentOn.ToString() + System.Environment.NewLine;// + EntryID.ToString();
                    // TODO: Actually delete it (oMail.Delete())
                }
                System.Windows.Forms.MessageBox.Show("Deberia Abrir Menu Alta_Inicidente: " + System.Environment.NewLine + result);
            }

        }
        #endregion


        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

            #region Button1 Event
        void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Controls.Button btn1 = sender as System.Windows.Controls.Button;
                Grid grid1 = (Grid)btn1.Parent;
                //Find from NameScope all TextBox controls 
                System.Windows.Controls.TextBox txtName = (System.Windows.Controls.TextBox)grid1.FindName("Name");
                System.Windows.Controls.TextBox txtSourname = (System.Windows.Controls.TextBox)grid1.FindName("Sourname");
                System.Windows.Controls.TextBox txtEmail = (System.Windows.Controls.TextBox)grid1.FindName("Email");
                //Call SendEmail Method to create email
                //SendEmail(txtName.Text, txtSourname.Text, txtEmail.Text);
                System.Windows.MessageBox.Show("HOLA VENTANA");

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message.ToString());
            }


        }
        #endregion

        #region Create and Open WPF form
        /// <param name="ctrl">Create WPF Form runtime</param>
        private void buttonOne_Click(Office.CommandBarButton ctrl, ref bool cancel)
        {
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
                firstButton.Caption = "button 1";
                firstButton.Tag = "button1";
                firstButton.Click += new Office._CommandBarButtonEvents_ClickEventHandler(buttonOne_Click);
                //firstButton.Click += new Office._CommandBarButtonEvents_ClickEventHandler(ButtonClick);
                
                // Add a second button to the command bar and an event handler.
                secondButton = (Office.CommandBarButton)commandBar.Controls.Add(
                    1, missing, missing, missing, missing);

                secondButton.Style = Office.MsoButtonStyle.msoButtonCaption;
                secondButton.Caption = "button 2";
                secondButton.Tag = "button2";
                secondButton.Click += new Office._CommandBarButtonEvents_ClickEventHandler(ButtonClick);

                commandBar.Visible = true;
            }
            catch (ArgumentException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        // Handles the event when a button on the new toolbar is clicked.
        private void ButtonClick(Office.CommandBarButton ctrl, ref bool cancel)
        {
            System.Windows.Forms.MessageBox.Show("You clicked: " + ctrl.Caption);
            //FormWPFCreate();
        }


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
