using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.WITLogic
{
    public interface IWITServices
    {

        bool ApplyChanges(string siteUrl);

        bool HasPendingChanges(string siteUrl);

        #region WorkPackages

        List<DTItem> GetWorkPackages(string urlSite);

        List<DTField> GetFieldsWorkPackage(string urlSite);

        bool AddWorkPackage(string urlSite, DTItem wp);

        bool DeleteWorkPackage(string urlSite, int idWp);

        bool UpdateWorkPackage(string urlSite, DTItem wp);

        #endregion

        #region Issues

        DTItem GetIssueTemplate(string urlSite);

        List<DTItem> GetIssues(string urlSite);

        void AddNewIssue(DTItem issue);

        void ModifyIssue(DTItem issue);

        void DeleteIssue(int issueId);

        #endregion

        #region Tasks

        List<DTItem> GetTasks(string urlSite);

        List<DTField> GetFieldsTask(string urlSite);
      
        bool AddTask(string urlSite, DTItem task);
      
        bool DeleteTask(string urlSite, int idTask);

        bool UpdateTask(string urlSite, DTItem task);

        #endregion

        #region Contract

        void AddNewContract(DTContract contract);

        void DeleteContract(string contractId);

        void ChangeCurrentContract(int contractId);

        List<DTContract> GetContracts();

        string GetContractSite(string contractId);

        void ModifyContract(DTContract contract);

        #endregion

    }
}
