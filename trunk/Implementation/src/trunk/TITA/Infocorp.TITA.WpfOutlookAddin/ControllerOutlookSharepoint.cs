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
            OutlookSharePoint2003 oOutlook2003 = new OutlookSharePoint2003();
            return (IOutlookSharePoint)oOutlook2003;

            return null;
        }
    }
}
