using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Infocorp.TITA.DataTypes
{
    [Serializable]
    [DataContract]
    public class DTRol
    {
        public enum RolType
        {
            Administrator = 0,
            Reader = 1,
            WebDesigner = 2,
            Contributor = 3,
            None = 4,
            Guest = 5
        }

        private string _name;
        private RolType _type;
        private string _permissionMask;

        public DTRol() { }

        public DTRol(string name, string permissionMask, RolType type)
        {
            _name = name;
            _permissionMask = permissionMask;
            _type = type;
        }

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [DataMember]
        public string PermissionMask
        {
            get { return _permissionMask; }
            set { _permissionMask = value; }
        }

        [DataMember]
        public RolType Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
