using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    public class StockListItem
    {
        protected int id = 0;
        protected int attachments = 0;
        protected int hiddenVersion = 0;
        protected string linkTitle = string.Empty;
        protected string title = string.Empty;

        public StockListItem()
        {
        }

        [XmlAttribute("ows_Title")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        [XmlAttribute("ows_Attachments")]
        public int AttachmentCount
        {
            get
            {
                return attachments;
            }
            set
            {
                attachments = value;
            }
        }

        [XmlAttribute("ows_hiddenversion")]
        public int HiddenVersion
        {
            get
            {
                return hiddenVersion;
            }
            set
            {
                hiddenVersion = value;
            }
        }

        [XmlAttribute("ows_ID")]
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        [XmlAttribute("ows_LinkTitle")]
        public string LinkTitle
        {
            get
            {
                return linkTitle;
            }
            set
            {
                linkTitle = value;
            }
        }

        public string ToXml()
        {
            string xml = string.Empty;
            xml += "<Field Name=\"ID\">" + id + "</Field>";
            xml += "<Field Name=\"Title\">" + title + "</Field>";
            xml += "<Field Name=\"Attachments\">" + attachments + "</Field>";
            //xml += "<Field Name=\"hiddenversion\">" + hiddenVersion + "</Field>";
            xml += "<Field Name=\"LinkTitle\">" + linkTitle + "</Field>";
            return xml;
        }
    }
}
