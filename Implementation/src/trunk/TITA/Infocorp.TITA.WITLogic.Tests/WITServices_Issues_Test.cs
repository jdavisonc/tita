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

        [Test]
        public void AddIssue()
        {
            string siteUrl = "http://localhost/infocorp";
            ISharePoint sharepoint = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint();
            DTItem issue = new DTItem();
            issue.Attachments = new List<DTAttachment>();
            issue.Fields = sharepoint.GetFieldsIssue(siteUrl);
            string name = string.Empty;
            foreach (DTField field in issue.Fields)
            {
                try
                {
                    if (!field.Hidden && !field.IsReadOnly )
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
                Assert.Fail("No se agregó el issue. ",  exc.Message);
                
            }

        }


        //    IWITServices witServices;
        //    MockRepository mocks;
        //    DataBaseAccess.DataBaseAccess dbMock;
        //    ISharePoint suMock;

        //    [TestFixtureSetUp]
        //    public void TestFixtureSetUp()
        //    {

        //    }

        //    [TestFixtureTearDown]
        //    public void TestFixtureTearDown()
        //    {
        //    }

        //    [SetUp]
        //    public void SetUp()
        //    {
        //        mocks = new MockRepository();
        //        dbMock = mocks.CreateMock<DataBaseAccess.DataBaseAccess>();
        //        suMock = mocks.CreateMock<ISharePoint>();
        //        witServices = WITFactory.Instance().WITServicesInstance(dbMock, suMock);
        //    }

        //    [TearDown]
        //    public void TearDown()
        //    {
        //        mocks.VerifyAll();
        //    }

        //    [Test]
        //    public void MustGetIssueTemplate()
        //    {
        //        string url = "http://www.testurl.com";
        //        List<DTField> fields = new List<DTField>() { new DTField("Test", DTField.Types.Boolean, true, null, "true") };
        //        using (mocks.Record())
        //        {
        //            Expect.On(suMock).Call(suMock.GetFieldsIssue(url)).Return(fields);
        //        }

        //        DTIssue issueTemplate = witServices.GetIssueTemplate(url);

        //        Assert.AreEqual(fields.Count, issueTemplate.Fields.Count);
        //        foreach (DTField field in issueTemplate.Fields)
        //        {
        //            Assert.Contains(field, fields);
        //        }

        //    }

        //    [Test]
        //    public void MustGetIssues()
        //    {
        //        string url = "http://www.testurl.com";
        //        List<DTIssue> issues = new List<DTIssue>() { new DTIssue(new List<DTField>(), new List<DTAttachment>()) };
        //        using (mocks.Record())
        //        {
        //            Expect.On(suMock).Call(suMock.GetIssues(url)).Return(issues);
        //        }

        //        List<DTIssue> result = witServices.GetIssues(url);

        //        Assert.AreEqual(issues.Count, result.Count);
        //    }

        //    [Test, Ignore("Not ready")]
        //    public void MustAddNewIssue()
        //    {
        //    }

        //    [Test, Ignore("Not ready")]
        //    public void MustModifyIssue()
        //    {
        //    }

        //    [Test, Ignore("Not ready")]
        //    public void MustDeleteIssue()
        //    {
        //    }
        //}
    }
}