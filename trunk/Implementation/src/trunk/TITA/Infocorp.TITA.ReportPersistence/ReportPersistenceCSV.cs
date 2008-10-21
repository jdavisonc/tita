using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;
using System.IO;

namespace Infocorp.TITA.ReportPersistence
{
    class ReportPersistenceCSV:IReportPersistenceCSV
    {
        private string _EXTENSION = ".csv";
        private string _FIELD_SEPARATOR = ",";
        private string _REGISTRY_END = "\n";
        

        private string _folderPathDest;

        private bool SetFolderDestination()
        {
            _folderPathDest = "Directorio de configuracion del sitio web donde se guardaran los reportes";
            return true;
        }

        private string _urlFolderPath;
        private bool SetUrlFolderPath() 
        {
            _urlFolderPath = "url raiz para retornar del reporte";
            return true;
        }

        /// <summary>
        /// funcion para generar la url que surge del nombre del reporte y la ruta del directorio con permisos de 
        /// lectura escritura para generar los reportes.
        /// </summary>
        /// <param name="reportName">Nombre del reporte generado </param>
        /// <returns> la url al reporte generado en el directorio con permisos para generarar los mismos</returns>
        private string ReportUrlToGet(string reportName) 
        {
            return _urlFolderPath + reportName;
        }

        #region IReportPersistenceCSV Members

        public string ReportDesvWorkPackageToCSV(List<DTWorkPackageReport> reportData)
        {
            this.SetFolderDestination();
            string reportName = "desvWorkPackage" + DateTime.Now.TimeOfDay.ToString()+ this._EXTENSION;
            try
            {
                
                StreamWriter stream = File.CreateText(this._folderPathDest+reportName);
                string lineData ;
                foreach (DTWorkPackageReport item in reportData)
                {
                    lineData = item.Site + this._FIELD_SEPARATOR + item.IdWorkPackage + this._FIELD_SEPARATOR + item.Title + this._FIELD_SEPARATOR + item.Desviation + this._REGISTRY_END;
                    stream.Write(lineData);
                }
                stream.Close();
                return ReportUrlToGet(reportName);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }


        public string IssuesReportToCSV(List<DTIssueReport> reportData)
        {
            this.SetFolderDestination();
            string reportName = "reporteIncidentes" + DateTime.Now.TimeOfDay.ToString() + this._EXTENSION;
            try
            {

                StreamWriter stream = File.CreateText(this._folderPathDest + reportName);
                string lineData;
                foreach (DTIssueReport item in reportData)
                {
                    lineData = item.IdIssue + this._FIELD_SEPARATOR + item.Title + this._FIELD_SEPARATOR + item.Site + this._FIELD_SEPARATOR + item.WorkPackage + this._REGISTRY_END;
                    stream.Write(lineData);
                }
                stream.Close();
                return ReportUrlToGet(reportName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
