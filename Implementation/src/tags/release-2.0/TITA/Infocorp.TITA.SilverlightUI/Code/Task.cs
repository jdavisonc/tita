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
    public class Task
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

        private string priority;

        public string Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private double porComplete;

        public double PorComplete
        {
            get { return porComplete; }
            set { porComplete = value; }
        }

        private string assignedTo;

        public string AssignedTo
        {
            get { return assignedTo; }
            set { assignedTo = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime dueDate;

        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }

        private bool isLocal;

        public bool IsLocal
        {
            get { return isLocal; }
            set { isLocal = value; }
        }

    }
}
