using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.WpfOutlookAddin
{
    /// <summary>
    /// Interfaz para comunicarse con  IOutlookSharePoint
    /// como manejador, y obtener datos para el AddIn
    /// </summary>
    public interface IHandlerAddIn
    {
        /// <summary>
        /// Obtiene los contratos disponibles, para seleccionar uno para impacatar
        /// </summary>
        /// <returns>
        /// Lista de DataUrls que contiene las urls disponibles para impactar incidentes
        /// </returns>
        List<DTUrl> GetUrlContracts();       

        /// <summary>
        /// Obtiene los campos que tiene que tener un incidente y 
        /// crea una ventana con esos campos
        /// </summary>
        
        void BuildIncidentWindow();

        /// <summary>
        /// Obtiene los datos de la ventana y crea un Issue
        /// para enviar a Sharepoint mediante la IOutlookSharePoint
        /// </summary>
        void BuildIssue(List<DTField> fields);


    }
}

