using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.OutlookSharePoint
{
    public interface IOutlookSharePoint
    {
        /// <summary>
        /// Da de alta un Incidente
        /// </summary>
        /// <param name="urlSite">Url del Sitio a dar de alta</param>
        /// <param name="issue">DTIssue con los datos del Incidente</param>
        /// <returns></returns>
        bool AddIssue(string urlSite);//, DTIssue issue);

    }
}
