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

        #region FunctionalTests

        [Test]
        public void AddIssue()
        {
            string siteUrl = "1";
            ISharePoint sharepoint = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint();
            DTItem issue = new DTItem();
            issue.Attachments = new List<DTAttachment>();
            issue.Fields = sharepoint.GetFieldsIssue(siteUrl);
            string name = string.Empty;
            foreach (DTField field in issue.Fields)
            {
                try
                {
                    if (!field.Hidden && !field.IsReadOnly)
                    {
                        switch (field.GetCustomType())
                        {
                            case DTField.Types.Number:
                                (field as DTFieldAtomicNumber).Value = 0;
                                break;
                            case DTField.Types.String:
                                (field as DTFieldAtomicString).Value = "0";
                                break;
                            case DTField.Types.Choice:
                                (field as DTFieldChoice).Value = (field as DTFieldChoice).Choices.First();
                                break;
                            case DTField.Types.Boolean:
                                (field as DTFieldAtomicBoolean).Value = false;
                                break;
                            case DTField.Types.DateTime:
                                (field as DTFieldAtomicDateTime).Value = DateTime.Today.AddDays(5);
                                break;
                            case DTField.Types.Note:
                                (field as DTFieldAtomicNote).Value = "Nota";
                                break;
                            case DTField.Types.User:
                                (field as DTFieldChoiceUser).Value = (field as DTFieldChoiceUser).Choices.First();
                                break;
                            case DTField.Types.Counter:

                                break;
                            case DTField.Types.Lookup:
                                break;
                            case DTField.Types.Default:
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception exc)
                {
                    string msg = exc.Message;
                }
            }
            try
            {
                int originalCount = sharepoint.GetIssues(siteUrl, string.Empty).Count;
                WITFactory.Instance().WITServicesInstance().AddIssue(issue, siteUrl);
                WITFactory.Instance().WITServicesInstance().ApplyChanges(siteUrl);
                // sharepoint.AddIssue(siteUrl, issue);
                int newCount = sharepoint.GetIssues(siteUrl, string.Empty).Count;

                Assert.AreEqual(originalCount + 1, newCount);
            }
            catch (Exception exc)
            {
                Assert.Fail("No se agregó el issue. ", exc.Message);

            }

        }

        [Test]
        public void ModifyIssue()
        {
            string siteUrl = "1";
            string newTitle = DateTime.Now.ToString("ddMMyyyyhhmmss");
            ISharePoint sharepoint = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint();
            List<DTItem> issues = sharepoint.GetIssues(siteUrl, string.Empty);
            if (issues.Count > 0)
            {
                DTItem issue = sharepoint.GetIssues(siteUrl, string.Empty).First();
                (issue.GetField("Title") as DTFieldAtomicString).Value = newTitle;
                bool result = true;
                try
                {
                    result &= sharepoint.UpdateIssue(siteUrl, issue);
                    result &= WITFactory.Instance().WITServicesInstance().ApplyChanges(siteUrl);

                    issue = sharepoint.GetIssues(siteUrl, string.Empty).First();
                }
                catch (Exception exc)
                {
                    Assert.Fail("No se agregó el issue. ", exc.Message);

                }

                Assert.AreEqual(newTitle, (issue.GetField("Title") as DTFieldAtomicString).Value);
                Assert.IsTrue(result);
            }
            else
            {
                Assert.Fail("No hay items en sharepoint");
            }

        }

        [Test]
        public void DeleteIssue()
        {
            string siteUrl = "1";
            ISharePoint sharepoint = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint();
            List<DTItem> issues = sharepoint.GetIssues(siteUrl, string.Empty);
            if (issues.Count > 0)
            {
                DTItem issue = issues.First();

                int issueId = (issue.GetField("ID") as DTFieldCounter).Value;
                bool result = true;
                try
                {
                    result &= sharepoint.DeleteIssue(siteUrl, issueId);
                    result &= WITFactory.Instance().WITServicesInstance().ApplyChanges(siteUrl);

                    issues = sharepoint.GetIssues(siteUrl, string.Empty);
                }
                catch (Exception exc)
                {
                    Assert.Fail("No se agregó el issue. ", exc.Message);
                }

                Assert.IsTrue(result);
                Assert.IsFalse(issues.Contains(issue));
            }
            else
            {
                Assert.Fail("No hay items en sharepoint");
            }

        }

        #endregion

        #region UnitTests


        [Test]
        public void MustGetIssueTemplate()
        {
            string siteId = "1";
            List<DTField> fields = new List<DTField>() { new DTFieldAtomicString("Test", true, false, true) };
            using (mocks.Record())
            {
                Expect.On(suMock).Call(suMock.GetFieldsIssue(siteId)).Return(fields);
            }

            DTItem issueTemplate = witServices.GetIssueTemplate(siteId);

            Assert.AreEqual(fields.Count, issueTemplate.Fields.Count);
            foreach (DTField field in issueTemplate.Fields)
            {
                Assert.Contains(field, fields);
            }

        }

        [Test]
        public void MustGetIssues()
        {
            string siteId = "1";
            List<DTItem> issues = new List<DTItem>() { new DTItem(new List<DTField>(), new List<DTAttachment>()) };
            using (mocks.Record())
            {
                Expect.On(suMock).Call(suMock.GetIssues(siteId, string.Empty)).Return(issues);
            }

            List<DTItem> result = witServices.GetIssues(siteId);

            Assert.AreEqual(issues.Count, result.Count);
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

        #endregion
}