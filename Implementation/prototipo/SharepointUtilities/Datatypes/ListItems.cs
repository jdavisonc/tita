using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    [XmlRoot(ElementName = "listitems", Namespace = "http://schemas.microsoft.com/sharepoint/soap/")]
    public class ListItems<T> where T : StockListItem
    {
        private RowData<T> rowData = null;

        [XmlElement(ElementName = "data", Namespace = "urn:schemas-microsoft-com:rowset")]
        public RowData<T> RowData
        {
            get
            {
                return rowData;
            }
            set
            {
                rowData = value;
            }
        }

        public static int GetIDFromLookupValue(string lookupValue)
        {
            string[] arr = lookupValue.Split(';');
            return Int32.Parse(arr[0]);
        }

        public static string GetDescriptionFromLookupValue(string lookupValue)
        {
            string[] arr = lookupValue.Split(';');
            return arr[1].Remove(0, 1); // remove the #-sign
        }

        public static ListItems<T> FromXml(string xmlNode)
        {
            XmlSerializer xs = new XmlSerializer(typeof(ListItems<T>));
            XmlTextReader xtr = new XmlTextReader(xmlNode, XmlNodeType.Element, null);
            object o = xs.Deserialize(xtr);
            xtr.Close();
            return (ListItems<T>)o;
        }
    }
}
