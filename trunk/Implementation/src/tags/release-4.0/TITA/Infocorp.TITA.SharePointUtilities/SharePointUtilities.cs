using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.SharePointUtilities
{
    public class SharePointUtilities
    {
        private static SharePointUtilities _instance = null;
        private ISharePoint _instanceISharePoint = null;

        public static SharePointUtilities GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SharePointUtilities();
            }
            return _instance;
        }

        public ISharePoint GetISharePoint()
        {
            if (_instanceISharePoint == null)
            {
                _instanceISharePoint = new SharePoint2003WS();
            }
            return _instanceISharePoint;
        }
    }
}
