using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.WITLogic
{
    
    public static class WITFactory
    {
        private static IWITServices _WITServicesInstance;
        public static IWITServices WITServicesInstance()
        {
            if (_WITServicesInstance == null){
                _WITServicesInstance = new WITServices();
            }

            return _WITServicesInstance;
        }
    }
}
