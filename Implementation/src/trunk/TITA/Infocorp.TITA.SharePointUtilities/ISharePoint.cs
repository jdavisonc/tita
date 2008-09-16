using System;
using System.Collections.Generic;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.SharePointUtilities
{
    public interface ISharePoint
    {
        /// <summary>
        /// Retorna la lista de incidentes de un Sitio
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <returns>Lista de DataType Incidente</returns>
        List<DTIssue> GetIssues(string urlSite);
    }
}
