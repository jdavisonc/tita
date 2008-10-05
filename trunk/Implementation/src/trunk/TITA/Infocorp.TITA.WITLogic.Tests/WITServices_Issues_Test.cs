//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NUnit.Framework;
//using Rhino.Mocks;
//using Infocorp.TITA.SharePointUtilities;
//using Infocorp.TITA.DataTypes;

//namespace Infocorp.TITA.WITLogic.Tests
//{
//    [TestFixture]
//    public class WITServices_Issues_Test
//    {
//        IWITServices witServices;
//        MockRepository mocks;
//        DataBaseAccess.DataBaseAccess dbMock;
//        ISharePoint suMock;

//        [TestFixtureSetUp]
//        public void TestFixtureSetUp()
//        {
          
//        }

//        [TestFixtureTearDown]
//        public void TestFixtureTearDown()
//        {
//        }

//        [SetUp]
//        public void SetUp()
//        {
//            mocks = new MockRepository();
//            dbMock = mocks.CreateMock<DataBaseAccess.DataBaseAccess>();
//            suMock = mocks.CreateMock<ISharePoint>();
//            witServices = WITFactory.Instance().WITServicesInstance(dbMock, suMock);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            mocks.VerifyAll();
//        }

//        [Test]
//        public void MustGetIssueTemplate()
//        {
//            string url = "http://www.testurl.com";
//            List<DTField> fields = new List<DTField>() { new DTField("Test", DTField.Types.Boolean, true, null, "true") };
//            using (mocks.Record())
//            {
//                Expect.On(suMock).Call(suMock.GetFieldsIssue(url)).Return(fields);
//            }

//            DTIssue issueTemplate = witServices.GetIssueTemplate(url);

//            Assert.AreEqual(fields.Count, issueTemplate.Fields.Count);
//            foreach (DTField field in issueTemplate.Fields)
//            {
//                Assert.Contains(field, fields);
//            }
            
//        }

//        [Test]
//        public void MustGetIssues()
//        {
//            string url = "http://www.testurl.com";
//            List<DTIssue> issues = new List<DTIssue>() { new DTIssue(new List<DTField>(), new List<DTAttachment>()) };
//            using (mocks.Record())
//            {
//                Expect.On(suMock).Call(suMock.GetIssues(url)).Return(issues);
//            }

//            List<DTIssue> result = witServices.GetIssues(url);

//            Assert.AreEqual(issues.Count, result.Count);
//        }

//        [Test, Ignore("Not ready")]
//        public void MustAddNewIssue()
//        {
//        }

//        [Test, Ignore("Not ready")]
//        public void MustModifyIssue()
//        {
//        }

//        [Test, Ignore("Not ready")]
//        public void MustDeleteIssue()
//        {
//        }
//    }
//}