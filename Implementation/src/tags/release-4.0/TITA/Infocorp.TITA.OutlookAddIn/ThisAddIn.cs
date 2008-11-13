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
        #region CONTEXTUALMENU Objects
        
        private Office.CommandBarButton btn;
        private Outlook.MailItem mail;
        private bool _inUse = false;
        
        #endregion

        #region TOOLBUTTONS Objects

        Office.CommandBar commandBar;
        Office.CommandBarButton firstButton;
       // Office.CommandBarButton secondButton;
        #endregion

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
                FormWPFCreate();
            }
        }
        #endregion

        #region Create and Open WPF form


        private void FormWPFCreate()
        {
            //Create a Windows form with WPF namespace
            HandlerAddIn oHandlerAddIn = HandlerAddIn.GetInstanceHandlerAddIn();
            if (mail != null)
            {
                oHandlerAddIn.MailSelected = new MyMail(mail);
            }
            oHandlerAddIn.BuildIncidentWindow();
            mail = null;
        }

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
                FormWPFCreate();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Debe seleccionar un mail", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region TOOLBUTTONS
        private void AddToolbar()
        {
            try
            {
                commandBar = ((CommandBars)this.Application.ActiveExplorer().CommandBars)["TITA Soft Toolbar"];
            }
            catch (ArgumentException e)
            {
                // Toolbar named Test does not exist so we should create it.
            }

            if (commandBar == null)
            {
                // Add a commandbar named Test.
                commandBar = Application.ActiveExplorer().CommandBars.Add("TITA Soft Toolbar", 1, missing, true);
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
        #endregion

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _inUse = true;
            this.Application.ItemContextMenuDisplay += new Outlook.ApplicationEvents_11_ItemContextMenuDisplayEventHandler(Application_ItemContextMenuDisplay);
            AddToolbar();
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }
     
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
