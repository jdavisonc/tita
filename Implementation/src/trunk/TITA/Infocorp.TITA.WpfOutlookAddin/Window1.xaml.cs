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
        //public Window1()
        //{
        //    InitializeComponent();
        //}
        //Label label1 = null;
        //Button button1 = null;

        public Window1(List<DTField> issueFields)
        {
            InitializeComponent();
            WindowMaker oWindow = new WindowMaker(StackPanelDT, issueFields);
            oWindow.GenerateWindow();
        }
        /*
              public void Generator(StackPanel mainPanel)
              {
            
            

          

                  //TextBox text = new TextBox();
                  //text.SetValue(Grid.ColumnProperty, 1);
                  ComboBox text = new ComboBox();
                  text.SetValue(Grid.ColumnProperty, 1);
                  List<string> lista = new List<string>();
                  lista.Add("hola");
                  lista.Add("ddddddd");
                  lista.Add("wwwwwww");
                  text.ItemsSource = lista;

                  grid.Children.Add(label1);
                  grid.Children.Add(text);

                  mainPanel.Children.Add(grid);
           
              }
      */
    }
}
