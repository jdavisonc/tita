using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Infocorp.TITA.SharePointUtilities;

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
    }
}
