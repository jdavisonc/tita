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
        /// <param name="idContract">ID de contrato</param>
        /// <param name="CAMLQuery">CAML Query es el filtro a aplicar sobre la coleccion.</param>
        /// <returns>Lista de DataType Incidente</returns>
        List<DTItem> GetIssues(string idContract, string CAMLQuery);

        /// <summary>
        /// Mapeo y cambio de valor de una propiedad en particular de los items de la lista Issues
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <param name="property">Propiedad</param>
        /// <param name="initialValue">Valor inicial</param>
        /// <param name="endValue">Valor por el cual cambiar</param>
        void SiteMapPropertyValueIssues(string idContract, string property, string initialValue, string endValue);

        /// <summary>
        /// Retorna la definicion de los campos de un incidente
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <returns>Lista de DataType Field</returns>
        List<DTField> GetFieldsIssue(string idContract);

        /// <summary>
        /// Alta de incidente en SharePoint
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <param name="issue">DataType del incidente</param>
        /// <returns>Retorna si fue satisfactoria la alta</returns>
        bool AddIssue(string idContract, DTItem issue);

        /// <summary>
        /// Baja de incidente en SharePoint
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <param name="IDIssue">ID del incidente a dar de baja</param>
        /// <returns>Retorna si fue satisfactoria la baja</returns>
        bool DeleteIssue(string idContract, int IDIssue);

        /// <summary>
        /// Actualizacion de un incidente en SharePoint
        /// </summary>
        /// <param name="urlSite">Url de Sitio</param>
        /// <param name="issue">DataType del incidente</param>
        /// <returns>Retorna si fue satisfactoria la actualizacion</returns>
        bool UpdateIssue(string idContract, DTItem issue);

        #endregion

        #region ABM Work Packages

        /// <summary>
        /// Retorna la lista de work package de un Sitio
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <param name="CAMLQuery">CAML Query es el filtro a aplicar sobre la coleccion.</param>
        /// <returns>Lista de DataType Work Package</returns>
        List<DTItem> GetWorkPackages(string idContract, string CAMLQuery);

        /// <summary>
        /// Mapeo y cambio de valor de una propiedad en particular de los items de la lista WorkPackages
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <param name="property">Propiedad</param>
        /// <param name="initialValue">Valor inicial</param>
        /// <param name="endValue">Valor por el cual cambiar</param>
        void SiteMapPropertyValueWorkPackages(string idContract, string property, string initialValue, string endValue);

        /// <summary>
        /// Retorna la definicion de los campos de un work package
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <returns>Lista de DataType Field</returns>
        List<DTField> GetFieldsWorkPackage(string idContract);

        /// <summary>
        /// Alta de work package en SharePoint
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <param name="wp">DataType de Work Package</param>
        /// <returns>Retorna si fue satisfactoria la alta</returns>
        bool AddWorkPackage(string idContract, DTItem wp);

        /// <summary>
        /// Baja de work package en SharePoint
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <param name="idWp">ID del work package a dar de baja</param>
        /// <returns>Retorna si fue satisfactoria la baja</returns>
        bool DeleteWorkPackage(string idContract, int idWp);

        /// <summary>
        /// Actualizacion de un work package en SharePoint
        /// </summary>
        /// <param name="urlSite">Url de Sitio</param>
        /// <param name="wp">DataType del work package</param>
        /// <returns>Retorna si fue satisfactoria la actualizacion</returns>
        bool UpdateWorkPackage(string idContract, DTItem wp);

        #endregion

        #region ABM Tasks

        /// <summary>
        /// Retorna la lista de Tasks de un Sitio
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <param name="CAMLQuery">CAML Query es el filtro a aplicar sobre la coleccion.</param>
        /// <returns>Lista de DataType Tasks</returns>
        List<DTItem> GetTasks(string idContract, string CAMLQuery);

        /// <summary>
        /// Mapeo y cambio de valor de una propiedad en particular de los items de la lista Tasks
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <param name="property">Propiedad</param>
        /// <param name="initialValue">Valor inicial</param>
        /// <param name="endValue">Valor por el cual cambiar</param>
        void SiteMapPropertyValueTasks(string idContract, string property, string initialValue, string endValue);

        /// <summary>
        /// Retorna la definicion de los campos de un Task
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <returns>Lista de DataType Field</returns>
        List<DTField> GetFieldsTask(string idContract);

        /// <summary>
        /// Alta de Task en SharePoint
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <param name="task">DataType de Task</param>
        /// <returns>Retorna si fue satisfactoria la alta</returns>
        bool AddTask(string idContract, DTItem task);

        /// <summary>
        /// Baja de Task en SharePoint
        /// </summary>
        /// <param name="idContract">ID de contrato</param>
        /// <param name="idTask">ID del Task a dar de baja</param>
        /// <returns>Retorna si fue satisfactoria la baja</returns>
        bool DeleteTask(string idContract, int idTask);

        /// <summary>
        /// Actualizacion de un Task en SharePoint
        /// </summary>
        /// <param name="urlSite">Url de Sitio</param>
        /// <param name="task">DataType del Task</param>
        /// <returns>Retorna si fue satisfactoria la actualizacion</returns>
        bool UpdateTask(string idContract, DTItem task);

        #endregion

        /// <summary>
        /// Retorna informacion sobre la cultura del sitio
        /// </summary>
        /// <param name="urlSite">ID Contrato</param>
        /// <returns>Retorna la informacion sobre la cultura del sitio</returns>
        CultureInfo GetSiteLocale(string idContract);

        /// <summary>
        /// Retorna la coleccion de listas existente en un Sitio SharePoint
        /// </summary>
        /// <param name="urlSite">ID Contrato</param>
        /// <returns>Coleccion de string que representa los nombre de las listas</returns>
        List<String> GetLists(string idContract);

        /// <summary>
        /// Retorna los permisos de un usuario, segun el sitio del contrato
        /// </summary>
        /// <param name="idContract">ID contrato</param>
        /// <param name="username">Nombre de usuario</param>
        /// <returns>Lista de roles</returns>
        List<DTRol> GetPermissions(string idContract, string username);


        /// <summary>
        /// Obtiene la dirección del usuario
        /// </summary>
        /// <returns>Email</returns>
        string GetCurrentUserEmail();
    }
}
