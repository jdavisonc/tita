using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SharepointUtilities
{
    public class RowData<T>
    {
        private int itemCount = 0;
        private T[] listItems;

        [XmlAttribute("ItemCount")]
        public int ItemCount
        {
            get
            {
                return itemCount;
            }
            set
            {
                itemCount = value;
            }
        }

        [XmlElement(ElementName = "row", Namespace = "#RowsetSchema")]
        public T[] ListItems
        {
            get
            {
                return listItems;
            }
            set
            {
                listItems = value;
            }
        }

    }
}
