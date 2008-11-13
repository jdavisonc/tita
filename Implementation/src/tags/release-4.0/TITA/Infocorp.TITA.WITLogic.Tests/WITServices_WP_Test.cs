using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Infocorp.TITA.SharePointUtilities;
using Infocorp.TITA.DataTypes;
using Rhino.Mocks;

namespace Infocorp.TITA.WITLogic.Tests
{
    [TestFixture]
    public class WITServices_WP_Test
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

        #region Tests Unitarios

        [Test]
        public void MustGetWorkPackageTemplate()
        {
            string contractId = "1";
            List<DTField> fields = new List<DTField>() { new DTFieldAtomicString("Test", "Test", true, false, true) };
            using (mocks.Record())
            {
                Expect.On(suMock).Call(suMock.GetFieldsWorkPackage(contractId)).Return(fields);
            }

            try
            {
                DTItem workPackageTemplate = witServices.GetWorkPackageTemplate(contractId);

                Assert.AreEqual(fields.Count, workPackageTemplate.Fields.Count);
                foreach (DTField field in workPackageTemplate.Fields)
                {
                    Assert.Contains(field, fields);
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void MustGetWorkPackages()
        {
            string contractId = "1";
            List<DTItem> workPackages = new List<DTItem>() { new DTItem(new List<DTField>(), new List<DTAttachment>()) };
            using (mocks.Record())
            {
                Expect.On(suMock).Call(suMock.GetWorkPackages(contractId, string.Empty)).Return(workPackages);
            }

            try
            {
                List<DTItem> result = witServices.GetWorkPackages(contractId);

                Assert.AreEqual(workPackages.Count, result.Count);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void MustAddNewWorkPackage()
        {
            DTItem workPackage = new DTItem(new List<DTField>(), new List<DTAttachment>());
            string contractId = "1";
            using (mocks.Record())
            {
                Expect.On(suMock).Call(suMock.AddWorkPackage(contractId, workPackage)).Return(true);
            }

            try
            {
                witServices.AddWorkPackage(workPackage, contractId);
                witServices.ApplyChanges(contractId, ItemType.WORKPACKAGE);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void MustModifyWorkPackage()
        {
            DTItem workPackage = new DTItem(new List<DTField>(), new List<DTAttachment>());
            workPackage.Fields.Add(new DTFieldCounter("ID", "ID", true, true, true, 1));
            string contractId = "1";
            using (mocks.Record())
            {
                Expect.On(suMock).Call(suMock.UpdateWorkPackage(contractId, workPackage)).Return(true);
            }

            try
            {
                witServices.UpdateWorkPackage(workPackage, contractId);
                witServices.ApplyChanges(contractId, ItemType.WORKPACKAGE);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void MustDeleteWorkPackage()
        {
            string contractId = "1";
            int workPackageId = 1;

            using (mocks.Record())
            {
                Expect.On(suMock).Call(suMock.DeleteWorkPackage(contractId, workPackageId)).Return(true);
            }

            try
            {
                witServices.DeleteWorkPackage(workPackageId, contractId);
                witServices.ApplyChanges(contractId, ItemType.WORKPACKAGE);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        #endregion

        #region Tests Funcionales

        /*
        [Test]
        public void AddWP()
        {
            string siteUrl = "1";
            ISharePoint sharepoint = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint();            
            DTItem workPackage = new DTItem();
            workPackage.Attachments = new List<DTAttachment>();
            workPackage.Fields = sharepoint.GetFieldsWorkPackage(siteUrl);
            string name = string.Empty;
            foreach (DTField field in workPackage.Fields)
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
                int originalCount = sharepoint.GetWorkPackages(siteUrl, string.Empty).Count;
                WITFactory.Instance().WITServicesInstance().AddWorkPackage(workPackage, siteUrl);
                WITFactory.Instance().WITServicesInstance().ApplyChanges(siteUrl, ItemType.WORKPACKAGE);

                int newCount = sharepoint.GetWorkPackages(siteUrl, string.Empty).Count;

                Assert.AreEqual(originalCount + 1, newCount);
            }
            catch (Exception exc)
            {
                Assert.Fail("No se agregó el wp. ", exc.Message);

            }

        }
 */

        #endregion


    }
}
