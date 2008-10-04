using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.SharePointUtilities;

namespace Infocorp.TITA.WITLogic
{

    public class WITFactory
    {
        private static IWITServices _wITServicesInstance;
        private static WITFactory _instance;

        private WITFactory() { }

        public static WITFactory Instance()
        {
            if (_instance == null)
            {
                _instance = new WITFactory();
            }

            return _instance;
        }

        public IWITServices WITServicesInstance()
        {
            if (_wITServicesInstance == null)
            {
                _wITServicesInstance = new WITServices();
            }

            return _wITServicesInstance;
        }

        public IWITServices WITServicesInstance(DataBaseAccess.DataBaseAccess db, ISharePoint sharepoint)
        {
            if (_wITServicesInstance == null)
            {
                _wITServicesInstance = new WITServices(db, sharepoint);
            }

            return _wITServicesInstance;
        }
    }
}