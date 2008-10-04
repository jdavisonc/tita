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

        public DTUrl(string contractName, string contractUrl)
        {
            _contractName = contractName;
            _contractUrl = contractUrl;
        }        
        

    }
}
