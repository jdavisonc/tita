using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [DataContract, Serializable]
    public class DTWorkPackageReport
    {
        private string _site;
        [DataMember]
        public string Site 
        {
            get {return _site;}
            set { _site = value.Trim(); }
        }

        private string _idWorkPackage;
        [DataMember]
        public string IdWorkPackage 
        { 
            get { return _idWorkPackage; }
            set { _idWorkPackage = value.Trim(); }
        }

        private string _title;
        [DataMember]
        public string Title 
        { 
            get { return _title; }
            set { _title = value.Trim(); }
        }

        private string _desviation;
        [DataMember]
        public string Desviation 
        { 
            get { return _desviation; }
            set { _desviation = value.Trim(); }
        }

        public DTWorkPackageReport() { }
        
        public DTWorkPackageReport(string site, string idWorkPackage, string title, string desviation)
        {
            _site = site;
            _idWorkPackage = idWorkPackage;
            _title = title;
            _desviation = desviation;
        }
    
    }
}
