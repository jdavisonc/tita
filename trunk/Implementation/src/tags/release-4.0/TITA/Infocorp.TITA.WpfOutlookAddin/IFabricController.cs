using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.OutlookSharePoint;

namespace Infocorp.TITA.WpfOutlookAddIn
{
    public interface IFabricOutlookSharePoint
    {
        IOutlookSharePoint GetOutlookSharePoint();
    }
}
