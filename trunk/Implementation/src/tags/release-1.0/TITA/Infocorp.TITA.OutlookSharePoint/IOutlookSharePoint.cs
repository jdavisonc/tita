using System;
using System.Collections.Generic;
using Infocorp.TITA.DataTypes;

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
        bool AddIssue(string urlSite, DTIssue issue);

        /// <summary>
        /// Retorna una coleccion de campos que contiene un incidente
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <returns>Coleccion de campos</returns>
        List<DTField> GetFieldsIssue(string urlSite);

    }
}
