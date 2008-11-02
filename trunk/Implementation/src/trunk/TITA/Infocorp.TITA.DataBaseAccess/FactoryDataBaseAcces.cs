using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.DataBaseAccess
{
    public class FactoryDataBaseAcces
    {
        private static FactoryDataBaseAcces _instance;
        private static IDataBaseAccess _instanceIDataBaseAcces;
        public static FactoryDataBaseAcces GetInstance()
        {
            if (_instance == null)
                 _instance = new FactoryDataBaseAcces();
             return _instance;   
        }
        public static IDataBaseAccess GetIDataBaseAccess()
        {
            if (_instanceIDataBaseAcces == null)
                _instanceIDataBaseAcces = new DataBaseAccess();
            return _instanceIDataBaseAcces;
        }
    }
}
