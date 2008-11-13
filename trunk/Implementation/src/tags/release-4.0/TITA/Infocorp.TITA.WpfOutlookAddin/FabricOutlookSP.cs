using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.OutlookSharePoint;

namespace Infocorp.TITA.WpfOutlookAddIn
{
    class FabricOutlookSP : IFabricOutlookSharePoint
    {
       
        #region IFabricOutlookSharePoint Members

        IOutlookSharePoint IFabricOutlookSharePoint.GetOutlookSharePoint()
        {
            ControllerOutlookSharepoint oControllerOutlook = new ControllerOutlookSharepoint();
            return oControllerOutlook.GetOutlookSharepoint();
        }

        #endregion
    }
}
