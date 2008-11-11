using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataBaseAccess;
using Infocorp.TITA.DataTypes;
using NUnit.Framework;
using NUnit.Gui;

namespace Infocorp.TITA.DataBaseAccess.Test
{
    [TestFixture]
    public class TestDataBaseAccess
    {
        [TestFixtureSetUp]
        public void Init()
        { /* Se usa para inicializar partes comunes del Test y no inicializar en c/u ... */
            DataBaseAccess db = new DataBaseAccess();
            List<DTContract> contracts = db.ContractList();
            DTContract contract = new DTContract();
            foreach (var con in contracts)
                db.DeleteContract(con.ContractId);
        }

        [TestFixtureTearDown]
        public void Cleanup()
        { /* Para liberar partes que se hayan usado en los test comunes ... */
            DataBaseAccess db = new DataBaseAccess();
            List<DTContract> contracts = db.ContractList();
            DTContract contract = new DTContract();
            foreach (var con in contracts)
                db.DeleteContract(con.ContractId);

        }


        [Test]
        public void PersistenceTest()
        {

            DTContract contract = new DTContract();
            contract.issuesList = "Issues";
            contract.Site = "http://localhost/infocorp";
            contract.taskList = "Tasks";
            contract.UserName = "developer1";
            contract.workPackageList = "Work Packages";
            DataBaseAccess db = new DataBaseAccess();
            db.AddContract(contract);
            DTContract contractNew = db.ContractList().Last();
            Assert.AreEqual(db.GetContract(contractNew.ContractId).Site.Trim(), contract.Site);
        }


        [Test]
        public void DeleteListTest()
        {
            DataBaseAccess db = new DataBaseAccess();
            DTContract contract = new DTContract();
            contract.issuesList = "Issues";
            contract.Site = "http://localhost/infocorp";
            contract.taskList = "Tasks";
            contract.UserName = "developer1";
            contract.workPackageList = "Work Packages";
            db.AddContract(contract);
            DTContract contractNew = new DTContract();
            contractNew.issuesList = "Issues";
            contractNew.Site = "http://localhost/infocorp";
            contractNew.taskList = "Tasks";
            contractNew.UserName = "developer1";
            contractNew.workPackageList = "Work Packages";
            db.AddContract(contractNew);
            List<DTContract> contracts =  db.ContractList();
            
            foreach(var con in contracts)
                db.DeleteContract(con.ContractId);
            Assert.AreEqual(db.ContractList().Count(), 0);
               
        }

        
    
    
    
    
    
    }
}
