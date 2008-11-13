using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable,DataContract]
    public class DTCurrentUser
    {
        private string _site;
        [DataMember]
        public string Site
        {
            set { _site = value; }
            get { return _site; }

        }
        private string _currentUser;
        [DataMember]
        public string CurrentUser
        {
            set { _currentUser = value; }
            get { return _currentUser; }
        }
        private string _loggedDate;
        [DataMember]
        public string LoggedDate
        {
            set { _loggedDate = value; }
            get { return _loggedDate; }
        }
        private string _lastModification;
        [DataMember]
        public string LastModification
        {
            set { _lastModification = value; }
            get { return _lastModification; }
        }

        public DTCurrentUser() { }
        public DTCurrentUser(string site, string currentUser, string loggedDate, string lastModification)
        {
            _site = site;
            _currentUser = currentUser;
            _loggedDate = loggedDate;
            _lastModification = lastModification;
        }
    
    
    }
}
