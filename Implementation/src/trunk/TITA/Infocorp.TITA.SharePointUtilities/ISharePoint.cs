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
        List<DTItem> GetIssues(string urlSite);

        /// <summary>
        /// Retorna la definicion de los campos de un incidente
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <returns>Lista de DataType Field</returns>
        List<DTField> GetFieldsIssue(string urlSite);

        /// <summary>
        /// Alta de incidente en SharePoint
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <param name="issue">DataType del incidente</param>
        /// <returns>Retorna si fue satisfactoria la alta</returns>
        bool AddIssue(string urlSite, DTItem issue);

        /// <summary>
        /// Baja de incidente en SharePoint
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <param name="IDIssue">ID del incidente a dar de baja</param>
        /// <returns>Retorna si fue satisfactoria la baja</returns>
        bool DeleteIssue(string urlSite, int IDIssue);

        /// <summary>
        /// Actualizacion de un incidente en SharePoint
        /// </summary>
        /// <param name="urlSite">Url de Sitio</param>
        /// <param name="issue">DataType del incidente</param>
        /// <returns>Retorna si fue satisfactoria la actualizacion</returns>
        bool UpdateIssue(string urlSite, DTItem issue);

    }
}
