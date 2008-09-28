using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Infocorp.TITA.WpfOutlookAddIn;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.WpfOutlookAddin
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Button _sendIssueButton;
        private Dictionary<DTField, Control> _mapElements;

        public Dictionary<DTField, Control> MapElements
        {
            get { return _mapElements; }
            set { _mapElements = value; }
        }

        public Button SendIssueButton
        {
            get { return _sendIssueButton; }
        }



        public Window1(List<DTField> issueFields)
        {
            InitializeComponent();
            _sendIssueButton = new Button();
            _sendIssueButton.Margin = new System.Windows.Thickness(30, 10, 30, 10);
            _sendIssueButton.Content = "Impactar";
          
            _mapElements = new Dictionary<DTField, Control>();

            WindowMaker oWindow = new WindowMaker(StackPanelDT, issueFields, _mapElements);
            oWindow.GenerateWindow(_sendIssueButton);
        }

        private void StackPanelDT_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
