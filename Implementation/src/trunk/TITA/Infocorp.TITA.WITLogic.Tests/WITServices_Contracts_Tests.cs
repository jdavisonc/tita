using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Infocorp.TITA.SharePointUtilities;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.WITLogic.Tests
{
    [TestFixture]
    public class WITServices_Contracts_Tests
    {
        IWITServices witServices;
        MockRepository mocks;
        DataBaseAccess.DataBaseAccess dbMock;
        ISharePoint suMock;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {

        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            dbMock = mocks.CreateMock<DataBaseAccess.DataBaseAccess>();
            suMock = mocks.CreateMock<ISharePoint>();
            witServices = WITFactory.Instance().WITServicesInstance(dbMock, suMock);
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void GetContracts()
        {
            DTContract contract = new DTContract();
            contract.ContractId = "1";
            contract.issuesList = string.Empty;
            contract.Site = string.Empty;
            contract.taskList = string.Empty;
            contract.UserName = string.Empty;
            contract.workPackageList = string.Empty;

            DTContract contract2 = new DTContract();
            contract2.ContractId = "2";
            contract2.issuesList = string.Empty;
            contract2.Site = string.Empty;
            contract2.taskList = string.Empty;
            contract2.UserName = string.Empty;
            contract2.workPackageList = string.Empty;

            List<DTContract> contracts = new List<DTContract>() { contract, contract2 };


            using (mocks.Record())
            {

                Expect.On(dbMock).Call(dbMock.ContractList()).Return(contracts);
            }

                List<DTContract> result = witServices.GetContracts();

                //Assert.AreEqual(contracts.Count, result.Count);
                //foreach (DTContract c in contracts)
                //{
                //    Assert.Contains(c, result);
                //}
            
        }

        [Test]
        public void AddContract()
        {
            DTContract contract = new DTContract();
            contract.ContractId = "1";

            using (mocks.Record())
            {                
                dbMock.AddContract(contract);
            }

            witServices.AddNewContract(contract);

        }

        [Test]
        public void RemoveContract()
        {
            string contractId = "1";

            using (mocks.Record())
            {
                dbMock.DeleteContract(contractId);
            }

            witServices.DeleteContract(contractId);

        }

        [Test]
        public void UpdateContract()
        {
            DTContract contract = new DTContract();
            contract.ContractId = "1";

            using (mocks.Record())
            {
                dbMock.ModifyContract(contract);
            }

            witServices.ModifyContract(contract);

        }



    }
}
