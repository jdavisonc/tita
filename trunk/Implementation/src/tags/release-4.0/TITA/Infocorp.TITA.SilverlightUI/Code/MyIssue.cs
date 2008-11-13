using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Infocorp.TITA.SilverlightUI.Code
{
        public class MyIssue
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string ReportedBy { get; set; }
            public string ReportedDate { get; set; }
            public string WP { get; set; }
            public float Ord { get; set; }
            public string Priority { get; set; }
            public string Category { get; set; }
            public string Status { get; set; }
            public string Resolution { get; set; }
            public int LinkIssueIdNoMenu { get; set; }

        }
}
