using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.OutlookSharePoint;

namespace Infocorp.TITA.WpfOutlookAddIn
{
    public class ControllerOutlookSharepoint
    {
        public IOutlookSharePoint GetOutlookSharepoint(){
            IOutlookSharePoint oOutlook2003 = new OutlookSharePoint2003WS();
            return oOutlook2003;
        }
    }
}
