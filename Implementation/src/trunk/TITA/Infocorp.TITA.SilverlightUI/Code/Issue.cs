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
    public class Issue
    {

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string reportedBy;

        public string ReportedBy
        {
            get { return reportedBy; }
            set { reportedBy = value; }
        }
        private DateTime reportedDate;

        public DateTime ReportedDate
        {
            get { return reportedDate; }
            set { reportedDate = value; }
        }
        private string workPackage;

        public string WorkPackage
        {
            get { return workPackage; }
            set { workPackage = value; }
        }
        private float ord;

        public float Ord
        {
            get { return ord; }
            set { ord = value; }
        }
        private string priority;

        public string Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        private string category;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string resolution;

        public string Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }

        private bool isLocal = false;

        public bool IsLocal
        {
            get { return isLocal; }
            set { isLocal = value; }
        }
    }
}
