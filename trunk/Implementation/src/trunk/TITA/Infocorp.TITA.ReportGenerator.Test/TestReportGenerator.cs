using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Gui;
using Infocorp.TITA.DataBaseAccess;
using Infocorp.TITA.DataTypes;
using Infocorp.TITA.ReportGenerator;

namespace Test
{
    [TestFixture]
    public class TestReportGenerator
    {
        [TestFixtureSetUp]
        public void Init()
        { /* Se usa para inicializar partes comunes del Test y no inicializar en c/u ... */
            DTContract contract = new DTContract();
            contract.issuesList = "Issues";
            contract.Site = "http://localhost/infocorp";
            contract.taskList = "Tasks";
            contract.workPackageList = "Work Packages";
            DataBaseAccess db = new DataBaseAccess();
            db.AddContract(contract);
            DTContract contractNew = new DTContract();
            contractNew.issuesList = "Issues";
            contractNew.Site = "http://localhost/pepe";
            contractNew.taskList = "Tasks";
            contractNew.workPackageList = "Work Packages";
       
            db.AddContract(contractNew);
        
        
        
        
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
        public void DesvWorkPackageInfocorp()
        {
            DataBaseAccess db = new DataBaseAccess();
            DTContract contract = db.ContractList().First();           
            IReportGenerator rep =  FactoryReport.GetInstance().GetIReportGenerator();            
            DateTime init = new DateTime(2007,1,1);
            DateTime final = new DateTime(2009,1,1);

            List<DTWorkPackageReport> wPackages = rep.ReportDesvWorkPackage(contract.ContractId, init, final);
            Assert.AreEqual(int.Parse(wPackages.First().Desviation), 30);
        }
        [Test]
        public void DesvWorkPackagePepe()
        {
            DataBaseAccess db = new DataBaseAccess();
            DTContract contract = db.ContractList().Last();
            IReportGenerator rep = FactoryReport.GetInstance().GetIReportGenerator();
            DateTime init = new DateTime(2007, 1, 1);
            DateTime final = new DateTime(2009, 1, 1);

            List<DTWorkPackageReport> wPackages = rep.ReportDesvWorkPackage(contract.ContractId, init, final);
            Assert.AreEqual(int.Parse(wPackages.Last().Desviation), 5);
        }
        [Test]
        public void AllIncidentChecking()
        {
            IReportGenerator rep = FactoryReport.GetInstance().GetIReportGenerator();
            DateTime init = new DateTime(2007, 1, 1);
            DateTime final = new DateTime(2009, 1, 1);
            List<DTReportedItem> issues = rep.IssuesReport(null, init, final);
            int changeReported = 0;
            int changeResolved = 0;
            int errorResolved = 0;
            int queryReported = 0;
            foreach (var aux in issues)
            {
                if (aux.Category.Equals("Change") && aux.Status.Equals("Reported"))
                    changeReported = aux.Count;
                else
                    if (aux.Category.Equals("Change") && aux.Status.Equals("Resolved"))
                        changeResolved = aux.Count;
                    else
                        if (aux.Category.Equals("Error") && aux.Status.Equals("Resolved"))
                            errorResolved = aux.Count;
                        else
                            if (aux.Category.Equals("Query") && aux.Status.Equals("Reported"))
                                queryReported = aux.Count;
                        
             }
            
            Assert.IsTrue((changeReported == 2) && (changeResolved == 1) && (errorResolved == 1) && (queryReported == 1));
        }

        [Test]
        public void InfocorpIncidentChecking()
        {
            DataBaseAccess db = new DataBaseAccess();
            DTContract contract = db.ContractList().First();
            IReportGenerator rep = FactoryReport.GetInstance().GetIReportGenerator();
            DateTime init = new DateTime(2007, 1, 1);
            DateTime final = new DateTime(2009, 1, 1);
            List<DTReportedItem> items = rep.IssuesReport(contract.ContractId, init, final);
            int changeReported = 0;
            int queryReported = 0;
            foreach (var aux in items)
            {
                if (aux.Category.Equals("Change") && aux.Status.Equals("Reported"))
                    changeReported = aux.Count;
                else
                    if (aux.Category.Equals("Query") && aux.Status.Equals("Reported"))
                        queryReported = aux.Count;
            }
            Assert.IsTrue((changeReported == 2) && (queryReported == 1));
                
        }

        [Test]
        public void PepeIncidentChecking()
        {
            DataBaseAccess db = new DataBaseAccess();
            DTContract contract = db.ContractList().Last();
            IReportGenerator rep = FactoryReport.GetInstance().GetIReportGenerator();
            DateTime init = new DateTime(2007, 1, 1);
            DateTime final = new DateTime(2009, 1, 1);
            List<DTReportedItem> items = rep.IssuesReport(contract.ContractId, init, final);
            int changeResolved = 0;
            int ErrorResolved = 0;
            foreach (var aux in items)
            {
                if (aux.Category.Equals("Change") && aux.Status.Equals("Resolved"))
                    changeResolved = aux.Count;
                else
                    if (aux.Category.Equals("Error") && aux.Status.Equals("Resolved"))
                        ErrorResolved = aux.Count;
            }
            Assert.IsTrue((changeResolved == 1) && (ErrorResolved == 1));

        }
    
    
    
    
    }
}
