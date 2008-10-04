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

        #endregion

        #region Issues

        DTIssue GetIssueTemplate(string urlSite);

        List<DTIssue> GetIssues(string urlSite);

        void AddNewIssue(DTIssue issue);

        void ModifyIssue(DTIssue issue);

        void DeleteIssue(int issueId);

        #endregion

        #region Tasks

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
