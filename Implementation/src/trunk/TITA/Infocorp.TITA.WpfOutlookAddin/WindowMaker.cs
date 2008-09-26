using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.WpfOutlookAddin;
using Infocorp.TITA.DataTypes;
using System.Windows.Controls;
using AC.AvalonControlsLibrary.Controls;

namespace Infocorp.TITA.WpfOutlookAddIn
{
    class WindowMaker
    {
        private StackPanel _mainPanel;
        private List<DTField> _issueFields;
        private Grid _grid;

        public WindowMaker( StackPanel mainPanel, List<DTField> issueFields)
        {
            _mainPanel = mainPanel;
            _issueFields = issueFields;
            _grid = new Grid();

            _grid.ColumnDefinitions.Add(new ColumnDefinition());
            _grid.ColumnDefinitions.Add(new ColumnDefinition());
            _grid.Height = 24;
        
            //IHandlerAddIn oHandlerAddIn = new HandlerAddIn();
            //InsertCombo(oHandlerAddIn.GetUrlContracts());
            
        }

        private ComboBox InsertCombo(List<DTUrl> listChoice)
        {
            ComboBox comboChoice = new ComboBox();
            comboChoice.SetValue(Grid.ColumnProperty, 1);
            comboChoice.ItemsSource = listChoice;
            return comboChoice;
        }
     
        private ComboBox InsertCombo(List<string> listChoice) 
        {
            ComboBox comboChoice = new ComboBox();
            comboChoice.SetValue(Grid.ColumnProperty, 1);
            comboChoice.ItemsSource = listChoice;
            return comboChoice;
        }

        private ComboBox InsertBooleanCombo()
        {
            ComboBox comboChoice = new ComboBox();
            List<string> listValues = new List<string>();
            listValues.Add("S");
            listValues.Add("N");
            comboChoice.SetValue(Grid.ColumnProperty, 1);
            comboChoice.ItemsSource = listValues;
            return comboChoice;
        }

        private Control InsertTextBox(string boxName) 
        {
            TextBox textBox = new TextBox();
            textBox.Height = 19;
            textBox.Width = 100;
            textBox.Name = boxName;
            // register textbox2's name with newgrid
            //newgrid.registername(textbox.name, textbox);
            textBox.Margin = new System.Windows.Thickness(140, 55, 0, 0);
            textBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            textBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            textBox.SetValue(Grid.ColumnProperty, 1);
            return textBox;

        }

        private Control InsertTextNote(int height, int width)
        {
            TextBox textBox = new TextBox();
            textBox.Height = height;
            textBox.Width = width;
            //textBox.Name = nameTextBox;
            //Register TextBox3's Name with newgrid
           // newgrid.RegisterName(TextBox4.Name, TextBox4);
            textBox.Margin = new System.Windows.Thickness(141, 80, 0, 72);
            //textBox.Margin = new System.Windows.Thickness(left, top, right, bottom);
            textBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            textBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            textBox.AcceptsReturn = true;
            textBox.AcceptsTab = true;
            textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            return textBox;
        }

        private Control AddValueLine(DTField lineField) 
        {
            Control oReturn = null;

            switch (lineField.Type)
            {
                case DTField.Types.Integer:
                    //textbox
                    oReturn = InsertTextBox(lineField.Value);
                    break;
                case DTField.Types.String:
                    //textbox
                    oReturn = oReturn = InsertTextBox(lineField.Value);
                    break;
                case DTField.Types.Choice:
                    //combo
                    oReturn = InsertCombo(lineField.Choices);
                    break;
                case DTField.Types.Boolean:
                    //combo boolean
                    oReturn = InsertBooleanCombo(); 
                    break;
                case DTField.Types.DateTime:
                    //datepicker
                    DatePicker oDate = new DatePicker();
                    oReturn = (Control)oDate; 
                    break;
                case DTField.Types.Note:
                    //Richtexbox
                    oReturn = InsertTextNote(19,100);
                    break;
                case DTField.Types.User:
                    //combo
                    oReturn = InsertCombo(lineField.Choices);
                    break;
                default:
                    break;
            }
            return oReturn;
        }

        private Label AddLabelLine(string labelName)
        {

            Label label = new Label();
            label.Content = labelName;
            label.FontSize = 12;
            Grid.SetRow(label, 0);
            
            return label;
        }

        private void AddGridLine(DTField lineField) 
        {
            if (lineField.Type != DTField.Types.Counter)
            {
                _grid.ColumnDefinitions.Add(new ColumnDefinition());
                _grid.ColumnDefinitions.Add(new ColumnDefinition());
                _grid.Height = 24;
                
                //agrega el nombre el label
                _grid.Children.Add(AddLabelLine(lineField.Name));
                
                //agrega el tipo correspondiente al Type del DTField
                _grid.Children.Add(AddValueLine(lineField));

            }
           
        }


        public bool GenerateWindow()
        {
            foreach (DTField item in _issueFields)
            {
                AddGridLine(item);
                
            }
            _mainPanel.Children.Add(_grid);
            return true;
        }
    }
}
