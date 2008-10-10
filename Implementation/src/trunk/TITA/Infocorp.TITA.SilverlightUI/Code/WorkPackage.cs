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
    public class WorkPackage
    {
        private int id;
        private String title;
        private DateTime startDate;
        private DateTime endDate;
        private DateTime proposedEndDate;
        private string status;
        private double estimation;
        private bool isLocal = false;

        public String Title
        {
            get { return title; }
            set { title = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public DateTime ProposedEndDate
        {
            get { return proposedEndDate; }
            set { proposedEndDate = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public double Estimation
        {
            get { return estimation; }
            set { estimation = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public bool IsLocal
        {
            get { return isLocal; }
            set { isLocal = value; }
        }
    }
}
