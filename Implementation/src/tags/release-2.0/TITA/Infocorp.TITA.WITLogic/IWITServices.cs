﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.WITLogic
{
    public interface IWITServices
    {

        bool ApplyChanges(string contractId);

        bool HasPendingChanges(string contractId);

        #region WorkPackages

        List<DTItem> GetWorkPackages(string contractId);

        DTItem GetWorkPackageTemplate(string contractId);

        void AddWorkPackage(DTItem workPackage, string contractId);

        void DeleteWorkPackage(int workPackageId, string contractId);

        void UpdateWorkPackage(DTItem workPackage, string contractId);

        #endregion

        #region Issues

        DTItem GetIssueTemplate(string contractId);

        List<DTItem> GetIssues(string contractId);

        void AddIssue(DTItem issue, string contractId);

        void ModifyIssue(DTItem issue, string contractId);

        void DeleteIssue(int issueId, string contractId);

        #endregion

        #region Tasks

        List<DTItem> GetTasks(string contractId);

        DTItem GetTaskTemplate(string contractId);

        void AddTask(DTItem task, string contractId);

        void DeleteTask(int taskId, string contractId);

        void UpdateTask(DTItem task, string contractId);

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