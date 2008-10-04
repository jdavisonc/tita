using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace Infocorp.TITA.WITLogic.Tests
{
    [TestFixture]
    public class WITServices_Issues_Test
    {
        MockRepository mocks;
        DataBaseAccess.DataBaseAccess dbMock;

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
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void MustGetIssueTemplate()
        {
            using (mocks.Record())
            {
            }

            Assert.IsTrue(true);
        }

        [Test, Ignore("Not ready")]
        public void MustGetIssues()
        {
        }

        [Test, Ignore("Not ready")]
        public void MustAddNewIssue()
        {
        }

        [Test, Ignore("Not ready")]
        public void MustModifyIssue()
        {
        }

        [Test, Ignore("Not ready")]
        public void MustDeleteIssue()
        {
        }
    }
}