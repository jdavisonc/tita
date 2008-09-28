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
using System.Windows.Shapes;
using Infocorp.TITA.WpfOutlookAddin;

namespace Infocorp.TITA.WpfOutlookAddIn
{
    /// <summary>
    /// Interaction logic for Contracts.xaml
    /// </summary>
    public partial class Contracts : Window
    {
        private List<DTUrl> _contracts;

        public Contracts(List<DTUrl> contracts)
        {
            InitializeComponent();
            _contracts = contracts;
            List<string> listContracts = new List<string>();
            foreach (DTUrl item in contracts)
            {
                listContracts.Add(item.ContractName);
            }
            comboBox1.ItemsSource = listContracts;
        }

        private void btnContracts_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
