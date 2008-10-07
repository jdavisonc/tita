using System;
using System.Collections.Generic;
using Infocorp.TITA.DataTypes;
using System.Globalization;

namespace Infocorp.TITA.SharePointUtilities
{
    public interface ISharePoint
    {
        #region ABM Issues

        /// <summary>
        /// Retorna la lista de incidentes de un Sitio
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <param name="CAMLQuery">CAML Query</param>
        /// <returns>Lista de DataType Incidente</returns>
        List<DTItem> GetIssues(string urlSite, string CAMLQuery);

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

        #endregion

        #region ABM Work Packages

        /// <summary>
        /// Retorna la lista de work package de un Sitio
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <param name="CAMLQuery">CAML Query</param>
        /// <returns>Lista de DataType Work Package</returns>
        List<DTItem> GetWorkPackages(string urlSite, string CAMLQuery);

        /// <summary>
        /// Retorna la definicion de los campos de un work package
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <returns>Lista de DataType Field</returns>
        List<DTField> GetFieldsWorkPackage(string urlSite);

        /// <summary>
        /// Alta de work package en SharePoint
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <param name="wp">DataType de Work Package</param>
        /// <returns>Retorna si fue satisfactoria la alta</returns>
        bool AddWorkPackage(string urlSite, DTItem wp);

        /// <summary>
        /// Baja de work package en SharePoint
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <param name="idWp">ID del work package a dar de baja</param>
        /// <returns>Retorna si fue satisfactoria la baja</returns>
        bool DeleteWorkPackage(string urlSite, int idWp);

        /// <summary>
        /// Actualizacion de un work package en SharePoint
        /// </summary>
        /// <param name="urlSite">Url de Sitio</param>
        /// <param name="wp">DataType del work package</param>
        /// <returns>Retorna si fue satisfactoria la actualizacion</returns>
        bool UpdateWorkPackage(string urlSite, DTItem wp);

        #endregion

        #region ABM Tasks

        /// <summary>
        /// Retorna la lista de Tasks de un Sitio
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <param name="CAMLQuery">CAML Query</param>
        /// <returns>Lista de DataType Tasks</returns>
        List<DTItem> GetTasks(string urlSite, string CAMLQuery);

        /// <summary>
        /// Retorna la definicion de los campos de un Task
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <returns>Lista de DataType Field</returns>
        List<DTField> GetFieldsTask(string urlSite);

        /// <summary>
        /// Alta de Task en SharePoint
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <param name="task">DataType de Task</param>
        /// <returns>Retorna si fue satisfactoria la alta</returns>
        bool AddTask(string urlSite, DTItem task);

        /// <summary>
        /// Baja de Task en SharePoint
        /// </summary>
        /// <param name="urlSite">Url del Sitio</param>
        /// <param name="idTask">ID del Task a dar de baja</param>
        /// <returns>Retorna si fue satisfactoria la baja</returns>
        bool DeleteTask(string urlSite, int idTask);

        /// <summary>
        /// Actualizacion de un Task en SharePoint
        /// </summary>
        /// <param name="urlSite">Url de Sitio</param>
        /// <param name="task">DataType del Task</param>
        /// <returns>Retorna si fue satisfactoria la actualizacion</returns>
        bool UpdateTask(string urlSite, DTItem task);

        #endregion

        /// <summary>
        /// Retorna informacion sobre la cultura del sitio
        /// </summary>
        /// <param name="urlSite">Url de Sitio</param>
        /// <returns>Retorna la informacion sobre la cultura del sitio</returns>
        CultureInfo GetSiteLocale(string urlSite);
    }
}
