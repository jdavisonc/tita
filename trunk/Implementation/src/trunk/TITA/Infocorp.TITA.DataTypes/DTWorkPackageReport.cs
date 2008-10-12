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
        public string Site {get {return _site;}}
        private string _idWorkPackage;
        [DataMember]
        public string IdWorkPackage { get { return _idWorkPackage; } }
        private string _title;
        [DataMember]
        public string Title { get { return _title; } }
        private string _desviation;
        [DataMember]
        public string Desviation { get { return _desviation; } }


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
