using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.WITLogic
{
    public interface IWITServices
    {

        void ApplyChanges();

        #region WorkPackages

        #endregion

        #region Issues

        List<DTIssue> GetIssues(string urlSite);

        void AddNewIssue(DTIssue issue);

        void ModifyIssue(DTIssue issue);

        void DeleteIssue(int issueId);

        #endregion

        #region Tasks

        #endregion

        #region Contract

        //void AddNewContract(DTContract contract);

        //void ChangeCurrentContract(int contractId);

        #endregion

    }
}
