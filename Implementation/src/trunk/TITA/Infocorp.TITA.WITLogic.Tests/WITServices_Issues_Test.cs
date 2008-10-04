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
            mocks = new MockRepository();
            dbMock =  mocks.CreateMock<DataBaseAccess.DataBaseAccess>();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        void MustGetIssueTemplate()
        {
            Assert.IsTrue(true);
        }

        [Test, Ignore("Not ready")]
        void MustGetIssues()
        {
        }

        [Test, Ignore("Not ready")]
        void MustAddNewIssue()
        {
        }

        [Test, Ignore("Not ready")]
        void MustModifyIssue()
        {
        }

        [Test, Ignore("Not ready")]
        void MustDeleteIssue()
        {
        }

    }
}