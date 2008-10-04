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
    public class WITServices_Issues_Test
    {
        IWITServices witServices;
        MockRepository mocks;
        DataBaseAccess.DataBaseAccess dbMock;
        ISharePoint su;

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
            witServices = WITFactory.Instance().WITServicesInstance();
            mocks = new MockRepository();
            dbMock = mocks.CreateMock<DataBaseAccess.DataBaseAccess>();
            su = mocks.CreateMock<ISharePoint>();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void MustGetIssueTemplate()
        {
            string url = "http://www.testurl.com";
            List<DTField> fields = new List<DTField>() { new DTField("Test", DTField.Types.Boolean, true, null, "true") };
            using (mocks.Record())
            {
                Expect.On(su).Call(su.GetFieldsIssue(url)).Return(fields);
            }

            DTIssue issueTemplate = witServices.GetIssueTemplate(url);

            Assert.AreEqual(fields.Count, issueTemplate.Fields.Count);
            foreach (DTField field in issueTemplate.Fields)
            {
                Assert.Contains(field, fields);
            }
            
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