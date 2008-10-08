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

        List<DTItem> GetWorkPackages(string siteUrl);

        DTItem GetWorkPackageTemplate(string siteUrl);

        void AddWorkPackage(DTItem workPackage, string siteUrl);

        void DeleteWorkPackage(int workPackageId, string siteUrl);

        void UpdateWorkPackage(DTItem workPackage, string siteUrl);

        #endregion

        #region Issues

        DTItem GetIssueTemplate(string siteUrl);

        List<DTItem> GetIssues(string siteUrl);

        void AddIssue(DTItem issue, string siteUrl);

        void ModifyIssue(DTItem issue, string siteUrl);

        void DeleteIssue(int issueId, string siteUrl);

        #endregion

        #region Tasks

        List<DTItem> GetTasks(string siteUrl);

        DTItem GetTaskTemplate(string siteUrl);

        void AddTask(DTItem task, string siteUrl);

        void DeleteTask(int taskId, string siteUrl);

        void UpdateTask(DTItem task, string siteUrl);

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