using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;

namespace ConsoleApplication1
{
    public class IssueListItem : StockListItem
    {

        public IssueListItem ()
        {
            LinkIssueIdNoMenu = 0;
            Description = String.Empty;
            Status = String.Empty;
            Priority = String.Empty;
            Ord = 0;
            Category = String.Empty;
            ReportedDate = String.Empty;
            WP = String.Empty;
            ReportedBy = String.Empty;
            Resolution = String.Empty;
        }

        [XmlAttribute("ows_LinkIssueIDNoMenu")]
        public int LinkIssueIdNoMenu { get; set; }

        [XmlAttribute("ows_Description")]
        public string Description { get; set; }

        [XmlAttribute("ows_Status")]
        public string Status { get; set; }

        [XmlAttribute("ows_Priority")]
        public string Priority { get; set; }

        [XmlAttribute("ows_Ord")]
        public float Ord { get; set; }

        [XmlAttribute("ows_Category")]
        public string Category { get; set; }

        [XmlAttribute("ows_Reported Date")]
        public string ReportedDate { get; set; }

        /* [XmlAttribute("ows_Reported Date")]
         public DateTime ReportedDate 
         { 
             get
             {
                 return ReportedDate;
             }
             set
             {
                 ReportedDate = DateTime.ParseExact(value.ToString, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
             }
         }*/

        [XmlAttribute("ows_WP")]
        public string WP { get; set; }

        [XmlAttribute("ows_Reported by")]
        public string ReportedBy { get; set; }

        [XmlAttribute("ows_Resolution")]
        public string Resolution { get; set; }

        public string ToXml()
        {
            string xml = base.ToXml();
            xml += "<Field Name=\"Resolution\">" + Resolution + "</Field>";
            xml += "<Field Name=\"Reported_x0020_Date\" Type=\"DateTime\">" + ReportedDate + "</Field>";
            xml += "<Field Name=\"Reported_x0020_by\">" + ReportedBy + "</Field>";
            xml += "<Field Name=\"WP\">" + WP + "</Field>";
            xml += "<Field Name=\"Category\">" + Category + "</Field>";
            xml += "<Field Name=\"Ord\" Type=\"Float\">" + Ord + "</Field>";
            xml += "<Field Name=\"LinkIssueIDNoMenu\">" + LinkIssueIdNoMenu + "</Field>";
            xml += "<Field Name=\"Description\">" + Description + "</Field>";
            xml += "<Field Name=\"Status\">" + Status + "</Field>";
            xml += "<Field Name=\"Priority\">" + Priority + "</Field>";
            return xml;
        }

    }
}
