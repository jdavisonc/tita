using System;
using System.Linq;
using System.Text;
using Infocorp.TITA.WpfOutlookAddin;
using Infocorp.TITA.DataTypes;
using System.Windows.Controls;
using AC.AvalonControlsLibrary.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;


namespace Infocorp.TITA.WpfOutlookAddIn
{
    class WindowMaker
    {
        private StackPanel _mainPanel;
        private List<DTField> _issueFields;
        private Dictionary<DTField, Control> _mapElements;
        private Grid _grid;
        private ScrollViewer _scrollViewer;

        public WindowMaker( StackPanel mainPanel, List<DTField> issueFields, Dictionary<DTField,Control> mapElements)
        {
            _mapElements = mapElements;
            _mainPanel = mainPanel;
            _issueFields = issueFields;
            _scrollViewer = new ScrollViewer();
            _grid = new Grid();

            _grid.ColumnDefinitions.Add(new ColumnDefinition());
            _grid.ColumnDefinitions.Add(new ColumnDefinition());
            _grid.ColumnDefinitions[0].Width = new GridLength(100, GridUnitType.Pixel);

            
            //_grid.SetValue( ScrollViewer.VerticalScrollBarVisibilityProperty ,);
            //_grid.Height = 24;
        
            //IHandlerAddIn oHandlerAddIn = new HandlerAddIn();
            //InsertCombo(oHandlerAddIn.GetUrlContracts());
        }

        private ComboBox InsertCombo(List<DTUrl> listChoice)
        {
            ComboBox comboChoice = new ComboBox();
            comboChoice.ItemsSource = listChoice;
            return comboChoice;
        }
     
        private ComboBox InsertCombo(List<string> listChoice) 
        {
            ComboBox comboChoice = new ComboBox();
            comboChoice.ItemsSource = listChoice;
            comboChoice.SelectedItem = listChoice[0];
            return comboChoice;
        }

        private ComboBox InsertBooleanCombo()
        {
            ComboBox comboChoice = new ComboBox();
            List<string> listValues = new List<string>();
            listValues.Add("True");
            listValues.Add("False");
            comboChoice.ItemsSource = listValues;
            comboChoice.SelectedItem = listValues[0];
            return comboChoice;
        }

        private Control InsertTextBox(string boxName) 
        {
            TextBox textBox = new TextBox();
            //textBox.Height = 19;
            //textBox.Width = 150;
            textBox.Name = boxName;
            // register textbox2's name with newgrid
            //newgrid.registername(textbox.name, textbox);
            //textBox.Margin = new System.Windows.Thickness(0, 3, 3, 3);
            //textBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //textBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            return textBox;

        }

        private Control InsertTextNote(int height, int width)
        {
            TextBox textBox = new TextBox();
            textBox.Height = height;
            //textBox.Width = width;
            //textBox.Name = nameTextBox;
            //Register TextBox3's Name with newgrid
           // newgrid.RegisterName(TextBox4.Name, TextBox4);
            //textBox.Margin = new System.Windows.Thickness(0, 3, 3, 3);
            //textBox.Margin = new System.Windows.Thickness(left, top, right, bottom);
            //textBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //textBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            textBox.AcceptsReturn = true;
            textBox.AcceptsTab = false;
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
                    oReturn = InsertTextNote(100,150);
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

        private Label AddLabelLine(DTField lineField)
        {

            Label label = new Label();
            label.Content = lineField.Name;
            label.FontSize = 12;
            if(lineField.Required)
            {
                label.Foreground = Brushes.Orange;
            }
            Grid.SetRow(label, 0);
            
            return label;
        }

        private Control AddGridLine(DTField lineField, int line) 
        {
            if (lineField.Type != DTField.Types.Counter)
            {
                //agrega el nombre el label
                Label oLabelLine = AddLabelLine(lineField);
                oLabelLine.SetValue(Grid.RowProperty, line);
                _grid.Children.Add(oLabelLine);
                
                //agrega el tipo correspondiente al Type del DTField
                Control oControl = AddValueLine(lineField);
                oControl.Margin = new System.Windows.Thickness(0, 3, 3, 3);
                oControl.SetValue(Grid.RowProperty, line);
                oControl.SetValue(Grid.ColumnProperty, 1);
                _grid.Children.Add(oControl);
                return oControl;

            }
            return null;
           
        }

        public bool GenerateWindow(Button sendIssueButton, double scrollHeight)
        {
            int i = 0;
            _scrollViewer.Height = scrollHeight;
            Control oTempControl;
            foreach (DTField item in _issueFields)
            {
                _grid.RowDefinitions.Add(new RowDefinition());
                oTempControl=AddGridLine(item,i++);
                if(oTempControl != null)
                {
                    _mapElements.Add(item, oTempControl);
                }
            }

            _grid.RowDefinitions.Add(new RowDefinition ());
            
            sendIssueButton.SetValue(Grid.ColumnProperty, 1);
            sendIssueButton.SetValue(Grid.RowProperty, i++);

            _grid.Children.Add(sendIssueButton);
            _scrollViewer.Content = _grid;
            _mainPanel.Children.Add(_scrollViewer);
            return true;
        }


        private void BuildIssueToSend(string urlIssue)
        {
           
            //List<DTAttachment> oMailListAttachments = new List<DTAttachment>();
            //DTIssue oDTIssueInfo = new DTIssue(_issueFields, oMailListAttachments);
 

           // oHandlerAddIn.BuildIssue(urlIssue,DTIssue issue);
            //url, DTIssue
        }


    }

}
