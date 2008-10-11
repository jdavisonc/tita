using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.WpfOutlookAddin
{
    public class DTUrl
    {
        private string _contractName;

        public string ContractName
        {
            get { return _contractName; }
        }
    
        private string _contractUrl;

        public string ContractUrl
        {
            get { return _contractUrl; }
        }

        private string _issueList;

        public string IssueList
        {
            get { return _issueList; }
            set { _issueList = value; }
        }

        public DTUrl(string contractName, string contractUrl, string issueList)
        {
            _contractName = contractName;
            _contractUrl = contractUrl;
            _issueList = issueList;
        }        
        

    }
}
