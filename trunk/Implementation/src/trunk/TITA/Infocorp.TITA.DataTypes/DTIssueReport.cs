using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable, DataContract]
    public class DTIssueReport
    {
        private string _idIssue;
        [DataMember]
        public string IdIssue
        {
            get{ return _idIssue;}
            set{ _idIssue = value;}
        }
        private string _title;
        [DataMember]
        public string Title
        {
            get{ return _title;}
            set{ _title = value;}
        }
        private string _site;
        [DataMember]
        public string Site
        {
            get{ return _site;}
            set{ _site = value;}
        }
        private string _workPackage;
        [DataMember]
        public string WorkPackage
        {
            get{ return _workPackage;}
            set{ _workPackage = value;}
        }

        public DTIssueReport() { }

        public DTIssueReport(string idIssue, string title, string site, string workPackage)
        {
            _idIssue = idIssue;
            _title = title;
            _site = site;
            _workPackage = workPackage;
        }
    
    
    }
}
